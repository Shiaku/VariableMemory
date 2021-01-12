using System;
using System.Collections.Generic;
using UnityEngine;

using GameStructure;
using Ingame.HUD;

namespace Ingame
{
    public class GameController : MonoBehaviour
    {
        //Game Related Variables
        private List<Action<int>> queue = new List<Action<int>>();
        private List<TurnPhase> turnPhases;
        private List<int> players = new List<int>();
        private int rounds = 0;
        private GameObject lastDeletedButton = null;
        private object deletedButtonReference = null;

        //Indexes and Semaphores
        private int phaseID = 0;
        private int currentPlayerID = 0;
        private bool isRepeating = false; //Used to differentiate the first repeat moment
        private int currentRepeat = 0;

        //Controller references
        [SerializeField]
        private HUDController hud;
        [SerializeField]
        private RectTransform buttonHolder;

        private Rule rule;

        private void Start() //Invoked by Game Engine
        {
            rule = Game.Instance.rule;
            rule.Setup(OnChangePhase, OnPlayerFailure, OnRepeatAction);
            Game.Instance.builder.Build(this, buttonHolder);
            turnPhases = rule.AvailableTurnPhases();
            for(int i = 0; i < Game.Instance.numberOfPlayers; i++)
            {
                players.Add(i + 1);
            }
            rounds = 1;
            SetupPhase(turnPhases[0]);
        }

        public void OnClick(Action<int> buttonCallback, GameObject buttonObject)
        {
            TurnPhase currentPhase = turnPhases[phaseID];
            switch (currentPhase)
            {
                case TurnPhase.SELECT:
                {
                    if (rule.ProcessSelection(buttonCallback, queue))
                    {
                        OnChangePhase(0);
                    }
                    break;
                }
                case TurnPhase.REPEAT:
                {
                    buttonCallback(currentRepeat + 1);
                    rule.ValidateInput(buttonCallback);
                    break;
                }
                case TurnPhase.DELETE:
                {
                    if(rule.ProcessDeletion(buttonCallback.Target, queue))
                    {
                        if(lastDeletedButton != null)
                            lastDeletedButton.SetActive(true);
                        lastDeletedButton = buttonObject;
                        lastDeletedButton.SetActive(false);
                        deletedButtonReference = buttonCallback.Target;
                    }
                    break;
                }
            }
        }

        private void OnRepeatAction(int previousExecutions)
        {
            currentRepeat = previousExecutions;
            SetupPhase(turnPhases[phaseID]);
        }

        private void OnChangePhase(int reason)
        {
            UpdateHUDBeforePhaseChange();
            phaseID++;
            if (phaseID >= turnPhases.Count)
            {
                phaseID = 0;
                rounds++;
                hud.UpdateRound(rounds);
                isRepeating = false;
                currentPlayerID++;
                if (currentPlayerID >= players.Count)
                    currentPlayerID = 0;

                hud.UpdatePlayer(players[currentPlayerID]);
            }

            SetupPhase(turnPhases[phaseID]);
        }

        private void UpdateHUDBeforePhaseChange()
        {
            TurnPhase currentPhase = turnPhases[phaseID];
            switch (currentPhase)
            {
                case TurnPhase.SELECT:
                {
                    break;
                }
                case TurnPhase.REPEAT:
                {
                    hud.UpdateSelectionProgress(++currentRepeat, queue.Count);
                    break;
                }
                case TurnPhase.DELETE:
                {
                    break;
                }
            }
        }

        private void SetupPhase(TurnPhase phase)
        {
            switch (phase)
            {
                case TurnPhase.SELECT:
                {
                    hud.AnnounceSelect();
                    break;
                }
                case TurnPhase.REPEAT:
                {
                    if (!isRepeating)
                    {
                        hud.AnnounceRepeat();
                        //Setups the Repeat turn with the proper action Queue given the situation.
                        if (lastDeletedButton == null)
                            rule.OnTurnStart(queue);
                        else
                            rule.OnTurnStart(UpdatedDeletedQueue(queue, deletedButtonReference));

                        isRepeating = true;
                        currentRepeat = 0;
                    }
                    hud.UpdateSelectionProgress(currentRepeat, queue.Count);
                    break;
                }
                case TurnPhase.DELETE:
                {
                    hud.AnnounceDelete();
                    break;
                }
            }
        }

        private void OnPlayerFailure(int step)
        {
            if (players.Count > 1) //If Multiplayer Game
            {
                players.RemoveAt(currentPlayerID);
                //HUD could announce player defeat
                Debug.Log(players.Count + " Players Left.");
                if (players.Count <= 1)
                    OnGameOver(rounds); //The one left is the winner.
                else
                {
                    phaseID = turnPhases.Count - 1; //Finish this player's turn
                    OnChangePhase(0);
                }
            }
            else
                OnGameOver(rounds);
        }

        private void OnGameOver(int rounds)
        {
            hud.GameOver(players[0], rounds);
        }

        private List<Action<int>> UpdatedDeletedQueue(List<Action<int>> queue, object lastDeletedButton)
        {
            if (queue.Count <= 1)
                return queue;

            List<Action<int>> updatedQueue = new List<Action<int>>();
            foreach(Action<int> input in queue)
            {
                if (input.Target == lastDeletedButton)
                    continue;
                updatedQueue.Add(input);
            }

            return updatedQueue;
        }
        #region Game Over Button Calls

        //Game Over Calls
        public void Restart()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
        }

        public void LoadMainMenu()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }

        #endregion
    }
}
