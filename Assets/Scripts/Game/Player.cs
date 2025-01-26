using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Game
{
    [Serializable]
    public class AnimationList
    {
        public Sprite sprite;
        public float duration;
    }

    public class Player : MonoBehaviour
    {
        [SerializeField] private List<AnimationList> playerSDAnimationList;
        [SerializeField] private Image playerSDImage;
        [SerializeField] private List<Bubble> bubbleList;

        private Sequence _playerAnimationSequence;
        private int _bubbleCount;

        public void Setup()
        {
            // SDキャラアニメーション
            var sequence = DOTween.Sequence();
            foreach (var element in playerSDAnimationList)
            {
                sequence.AppendCallback(() => playerSDImage.sprite = element.sprite).AppendInterval(element.duration);
            }

            sequence.SetLoops(-1, LoopType.Restart).Play();

            foreach (var element in bubbleList)
            {
                element.SetEnable(false);
            }
        }

        public void AddBubble(BubbleData bubbleData)
        {
            // 規定個数以上は一番古いものから張り替え
            if (_bubbleCount >= GameConst.PlayerBubbleLimit)
            {
                RemoveBubble();
            }

            bubbleList[_bubbleCount].Setup(bubbleData, _bubbleCount, true);
            _bubbleCount++;
        }

        public void RemoveBubble(int count = 1)
        {
            for (var i = 0; i < count; i++)
            {
                _bubbleCount--;
                bubbleList[_bubbleCount].SetEnable(false);
            }
        }

        public List<Bubble> GetBubbleData(int bubbleCount)
        {
            return new List<Bubble>(bubbleList.Take(bubbleCount));
            // return new List<Bubble>(bubbleList.GetRange(bubbleList.Count - bubbleCount, bubbleCount));
        }
    }
}