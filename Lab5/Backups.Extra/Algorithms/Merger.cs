using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backups.Algorithms;
using Backups.Entities;
using Backups.Extra.Interfaces;
using Backups.Interfaces;
using Zio;

namespace Backups.Extra.Algorithms
{
    public class Merger : IRemoverRestorePoints
    {
        public void Remove(List<IRestorePoint> restorePoints, Backup backup)
        {
            if (backup.StorageAlgorithm is SingleStorageAlgorithm)
            {
                MergeSingleStorage(restorePoints, backup);
            }
            else if (backup.StorageAlgorithm is SplitStorageAlgorithm)
            {
                MergeSplitStorage(restorePoints, backup);
            }
            else
            {
                throw new ArgumentException("Invalid type storage algorithm");
            }
        }

        private void MergeSingleStorage(List<IRestorePoint> restorePoints, Backup backup)
        {
            var sortRestorePoints = restorePoints.OrderByDescending(x => x.DateTimeOfCreation).ToList();

            IRestorePoint resultRestorePoint = sortRestorePoints[0];
            sortRestorePoints.Remove(resultRestorePoint);

            var storegeOfResultRestorePoint = (SingleStorage)resultRestorePoint.Storage;
            using (Stream resultReadStream = resultRestorePoint.Repository.WriteFile(storegeOfResultRestorePoint.NameOfFile))
            {
                var resultZipArchive = new ZipArchive(resultReadStream);

                foreach (IRestorePoint restorePoint in sortRestorePoints)
                {
                    var storageOfRestorePoint = (SingleStorage)restorePoint.Storage;

                    using (Stream readFile = restorePoint.Repository.ReadFile(storageOfRestorePoint.NameOfFile))
                    {
                        var zipArchive = new ZipArchive(readFile);
                        var exceptEntry = zipArchive.Entries.ToList();

                        foreach (ZipArchiveEntry entry in resultZipArchive.Entries)
                        {
                            ZipArchiveEntry? removeEntry = exceptEntry.FirstOrDefault(x => x.FullName == entry.FullName && x.Length == entry.Length);

                            if (removeEntry != null)
                            {
                                exceptEntry.Remove(removeEntry);
                            }
                        }

                        foreach (ZipArchiveEntry zipArchiveEntry in exceptEntry)
                        {
                            CopyEntry(zipArchive, resultZipArchive, zipArchiveEntry.FullName);
                        }
                    }

                    var repository = (IAdvancedRepository)restorePoint.Repository;
                    repository.RemoveDirectory(string.Empty);
                    backup.RemoveRestorePoint(restorePoint);
                }
            }
        }

        private void MergeSplitStorage(List<IRestorePoint> restorePoints, Backup backup)
        {
            var sortRestorePoints = restorePoints.OrderByDescending(x => x.DateTimeOfCreation).ToList();

            var resultRepository = (IAdvancedRepository)sortRestorePoints[0].Repository;

            List<string> addedFiles = sortRestorePoints[0].Repository.GetAllFile();
            sortRestorePoints.Remove(sortRestorePoints[0]);

            foreach (IRestorePoint restorePoint in sortRestorePoints)
            {
                var repository = (IAdvancedRepository)restorePoint.Repository;
                List<string> filesInRestorePoint = restorePoint.Repository.GetAllFile();

                var exceptFiles = new List<string>();
                exceptFiles.AddRange(filesInRestorePoint);

                foreach (string addedFile in addedFiles)
                {
                    string? removeFile = filesInRestorePoint.FirstOrDefault(x => x == addedFile && repository.GetSizeFile(x) == resultRepository.GetSizeFile(addedFile));

                    if (removeFile != null)
                    {
                        exceptFiles.Remove(removeFile);
                    }
                }

                foreach (string exceptFile in exceptFiles)
                {
                    repository.MoveFile(exceptFile, UPath.Combine(new UPath(resultRepository.Path()), new UPath(exceptFile)).ToString());
                }

                repository.RemoveDirectory(string.Empty);
                backup.RemoveRestorePoint(restorePoint);
            }
        }

        private void CopyEntry(ZipArchive zipArchive, ZipArchive resultZipArchive, string relativePath)
        {
            ZipArchiveEntry zipArchiveEntry = zipArchive.GetEntry(relativePath) ?? throw new Exception("Entry not Exist");
            using (Stream streamOldEntry = zipArchiveEntry.Open())
            {
                ZipArchiveEntry newEntry = resultZipArchive.CreateEntry(relativePath);
                using (Stream streamNewEntry = newEntry.Open())
                {
                    streamOldEntry.CopyTo(streamNewEntry);
                }
            }
        }
    }
}
