using System;
using System.Collections.Generic;

namespace GameStructure
{
    namespace Rules
    {
        public class FiveQueueRule : Rule
        {
            //Repeat Check Variables
            private List<Action<int>> queue;
            private int currentIndex = 0;
            private int initialInsertions = 5;

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
                bool initialInsertionConcluded = initialInsertions <= 0;
                if (initialInsertionConcluded)
                    queue.RemoveAt(0);

                queue.Add(command);
                if (!initialInsertionConcluded)
                {
                    initialInsertions--;
                    initialInsertionConcluded = initialInsertions <= 0;
                }

                return initialInsertionConcluded;
            }
            public bool ProcessDeletion(object target, List<Action<int>> queue)
            {
                return true;
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
                return new List<TurnPhase> { TurnPhase.SELECT, TurnPhase.REPEAT };
            }
        }
    }
}
