using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game
{
    public class ButtonExtension : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Button _button;
        private Color _originalColor;
        public Color hoverColor = Color.yellow; // マウスオーバー時の色

        void Start()
        {
            _button = GetComponent<Button>();
            _originalColor = _button.image.color; // 元の色を保存
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _button.image.color = hoverColor; // マウスオーバー時の色に変更
            _button.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f); // スケールを大きくする
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _button.image.color = _originalColor; // 元の色に戻す
            _button.transform.localScale = Vector3.one; // 元のスケールに戻す
        }
    }
}