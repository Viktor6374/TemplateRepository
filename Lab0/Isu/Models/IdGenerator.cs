namespace Isu.Models
{
    public class IdGenerator
    {
        private int idCounter = 1;
        public IdGenerator() { }
        public int Generate()
        {
            return idCounter++;
        }
    }
}
