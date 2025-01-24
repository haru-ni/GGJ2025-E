using TMPro;
using UnityEngine;

namespace Game
{
    public class BehaviorDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI behaviourText;

        private BubbleData _bubbleData;

        public void Setup(BubbleData bubbleData)
        {
            _bubbleData = bubbleData;

            behaviourText.SetText(_bubbleData.power.ToString());
            behaviourText.color = bubbleData.GetColor();
        }
    }
}