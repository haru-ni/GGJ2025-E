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

        public override void Setup(CardDefinition cardDefinition)
        {
            base.Setup(cardDefinition);
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(Play);
            SetEnable(true);
        }

        public void Play()
        {
            if (!IsEnable()) return;
            SetEnable(false);
            // TODO 効果発動
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