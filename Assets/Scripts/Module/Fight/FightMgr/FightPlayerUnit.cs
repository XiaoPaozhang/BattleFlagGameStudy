using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  /// <summary>
  /// 玩家的回合
  /// </summary>
  public class FightPlayerUnit : FightUnitBase
  {
    public override void Init()
    {
      base.Init();

      GameApp.viewManager.Open(ViewType.TipView, "玩家回合");
    }

  }
}
