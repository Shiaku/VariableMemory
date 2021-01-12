using UnityEngine;
namespace Ingame.Button
{
    public class RedButton : CommandButton
    {
        protected override void ExecuteAction(int executionRound)
        {
            Debug.Log("A Red Button has been clicked on selection turn <" + executionRound + ">!");
        }
    }
}
