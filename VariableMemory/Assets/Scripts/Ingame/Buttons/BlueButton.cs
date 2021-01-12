using UnityEngine;
namespace Ingame.Button
{
    public class BlueButton : CommandButton
    {
        protected override void ExecuteAction(int executionRound)
        {
            Debug.Log("A Blue Button has been clicked on selection turn <" + executionRound + ">!");
        }
    }
}
