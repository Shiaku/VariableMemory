using System.Collections.Generic;
using UnityEngine;
using Ingame;
using Ingame.Button;

namespace GameStructure.Builders
{
    public class SquareBuilder : Builder
    {
        private Vector3[] positions =
        {
            new Vector3(-150, 150, 0),
            new Vector3(150, 150, 0),
            new Vector3(150, -150, 0),
            new Vector3(-150, -150, 0)
        };
        private List<GameObject> buttons;
        private int index = 0;

        public void Build(GameController controller, RectTransform holder)
        {
            buttons = new List<GameObject>();

            buttons.Add((GameObject)Resources.Load("GreenButton"));
            buttons.Add((GameObject)Resources.Load("RedButton"));

            foreach (Vector3 position in positions)
            {
                CommandButton button = GameObject.Instantiate(GetNextObject()).GetComponent<CommandButton>();
                button.transform.parent = holder;
                button.transform.localPosition = position;
                button.Setup(controller);
                index++;
            }
        }

        private GameObject GetNextObject()
        {
            if (index >= buttons.Count)
                index = 0;
            return buttons[index];
        }
    }
}