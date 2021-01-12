using UnityEngine;
namespace Ingame.Button
{
    public class GreenButton : CommandButton
    {
        protected override void ExecuteAction(int executionRound)
        {
            Debug.Log("A Green Button has been clicked on selection turn <" + executionRound + ">!");
        }
    }
}
