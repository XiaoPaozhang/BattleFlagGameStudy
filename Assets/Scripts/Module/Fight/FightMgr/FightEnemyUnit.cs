using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  /// <summary>
  /// 敌人回合
  /// </summary>
  public class FightEnemyUnit : FightUnitBase
  {
    public override void Init()
    {
      base.Init();

      GameApp.fightWorldManager.ResetHeros();//重置英雄行动

      GameApp.viewManager.Open(ViewType.TipView, "敌人回合");

      GameApp.commandManager.AddCommand(new WaitCommand(1.25f));

      //敌人移动 使用技能等
      for (int i = 0; i < GameApp.fightWorldManager.enemies.Count; i++)
      {
        Enemy enemy = GameApp.fightWorldManager.enemies[i];
        GameApp.commandManager.AddCommand(new WaitCommand(0.2f));//等待 
        GameApp.commandManager.AddCommand(new AiMoveCommand(enemy));//敌人ai移动
        GameApp.commandManager.AddCommand(new WaitCommand(0.2f));//等待 
        GameApp.commandManager.AddCommand(new SkillCommand(enemy));//敌人ai使用技能
        GameApp.commandManager.AddCommand(new WaitCommand(0.2f));//等待 
      }
      //等待一段时间 切换回玩家回合
      GameApp.commandManager.AddCommand(new WaitCommand(0.2f, () =>
      {
        GameApp.fightWorldManager.ChangeState(GameState.Player);
      }));
    }
  }
}
