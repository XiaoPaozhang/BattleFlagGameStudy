using System.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BattleFlagGameStudy
{
  public class MessageInfo
  {
    public string msgTxt;
    public Action okCallback;
    public Action noCallback;

  }

  /// <summary>
  /// 提示界面
  /// </summary>
  public class MessageView : BaseView
  {
    MessageInfo messageInfo;
    protected override void OnAwake()
    {
      base.OnAwake();

      Find<Button>("okBtn").onClick.AddListener(OnOkBtn);
      Find<Button>("noBtn").onClick.AddListener(OnNoBtn);
    }

    public override void Open(params object[] args)
    {
      base.Open(args);

      messageInfo = args[0] as MessageInfo;

      Find<Text>("content/txt").text = messageInfo.msgTxt;
    }
    private void OnOkBtn()
    {
      messageInfo.okCallback?.Invoke();
    }

    private void OnNoBtn()
    {
      messageInfo.noCallback?.Invoke();
      GameApp.viewManager.Close(ViewId);
    }
  }

}
