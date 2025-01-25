using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Game
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Image playerImage;
        [SerializeField] private Bubble bubblePrefab;
        [SerializeField] private RectTransform bubbleParent;

        private readonly List<Bubble> _bubbles = new();

        public void AddBubble(BubbleData bubbleData)
        {
            var bubble = Instantiate(bubblePrefab, bubbleParent);
            bubble.Setup(bubbleData, _bubbles.Count);

            // 規定個数以上は一番古いものから張り替え
            // TODO 張り替え処理にバグあり
            if (_bubbles.Count > GameConst.PlayerBubbleLimit)
            {
                RemoveBubble();
            }
            _bubbles.Add(bubble);
        }

        public void RemoveBubble(int count = 1)
        {
            for (var i = 0; i < count; i++)
            {
                var target = _bubbles.Last();
                if (target == null) return;
                target.Destroy();
                _bubbles.Remove(target);   
            }
        }

        public List<Bubble> GetBubbleData(int bubbleCount)
        {
            return new List<Bubble>(_bubbles.GetRange(_bubbles.Count - bubbleCount, bubbleCount));
        }
    }
}