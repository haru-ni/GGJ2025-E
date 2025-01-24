using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private Image image;
        public RectTransform RectTransform => transform as RectTransform;

        public void SetImage(Sprite sprite)
        {
            image.sprite = sprite;
        }
    }
}
