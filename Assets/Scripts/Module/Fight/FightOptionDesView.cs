using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BattleFlagGameStudy
{
  public class FightOptionDesView : BaseView
  {
    protected override void OnStart()
    {
      base.OnStart();

      Find<Button>("bg/turnBtn").onClick.AddListener(onChangeEnemyTurnBtn);
      Find<Button>("bg/gameOverBtn").onClick.AddListener(OnGameOverBtn);
      Find<Button>("bg/cancelBtn").onClick.AddListener(onCancelBtn);
    }

    //结束本局游戏
    private void OnGameOverBtn()
    {

      GameApp.viewManager.Close(ViewType.FightOptionDesView);
    }

    //回合结束 切换到敌人回合
    private void onChangeEnemyTurnBtn()
    {
      GameApp.viewManager.Close(ViewType.FightOptionDesView);

      GameApp.fightWorldManager.ChangeState(GameState.Enemy);
    }

    //取消
    private void onCancelBtn()
    {
      GameApp.viewManager.Close(ViewType.FightOptionDesView);
    }
  }
}
