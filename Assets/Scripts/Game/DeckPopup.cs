using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Game
{
    public class DeckPopup : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Button modalButton;
        [SerializeField] private CardNumberInfo cardNumberInfoPrefab;
        [SerializeField] private RectTransform cardNumberInfoContainer;

        private readonly List<CardNumberInfo> _cardNumberInfo = new();
        private bool _isEnable;

        public void Setup()
        {
            for (var i = _cardNumberInfo.Count - 1; i >= 0; i--)
            {
                DestroyImmediate(_cardNumberInfo[i].gameObject);
            }

            _cardNumberInfo.Clear();

            var currentDeckList = CardManager.Instance.GetCurrentDeck();
            var stackCardList = CardManager.Instance.GetStackCardList();

            foreach (var validCard in CardManager.Instance.GetValidCardList())
            {
                var maxCount = currentDeckList.Count(x => validCard.cardName.Equals(x.cardName));
                var currentCount = stackCardList.Count(x => validCard.cardName.Equals(x.cardName));
                var cardNumberInfo = Instantiate(cardNumberInfoPrefab, cardNumberInfoContainer);
                cardNumberInfo.Setup(validCard, currentCount, maxCount);
                _cardNumberInfo.Add(cardNumberInfo);
            }

            modalButton.onClick.RemoveAllListeners();
            modalButton.onClick.AddListener(ModalButtonEvent);

            SetEnable(true);
        }

        public void SetEnable(bool isEnable)
        {
            _isEnable = isEnable;
            canvasGroup.alpha = isEnable ? 1 : 0;
            canvasGroup.interactable = isEnable;
            canvasGroup.blocksRaycasts = isEnable;
        }

        private void ModalButtonEvent()
        {
            SoundManager.Instance.PlaySe(GameConst.SeType.Decision31);
            SetEnable(false);
        }
    }
}