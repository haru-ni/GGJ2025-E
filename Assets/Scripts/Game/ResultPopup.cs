using System;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Game
{
    public class ResultPopup : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Button modalButton;

        [SerializeField] private RectTransform winResult;
        [SerializeField] private RectTransform loseResult;

        private bool _isEnable;
        private Action _modalCallback;

        public void Setup(bool isWin, Action winCallback, Action loseCallback)
        {
            if (isWin)
            {
                winResult.gameObject.SetActive(true);
                loseResult.gameObject.SetActive(false);
                SoundManager.Instance.PlaySe(GameConst.SeType.Decision24);
                SoundManager.Instance.PlaySe(GameConst.SeType.VoiceWin);
                _modalCallback = winCallback;
                modalButton.onClick.RemoveAllListeners();
                modalButton.onClick.AddListener(ModalButtonEvent);
            }
            else
            {
                winResult.gameObject.SetActive(false);
                loseResult.gameObject.SetActive(true);
                SoundManager.Instance.PlaySe(GameConst.SeType.VoiceLose);
                _modalCallback = loseCallback;
                modalButton.onClick.RemoveAllListeners();
                modalButton.onClick.AddListener(ModalButtonEvent);
            }

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
            _modalCallback?.Invoke();
        }
    }
}