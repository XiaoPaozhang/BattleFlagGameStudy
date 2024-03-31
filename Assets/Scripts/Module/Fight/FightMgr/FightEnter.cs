using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  public class FightEnter : FightUnitBase
  {
    public override void Init()
    {
      base.Init();

      GameApp.mapManager.Init();

      //进入战斗
      GameApp.fightWorldManager.EnterFight();
    }
  }
}
