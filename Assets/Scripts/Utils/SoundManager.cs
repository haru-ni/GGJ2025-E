using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Utils
{
    public class SoundManager : Singleton<SoundManager>
    {
        [SerializeField] private AudioSource bgmAudioSource;
        [SerializeField] private List<AudioSource> seAudioSources;

        /// <summary>
        /// SE再生
        /// </summary>
        /// <param name="seType">SEのタイプ</param>
        public void PlaySe(GameConst.SeType seType)
        {
            if (!GameConst.SePathDictionary.TryGetValue(seType, out var value))
            {
                Debug.LogError($"指定されたSEのパスの定義がありません：{seType.ToString()}");
                return;
            }

            var se = Resources.Load<AudioClip>(value);
            if (se == null)
            {
                Debug.LogError($"指定されたパスにSEが見つかりません！{value}");
                return;
            }

            var targetAudioSource = seAudioSources.FirstOrDefault(x => !x.isPlaying);
            if (targetAudioSource == null)
            {
                Debug.LogAssertion($"あいているAudioSourceがありません：{seAudioSources.ToList()}個全て使用中");
                return;
            }

            targetAudioSource.clip = se;
            targetAudioSource.Play();
        }

        /// <summary>
        /// BGM再生
        /// </summary>
        /// <param name="bgmType">BGMのタイプ</param>
        /// <param name="force">すでに再生中だったときに強制的に変えるかどうか</param>
        public void PlayBgm(GameConst.BgmType bgmType, bool force = true)
        {
            if (!GameConst.BgmPathDictionary.TryGetValue(bgmType, out var value))
            {
                Debug.LogError($"指定されたBGMのパスの定義がありません：{bgmType.ToString()}");
                return;
            }

            var bgm = Resources.Load<AudioClip>(value);
            if (bgm == null)
            {
                Debug.LogError($"指定されたパスにBGMが見つかりません！{value}");
                return;
            }

            if (bgmAudioSource.isPlaying && !force)
            {
                Debug.LogAssertion($"既にBGMが再生中のため再生をしませんでした");
                return;
            }

            bgmAudioSource.Stop();
            bgmAudioSource.clip = bgm;
            bgmAudioSource.Play();
        }
    }
}