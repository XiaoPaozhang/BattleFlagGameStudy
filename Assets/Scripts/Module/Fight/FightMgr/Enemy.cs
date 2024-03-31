using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  public class Enemy : ModelBase
  {
    protected override void Start()
    {
      base.Start();

      data = GameApp.configManager.GetConfigData("enemy").GetDataById(id);


      this.Type = int.Parse(data["Type"]);
      this.Attack = int.Parse(data["Attack"]);
      this.Step = int.Parse(data["Step"]);
      this.MaxHp = int.Parse(data["Hp"]);
      this.CurHp = this.MaxHp;
    }

    protected override void OnSelectCallback(object args)
    {
      base.OnSelectCallback(args);

      GameApp.viewManager.Open(ViewType.EnemyDesView, this);
    }

    protected override void OnUnSelectCallback(object args)
    {
      base.OnUnSelectCallback(args);

      GameApp.viewManager.Close(ViewType.EnemyDesView);
    }
  }
}
