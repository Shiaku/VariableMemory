using UnityEngine;
using GameStructure;
using Factory;

namespace MainMenu
{
    public class MenuController : MonoBehaviour
    {
        private int playerCount = 0;
        private Rule rule = null;
        private Builder builder = null;

        public void Start()
        {
            //Setting up default values for Quickstart
            playerCount = 2;
            SelectRuleType(0);
            SelectBuilderType(0);
        }
        public void SelectPlayerCount (int count)
        {
            playerCount = count;
        }
        public void SelectRuleType(int type)
        {
            rule = RuleFactory.CreateRule(type);
        }
        public void SelectBuilderType(int type)
        {
            builder = BuilderFactory.CreateBuilder(type);
        }
        public void StartGame()
        {
            new Game(playerCount, rule, builder);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }
    }
}
