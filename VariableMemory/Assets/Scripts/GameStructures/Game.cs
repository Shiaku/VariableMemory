using GameStructure.Rules;
using GameStructure.Builders;
namespace GameStructure
{
    public class Game
    {
        public static Game Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Game();
                    return instance;
                }
                return instance;
            }
        }
        private static Game instance;


        public Game(int numberOfPlayers, Rule rule, Builder builder) 
        {
            this.numberOfPlayers = numberOfPlayers;
            this.rule = rule;
            this.builder = builder;
            instance = this;
        }

        private Game() : this(2, new SimpleRule(), new HexagonBuilder())
        {
            
        }

        public int numberOfPlayers
        { get; private set; }
        public Rule rule
        { get; private set; }
        public Builder builder
        { get; private set; }
    }
}