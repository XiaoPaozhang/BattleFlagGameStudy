using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BattleFlagGameStudy
{
  public class TipView : BaseView
  {
    public override void Open(params object[] args)
    {
      base.Open(args);

      Find<Text>("content/txt").text = args[0] as string;
      Sequence seq = DOTween.Sequence();
      seq.Append(Find("content").transform.DOScaleY(1, 0.15f)).SetEase(Ease.OutBack);
      seq.AppendInterval(.75f);
      seq.Append(Find("content").transform.DOScaleY(0, 0.15f)).SetEase(Ease.Linear);
      seq.OnComplete(() =>
      {
        GameApp.viewManager.Close(ViewId);
      });
    }
  }
}
