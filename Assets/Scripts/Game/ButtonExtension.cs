using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game
{
    [RequireComponent(typeof(Button))]
    public class ButtonExtension : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Button _button;
        private Color _originalColor;
        private Vector3 _originalScale;
        // public Color hoverColor = Color.yellow;

        private RectTransform RectTransform => transform as RectTransform;

        private Sequence _hoverSequence;

        void Start()
        {
            _button = GetComponent<Button>();
            _originalColor = _button.image.color;
            _originalScale = RectTransform.localScale;
        }

        /// <summary>
        /// ポインターが入った時の処理
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            // _button.image.color = hoverColor;
            _button.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            _hoverSequence = DOTween.Sequence()
                .Append(_button.targetGraphic.DOColor(Color.magenta, 1f)
                    .SetEase(Ease.InOutQuad)
                    .SetLoops(-1, LoopType.Yoyo));
        }

        /// <summary>
        /// ポインターが外れた時の処理
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            _button.image.color = _originalColor;
            _button.transform.localScale = _originalScale;
            _hoverSequence?.Kill();
            _hoverSequence = null;
        }
    }
}