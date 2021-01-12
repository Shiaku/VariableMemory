using System;
using System.Collections.Generic;

namespace GameStructure
{
    public interface Rule
    {
        void Setup(Action<int> OnChangePhase, Action<int> onFailure, Action<int> onRepeatAction); //Sets all requires callbacks
        void OnTurnStart(List<Action<int>> queue); //Setups the queue for the new turn according to the rule.
        bool ProcessSelection(Action<int> command, List<Action<int>> queue); //Validates if the input can be queued. if it can, it is.
        bool ProcessDeletion(object target, List<Action<int>> queue);
        void ValidateInput(Action<int> command); //Validates if the input was right
        List<TurnPhase> AvailableTurnPhases(); //Returns a list of all turn phases present in the specific rule.
    }

    public enum TurnPhase
    {
        SELECT,
        REPEAT,
        DELETE
    }
}
