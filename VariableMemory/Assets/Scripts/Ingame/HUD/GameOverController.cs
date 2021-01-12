using UnityEngine;
using UnityEngine.UI;

namespace Ingame.HUD.Gameover
{ 
    public class GameOverController : MonoBehaviour 
    {
        [SerializeField]
        private Text winnerText, roundText;
    
        public void Setup(int id, int rounds)
        {
            winnerText.text += id.ToString();
            roundText.text += rounds.ToString();
            gameObject.SetActive(true);
        }
    }
}
