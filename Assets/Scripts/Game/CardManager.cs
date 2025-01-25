using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Card;
using ScriptableObjects.CardPack;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Random = UnityEngine.Random;

namespace Game
{
    public class CardManager : Singleton<CardManager>
    {
        [SerializeField] private CardPackDefinition initialDeck;
        [SerializeField] private List<CardPackDefinition> cardPackDefinitions = new();

        [SerializeField] private Card cardPrefab;
        [SerializeField] private HandCard handCardPrefab;

        [SerializeField] private RectTransform handCardContainer;
        [SerializeField] private Button stackCardButton;
        [SerializeField] private TextMeshProUGUI stackCardCountText;

        private readonly List<HandCard> _handCards = new();
        private List<CardDefinition> _stackCards = new();

        public void Setup()
        {
            // TODO 所持デッキ情報を山札に読み込み
            _stackCards = new List<CardDefinition>(initialDeck.cards);

            foreach (var handCard in _handCards)
            {
                handCard.Destroy();
            }

            _handCards.Clear();

            for (var i = 0; i < GameConst.HandCardCount; i++)
            {
                var handCard = Instantiate(handCardPrefab, handCardContainer);
                _handCards.Add(handCard);
            }

            var needDrawCardNumber = _handCards.Count(x => !x.IsEnable());
            DrawCard(needDrawCardNumber);
        }

        private void StackCardEvent()
        {
            // TODO 山札情報表示用のイベント
        }

        /// <summary>
        /// 山札から手札にカードを引く
        /// 手札を置ける場所を左から走査して一番最初に見つけたところに引く
        /// </summary>
        /// <returns></returns>
        private void DrawCard(int number = 1)
        {
            for (var i = 0; i < number; i++)
            {
                if (_stackCards.Count <= 0)
                {
                    // TODO 山札がなくなった時の処理
                    Debug.LogError($"山札がなくなりました！");
                    return;
                }

                var drawCardDefinition = _stackCards[Random.Range(0, _stackCards.Count)];
                _stackCards.Remove(drawCardDefinition);

                var targetHandCard = _handCards.FirstOrDefault(x => !x.IsEnable());
                if (targetHandCard == null)
                {
                    Debug.LogAssertion($"手札にドロー先にできる箇所がありませんでした！：{i}/{number}　のドロー");
                    return;
                }

                targetHandCard.Setup(drawCardDefinition);
                targetHandCard.SetPlayCardAction(CardPlayEvent);
            }
        }

        private void CardPlayEvent()
        {
            // 手札がなくなったら補充する
            if (_handCards.Count(x => x.IsEnable()) > 0) return;
            DrawCard(_handCards.Count(x => !x.IsEnable()));
        }
    }
}