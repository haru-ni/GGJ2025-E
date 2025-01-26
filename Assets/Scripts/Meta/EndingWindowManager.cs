using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game;
using Spine.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Sequence = DG.Tweening.Sequence;

namespace Meta
{
    public class EndingWindowManager : MonoBehaviour
    {
        private const float GalleryTimeSeconds = 90f;
        private const float Interval = 3.5f;
        private const float SpineAnimationTimeSeconds = 30f;
        private const float StaffRollTimeSeconds = 30f;

        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private List<AnimationList> playerSDAnimationList;
        [SerializeField] private Image playerSDImage;
        [SerializeField] private Image galleryImage;
        [SerializeField] private List<Sprite> gallerySprites;
        [SerializeField] private TextMeshProUGUI staffRollText;
        [SerializeField] private List<string> staffRollStringList;
        [SerializeField] private SkeletonGraphic playerShieldSpine;
        [SerializeField] private SkeletonGraphic battleEffectSpine;

        private bool _isEnable;
        private Sequence _characterSDAnimationSequence;
        private Sequence _gallerySequence;
        private Sequence _spineAnimationSequence;
        private Sequence _staffRollSequence;
        private int _gallerySpriteIndex;
        private int _staffRollStringIndex;

        public void Setup()
        {
            SoundManager.Instance.PlayBgm(GameConst.BgmType.Ending);

            // SDキャラアニメーション
            _characterSDAnimationSequence = DOTween.Sequence();
            foreach (var element in playerSDAnimationList)
            {
                _characterSDAnimationSequence.AppendCallback(() => playerSDImage.sprite = element.sprite)
                    .AppendInterval(element.duration);
            }

            _characterSDAnimationSequence.SetLoops(-1, LoopType.Restart).Play();

            _ = EndingAnimation();
        }

        private void ContentDisable()
        {
            galleryImage.gameObject.SetActive(false);
            staffRollText.gameObject.SetActive(false);
            playerShieldSpine.gameObject.SetActive(false);
            battleEffectSpine.gameObject.SetActive(false);
        }

        private async UniTask EndingAnimation()
        {
            await GalleryAnimation();
            await UniTask.WaitForSeconds(Interval);
            await SpineAnimation();
            await UniTask.WaitForSeconds(Interval);
            await StaffRoll();
        }

        private async UniTask GalleryAnimation()
        {
            _gallerySpriteIndex = 0;
            ContentDisable();
            galleryImage.gameObject.SetActive(true);
            var oneGallerySeconds = GalleryTimeSeconds / gallerySprites.Count;

            var fadeinDuration = oneGallerySeconds * 0.3f;
            var fadeOutDuration = oneGallerySeconds * 0.3f;
            var enableDuration = oneGallerySeconds * 0.2f;
            var disableDuration = oneGallerySeconds * 0.2f;

            _gallerySequence = DOTween.Sequence()
                .AppendCallback(ChangeSprite)
                .Append(galleryImage.DOFade(1f, fadeinDuration).SetEase(Ease.InOutSine))
                .AppendInterval(enableDuration)
                .Append(galleryImage.DOFade(0f, fadeOutDuration).SetEase(Ease.InOutSine))
                .AppendInterval(disableDuration).AppendCallback(() =>
                {
                    if (_gallerySpriteIndex > gallerySprites.Count - 1) _gallerySequence.Complete();
                });
            _gallerySequence.SetLoops(gallerySprites.Count, LoopType.Restart);
            await _gallerySequence;
            ContentDisable();
        }

        private async UniTask SpineAnimation()
        {
            ContentDisable();
            playerShieldSpine.gameObject.SetActive(true);
            battleEffectSpine.gameObject.SetActive(true);
            const float spineInterval = 1f;
            var battleEffectList =
                Enum.GetValues(typeof(GameConst.BattleEffect)).Cast<GameConst.BattleEffect>().ToList();
            var playerShieldList =
                Enum.GetValues(typeof(GameConst.PlayerShield)).Cast<GameConst.PlayerShield>().ToList();

            _spineAnimationSequence = DOTween.Sequence();
            foreach (var element in battleEffectList)
            {
                _spineAnimationSequence.AppendCallback(() =>
                        StartSpine(battleEffectSpine, GameConst.BattleEffectDictionary[element]))
                    .AppendInterval(spineInterval + GameConst.BattleEffectTime[element]);
            }

            foreach (var element in playerShieldList)
            {
                _spineAnimationSequence.AppendCallback(() =>
                        StartSpine(playerShieldSpine, GameConst.PlayerShieldDictionary[element]))
                    .AppendInterval(spineInterval + GameConst.PlayerShieldTime[element]);
            }

            await _spineAnimationSequence;
            ContentDisable();
        }

        private void StartSpine(SkeletonGraphic skeleton, string animationName)
        {
            skeleton.AnimationState.SetAnimation(0, animationName, false);
        }

        private async UniTask StaffRoll()
        {
            _staffRollStringIndex = 0;
            ContentDisable();
            staffRollText.gameObject.SetActive(true);
            var oneGallerySeconds = StaffRollTimeSeconds / staffRollStringList.Count;

            var fadeinDuration = oneGallerySeconds * 0.3f;
            var fadeOutDuration = oneGallerySeconds * 0.3f;
            var enableDuration = oneGallerySeconds * 0.2f;
            var disableDuration = oneGallerySeconds * 0.2f;

            _staffRollSequence = DOTween.Sequence()
                .AppendCallback(ChangeText)
                .Append(staffRollText.DOFade(1f, fadeinDuration).SetEase(Ease.InOutSine))
                .AppendInterval(enableDuration)
                .Append(staffRollText.DOFade(0f, fadeOutDuration).SetEase(Ease.InOutSine))
                .AppendInterval(disableDuration)
                .AppendCallback(() =>
                {
                    if (_staffRollStringIndex > staffRollStringList.Count - 1) _staffRollSequence.Complete();
                });
            _staffRollSequence.SetLoops(gallerySprites.Count, LoopType.Restart);
            await _staffRollSequence;
            ContentDisable();
        }

        private void ChangeSprite()
        {
            galleryImage.sprite = gallerySprites[_gallerySpriteIndex];
            galleryImage.SetNativeSize();
            _gallerySpriteIndex++;
        }

        private void ChangeText()
        {
            staffRollText.text = staffRollStringList[_staffRollStringIndex];
            staffRollText.parseCtrlCharacters = false;
            staffRollText.ForceMeshUpdate(true, true);
            staffRollText.parseCtrlCharacters = true;
            staffRollText.ForceMeshUpdate(true, true);
            _staffRollStringIndex++;
        }

        public void SetEnable(bool isEnable)
        {
            _isEnable = isEnable;
            canvasGroup.alpha = isEnable ? 1 : 0;
            canvasGroup.interactable = isEnable;
            canvasGroup.blocksRaycasts = isEnable;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                SetEnable(!_isEnable);
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                _ = GalleryAnimation();
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                _ = StaffRoll();
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                _ = SpineAnimation();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                Setup();
            }
        }
    }
}