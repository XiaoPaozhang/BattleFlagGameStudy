using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  public class SoundManager
  {
    private AudioSource bgmSource;
    private Dictionary<string, AudioClip> clips;
    public SoundManager()
    {
      clips = new Dictionary<string, AudioClip>();
      bgmSource = GameObject.Find("game").GetComponent<AudioSource>();
    }

    public void PlayBGM(string clipName)
    {
      AudioClip clip;
      if (!clips.TryGetValue(clipName, out clip))
      {
        clip = Resources.Load<AudioClip>($"Sounds/{clipName}");
        clips.Add(clipName, clip);
      }

      bgmSource.clip = clip;
      bgmSource.Play();
    }

  }
}

