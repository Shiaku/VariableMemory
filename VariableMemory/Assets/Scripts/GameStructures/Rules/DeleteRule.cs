using System;
using System.Collections.Generic;

namespace GameStructure
{
    namespace Rules
    {
        public class DeleteRule : Rule
        {
            //Repeat Check Variables
            private List<Action<int>> queue;
            private int currentIndex = 0;

            //Callbacks
            private Action<int> onChangePhase;
            private Action<int> onRepeatAction;
            private Action<int> onFailure;

            public void Setup(Action<int> onChangePhase, Action<int> onFailure, Action<int> onRepeatAction)
            {
                this.onChangePhase = onChangePhase;
                this.onFailure = onFailure;
                this.onRepeatAction = onRepeatAction;
            }

            public void OnTurnStart(List<Action<int>> queue)
            {
                this.queue = queue;
                currentIndex = 0;
            }

            public bool ProcessSelection(Action<int> command, List<Action<int>> queue)
            {
                queue.Add(command);
                return true;
            }

            public bool ProcessDeletion(object target, List<Action<int>> queue)
            {
                bool hasDifferentButtonStep = false;
                foreach (Action<int> input in queue)
                {
                    if (input.Target != target) //If there's a single different button input, it's ok to delete this button.
                    {
                        hasDifferentButtonStep = true;
                        break;
                    }
                }
                if (hasDifferentButtonStep)
                    onChangePhase(0);

                return hasDifferentButtonStep;
            }

            public void ValidateInput(Action<int> command)
            {
                if (queue != null && currentIndex < queue.Count && command == queue[currentIndex])
                {
                    currentIndex++;
                    if (currentIndex < queue.Count)
                    {
                        onRepeatAction(currentIndex);
                    }
                    else
                    {
                        onChangePhase(0);
                    }
                }
                else
                {
                    onFailure(queue.Count);
                }
            }

            public List<TurnPhase> AvailableTurnPhases()
            {
                return new List<TurnPhase> {TurnPhase.SELECT, TurnPhase.REPEAT, TurnPhase.DELETE};
            }
        }
    }
}