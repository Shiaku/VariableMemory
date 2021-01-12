using Ingame;
using UnityEngine;
namespace GameStructure
{
    public interface Builder
    {
        void Build(GameController controller, RectTransform holder);
    }
}
