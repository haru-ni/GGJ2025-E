using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Game
{
    [Serializable]
    public class BubbleData
    {
        public int power;
        public GameConst.ColorType color;
    }

    public class Bubble : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image colorBubbleImage;
        [SerializeField] private List<Sprite> colorBubbleSprite;

        [SerializeField] private RectTransform informationContainer;
        [SerializeField] private Image starPrefab;
        [SerializeField] private List<Sprite> startSprites = new();

        private BubbleData _bubbleData;
        private int _layer;
        private bool _enable;

        public void Setup(BubbleData bubbleData, int layer, bool enable = false)
        {
            _bubbleData = bubbleData;
            _layer = layer;
            _enable = enable;

            SetEnable(_enable);
            colorBubbleImage.sprite = colorBubbleSprite[(int)bubbleData.color];
            colorBubbleImage.SetNativeSize();

            for (var i = 0; i < bubbleData.power; i++)
            {
                var star = Instantiate(starPrefab, informationContainer);
                star.sprite = startSprites[(int)bubbleData.color];
            }
        }

        public BubbleData GetBubbleData()
        {
            return _bubbleData;
        }

        public void SetEnable(bool enable)
        {
            canvasGroup.alpha = enable ? 1 : 0;
        }
    }
}