using UnityEngine;
using UnityEngine.UI;

namespace Ingame.HUD
{
    public class Announcer : MonoBehaviour
    {
        [SerializeField]
        private Text text;

        public void SetupText(string phaseName)
        {
            text.text = "Starting " + phaseName + " Phase";
        }
    }
}
