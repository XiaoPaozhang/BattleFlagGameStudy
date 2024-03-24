using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BattleFlagGameStudy
{
  /// <summary>
  /// 设置音量面板
  /// </summary>
  public class SetView : BaseView
  {
    protected override void OnAwake()
    {
      base.OnAwake();

      //添加监听
      Find<Button>("bg/closeBtn").onClick.AddListener(OnCloseBtn);
      Find<Toggle>("bg/IsOpnSound").onValueChanged.AddListener(OnIsStopBtn);
      Find<Slider>("bg/soundCount").onValueChanged.AddListener(OnSliderBgmBtn);
      Find<Slider>("bg/effectCount").onValueChanged.AddListener(OnSliderSoundEffectBtn);


      Find<Toggle>("bg/IsOpnSound").isOn = GameApp.soundManager.IsStop;
      Find<Slider>("bg/soundCount").value = GameApp.soundManager.BgmVolume;
      Find<Slider>("bg/effectCount").value = GameApp.soundManager.EffectVolume;
    }

    //是否静音
    private void OnIsStopBtn(bool isStop)
    {
      GameApp.soundManager.IsStop = isStop;
    }

    //设置bgm音量
    private void OnSliderBgmBtn(float value)
    {
      GameApp.soundManager.BgmVolume = value;
    }

    //设置音效音量
    private void OnSliderSoundEffectBtn(float value)
    {
      GameApp.soundManager.EffectVolume = value;
    }
    //关闭面板
    private void OnCloseBtn()
    {
      GameApp.viewManager.Close(ViewId);
    }

  }
}
