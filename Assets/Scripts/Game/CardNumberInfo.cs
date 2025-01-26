using System.Collections.Generic;
using ScriptableObjects.Card;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class CardNumberInfo : MonoBehaviour
    {
        [SerializeField] private List<Image> cardImage;
        [SerializeField] private TextMeshProUGUI numberText;

        public void Setup(CardDefinition cardDefinition, int currentCount, int maxCount)
        {
            var displayCount = cardImage.Count;
            for (var i = 0; i < displayCount; i++)
            {
                var target = cardImage[i];
                target.sprite = cardDefinition.sprite;
                target.SetNativeSize();
                target.gameObject.SetActive(currentCount > i);
            }

            numberText.SetText($"{currentCount}/{maxCount}");
        }
    }
}