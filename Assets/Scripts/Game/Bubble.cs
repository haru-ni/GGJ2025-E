using System;
using TMPro;
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

        public Color GetColor()
        {
            return color switch
            {
                GameConst.ColorType.Red => Color.red,
                GameConst.ColorType.Blue => Color.blue,
                GameConst.ColorType.Purple => Color.magenta,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    public class Bubble : MonoBehaviour
    {
        private const float FirstScale = 1f;
        private const float ScaleMultiplier = 1.5f;

        [SerializeField] private Image bubbleImage;
        [SerializeField] private TextMeshProUGUI bubbleText;

        private BubbleData _bubbleData;
        private int _layer;

        public void Setup(BubbleData bubbleData, int layer)
        {
            _bubbleData = bubbleData;
            _layer = layer;

            var color = _bubbleData.GetColor();

            bubbleText.SetText(_bubbleData.power.ToString());
            bubbleText.color = color;

            var scale = Mathf.Pow(ScaleMultiplier, _layer) * FirstScale;
            bubbleImage.rectTransform.localScale = new Vector3(scale, scale, scale);
            bubbleImage.color = color;
        }

        public BubbleData GetBubbleData()
        {
            return _bubbleData;
        }

        public void Destroy()
        {
            DestroyImmediate(gameObject);
        }
    }
}