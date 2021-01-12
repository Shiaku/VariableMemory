using GameStructure;
using GameStructure.Rules;

namespace Factory
{
    public class RuleFactory
    {
        public static Rule CreateRule(int id)
        {
            switch(id)
            {
                case 0:
                    return new SimpleRule();
                case 1:
                    return new DeleteRule();
                case 2:
                    return new FiveQueueRule();
                default:
                    return new SimpleRule();
            }
        }
    }
}
