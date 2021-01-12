using System.Collections.Generic;
using UnityEngine;
using Ingame;
using Ingame.Button;

namespace GameStructure.Builders
{
    public class HexagonBuilder : Builder
    {
        protected Vector3[] positions = 
        {
            new Vector3(115, 190, 0),
            new Vector3(-115, 190, 0),
            new Vector3(-270, 0, 0),
            new Vector3(270, 0, 0),
            new Vector3(115, -190, 0),
            new Vector3(-115, -190, 0)
        };
        private List<GameObject> buttons;
        private int index = 0;

        public void Build(GameController controller, RectTransform holder)
        {
            buttons = new List<GameObject>();
            
            buttons.Add((GameObject)Resources.Load("GreenButton"));
            buttons.Add((GameObject)Resources.Load("RedButton"));
            buttons.Add((GameObject)Resources.Load("BlueButton"));

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
