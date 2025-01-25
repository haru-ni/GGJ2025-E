using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Meta
{
    public class TitleWindowManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Button startButton;
        [SerializeField] private Button optionButton;

        private bool _isEnable;

        public void Setup()
        {
            SoundManager.Instance.PlayBgm(GameConst.BgmType.PixelLoveInTokyo);

            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(OnStartButtonClicked);

            optionButton.onClick.RemoveAllListeners();
            optionButton.onClick.AddListener(OnOptionButtonClicked);
        }

        public void SetEnable(bool isEnable)
        {
            _isEnable = isEnable;
            canvasGroup.alpha = isEnable ? 1 : 0;
            canvasGroup.interactable = isEnable;
            canvasGroup.blocksRaycasts = isEnable;
        }

        /// <summary>
        /// スタートボタン押下時処理
        /// </summary>
        private static void OnStartButtonClicked()
        {
            SoundManager.Instance.PlaySe(GameConst.SeType.Decision31);
            GameManager.Instance.TitleToInGame();
        }

        /// <summary>
        /// オプションボタン押下時処理
        /// </summary>
        private static void OnOptionButtonClicked()
        {
            SoundManager.Instance.PlaySe(GameConst.SeType.Decision31);
        }
    }
}