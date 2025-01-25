using System;
using ScriptableObjects.Card;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class HandCard : Card
    {
        [SerializeField] private Button button;
        [SerializeField] private CanvasGroup canvasGroup;

        private bool _enable;
        private Action _playCardAction;

        public override void Setup(CardDefinition cardDefinition)
        {
            base.Setup(cardDefinition);
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(Play);
            SetEnable(true);
        }

        public void SetPlayCardAction(Action playCardAction)
        {
            _playCardAction = playCardAction;
        }

        public void Play()
        {
            if (!IsEnable()) return;

            InGameManager.Instance.PlayCard(this);
            _playCardAction?.Invoke();
        }

        public bool IsEnable()
        {
            return _enable;
        }

        public void SetEnable(bool enable)
        {
            _enable = enable;
            canvasGroup.alpha = enable ? 1 : 0;
        }

        public void Destroy()
        {
            DestroyImmediate(gameObject);
        }
    }
}