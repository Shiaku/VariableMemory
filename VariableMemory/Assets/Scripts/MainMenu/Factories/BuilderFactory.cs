using GameStructure;
using GameStructure.Builders;

namespace Factory
{
    public class BuilderFactory
    {
        public static Builder CreateBuilder(int id)
        {
            switch(id)
            {
                case 4:
                    return new SquareBuilder();
                case 5:
                    return new PentagonBuilder();
                case 6:
                    return new HexagonBuilder();
                default:
                    return new HexagonBuilder();
            }
        }
    }
}