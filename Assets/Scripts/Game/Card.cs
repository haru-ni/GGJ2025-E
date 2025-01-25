using ScriptableObjects.Card;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game
{
    public class Card : MonoBehaviour
    {
        [SerializeField] protected Image cardImage;
        [SerializeField] protected TextMeshProUGUI powerText;
        public RectTransform RectTransform => transform as RectTransform;

        public CardDefinition cardDefinition;

        public virtual void Setup(CardDefinition cardData)
        {
            cardDefinition = cardData;
            cardImage.sprite = cardData.sprite;
            cardImage.color = cardData.bubbleData.GetColor();
            powerText.SetText($"{cardData.bubbleData.power}");
        }
    }
}