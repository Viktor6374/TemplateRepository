using System;

namespace Isu.Exceptions
{
    public class CrowededGroupExeption : IsuException
    {
        public CrowededGroupExeption()
        {
            Console.WriteLine("The group is crowded");
        }
    }
}
