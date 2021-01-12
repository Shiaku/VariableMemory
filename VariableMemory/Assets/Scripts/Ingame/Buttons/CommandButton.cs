using UnityEngine;
using UnityEngine.UI;

namespace Ingame.Button
{
    public class CommandButton : MonoBehaviour
    {
        [SerializeField]
        private GameController controller;

        public void Setup(GameController controller)
        {
            this.controller = controller;
        }

        public void OnClick() //Invoked by Button Interaction
        {
            controller.OnClick(ExecuteAction, gameObject);
        }

        protected virtual void ExecuteAction(int executionRound)
        {
            Debug.Log("A Simple Button has been clicked!");
            //Specific implementation for each button class
        }

    }
}
