using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  public class FightGameOverUnit : FightUnitBase
  {
    public override void Init()
    {
      base.Init();

      GameApp.commandManager.Clear();//清除指令

      if (GameApp.fightWorldManager.heroes.Count == 0)
      {
        GameApp.commandManager.AddCommand(new WaitCommand(1.0f, () =>
        {
          GameApp.viewManager.Open(ViewType.LossView);
        }));
      }
      else if (GameApp.fightWorldManager.enemies.Count == 0)
      {
        GameApp.commandManager.AddCommand(new WaitCommand(1.0f, () =>
        {
          GameApp.viewManager.Open(ViewType.WinView);
        }));
      }
      else
      {

      }
    }

    public override bool Update(float deltaTime)
    {
      return true;
    }
  }
}
