using UnityEngine;
using UnityEngine.UI;
using Ingame.HUD.Gameover;

namespace Ingame.HUD
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField]
        private Text currentPlayer, currentRound, currentSelectionProgress;
        [SerializeField]
        private GameOverController gameOverObject;

        [SerializeField]
        private Announcer announcer;
        private float announceTime = 3f;


        public void AnnounceSelect()
        {
            SetupAnnouncer("Select");
        }
        public void AnnounceRepeat()
        {
            SetupAnnouncer("Repeat");
        }
        public void AnnounceDelete()
        {
            SetupAnnouncer("Delete");
        }

        private void SetupAnnouncer(string phaseName)
        {
            announcer.SetupText(phaseName);
            announcer.gameObject.SetActive(true);
            Invoke("ClearAnnouncer", announceTime);
        }

        private void ClearAnnouncer() //Invoked by SetupAnnouncer
        {
            announcer.gameObject.SetActive(false);
        }

        public void UpdatePlayer(int playerID)
        {
            currentPlayer.text = "Player " + playerID;
        }

        public void UpdateRound(int currentRound)
        {
            this.currentRound.text = currentRound.ToString();
        }

        public void UpdateSelectionProgress(int currentProgress, int total)
        {
            this.currentSelectionProgress.text = currentProgress + "/" + total;
        }

        public void GameOver(int id, int rounds)
        {
            gameOverObject.Setup(id, rounds);
        }

    }
} 