using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BattleFlagGameStudy
{
  /// <summary>
  /// 敌人信息界面
  /// </summary>.
  public class EnemyDesView : BaseView
  {
    public override void Open(params object[] args)
    {
      base.Open(args);

      Enemy enemy = args[0] as Enemy;
      Find<Image>("bg/icon").SetIcon(enemy.data["Icon"]);
      Find<Image>("bg/hp/fill").fillAmount = (float)enemy.CurHp / (float)enemy.MaxHp;
      Find<Text>("bg/hp/txt").text = $"{enemy.CurHp}/{enemy.MaxHp}";
      Find<Text>("bg/atkTxt/txt").text = enemy.Attack.ToString();
      Find<Text>("bg/stepTxt/txt").text = enemy.Step.ToString();


    }
  }
}
