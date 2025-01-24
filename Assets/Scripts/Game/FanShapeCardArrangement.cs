using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class FanShapeCardArrangement : MonoBehaviour
    {
        private const float AngleStart = -80f;
        private const float AngleEnd = 80f;
        private const float DistanceFromCenter = 200f;

        [SerializeField] private Card cardPrefab;

        private int NumberOfCards => _cards.Count;
        private RectTransform RectTransform => transform as RectTransform;
        private Vector2 CenterPoint => RectTransform.anchoredPosition;

        private readonly List<Card> _cards = new();


        void Start()
        {
            ArrangeCardsInFanShape();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                AddCard();
                ArrangeCardsInFanShape();
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                RemoveCard(_cards.First());
                ArrangeCardsInFanShape();
            }
        }

        /// <summary>
        /// カードを保持リストに追加
        /// </summary>
        public void AddCard()
        {
            var card = Instantiate(cardPrefab, transform);
            var cardRectTransform = card.GetComponent<Card>();
            _cards.Add(cardRectTransform);
            ArrangeCardsInFanShape();
        }

        /// <summary>
        /// カードを保持リストから削除
        /// </summary>
        /// <param name="card"></param>
        public void RemoveCard(Card card)
        {
            if (!_cards.Contains(card)) return;
            _cards.Remove(card);
            Destroy(card.gameObject);
            ArrangeCardsInFanShape();
        }

        /// <summary>
        /// 保持しているカードを扇形に並べる
        /// </summary>
        private void ArrangeCardsInFanShape()
        {
            var angleStep = (AngleEnd - AngleStart) / (NumberOfCards - 1); // 各カードの間の角度
            var currentAngle = AngleStart + 90f;

            for (var i = 0; i < NumberOfCards; i++)
            {
                // カードの位置を計算
                var position = CalculateCardPosition(currentAngle);
                _cards[i].RectTransform.anchoredPosition = position;

                // カードの回転を設定（必要に応じて）
                _cards[i].RectTransform.rotation = Quaternion.Euler(0, 0, currentAngle);

                // 次の角度に進む
                currentAngle += angleStep;
            }
        }

        /// <summary>
        /// 座標を計算
        /// </summary>
        /// <param name="angle">角度</param>
        /// <returns>座標</returns>
        private Vector2 CalculateCardPosition(float angle)
        {
            // ラジアンに変換
            var radian = angle * Mathf.Deg2Rad;

            // 基準点からの距離を考慮して位置を計算
            var x = Mathf.Cos(radian) * DistanceFromCenter;
            var y = Mathf.Sin(radian) * DistanceFromCenter;

            return CenterPoint + new Vector2(x, y); // 基準点を加算
        }
    }
}