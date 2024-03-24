using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  public class SoundManager
  {
    private AudioSource bgmSource;
    private Dictionary<string, AudioClip> cachePlayedClips;
    private bool isStop;
    public bool IsStop
    {
      get => isStop;
      //响应界面交互
      set
      {
        isStop = value;
        if (isStop)
        {
          bgmSource.Pause();//暂停播放
        }
        else
        {
          bgmSource.Play();
        }
      }
    }

    private float bgmVolume;//背景音乐音量
    public float BgmVolume
    {
      get => bgmVolume;
      //响应界面交互
      set
      {
        bgmVolume = value;
        bgmSource.volume = bgmVolume;
      }
    }

    private float effectVolume;//音效音量
    public float EffectVolume
    {
      get => effectVolume;
      set
      {
        effectVolume = value;
      }
    }
    public SoundManager()
    {
      cachePlayedClips = new Dictionary<string, AudioClip>();
      bgmSource = GameObject.Find("game").GetComponent<AudioSource>();

      IsStop = false;
      bgmVolume = 1;
      effectVolume = 1;
    }

    public void PlayBGM(string clipName)
    {
      //如果当前音乐已暂停，则不播放
      if (isStop)
        return;

      AudioClip clip;
      if (!cachePlayedClips.TryGetValue(clipName, out clip))
      {
        clip = Resources.Load<AudioClip>($"Sounds/{clipName}");
        cachePlayedClips.Add(clipName, clip);
      }

      bgmSource.clip = clip;
      bgmSource.Play();
    }

  }
}

