using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  public class Hero : ModelBase
  {
    public void Init(Dictionary<string, string> data, int row, int col)
    {
      this.data = data;
      this.RowIndex = row;
      this.ColIndex = col;
      this.id = int.Parse(data["Id"]);
      this.Type = int.Parse(data["Type"]);
      this.Attack = int.Parse(data["Attack"]);
      this.Step = int.Parse(data["Step"]);
      this.MaxHp = int.Parse(data["Hp"]);
      this.CurHp = this.MaxHp;

    }

    protected override void OnSelectCallback(object args)
    {
      //玩家回合 才能选中角色
      if (GameApp.fightWorldManager.state == GameState.Player)
      {
        if (HasMovementCompleted)
        {
          return;
        }
        if (GameApp.commandManager.IsRunningCommand)
        {
          return;
        }

        //添加显示路径的指令
        GameApp.commandManager.AddCommand(new ShowPathCommand(this));

        base.OnSelectCallback(args);

        GameApp.viewManager.Open(ViewType.HeroDesView, this);
      }
    }

    protected override void OnUnSelectCallback(object args)
    {
      base.OnUnSelectCallback(args);

      GameApp.viewManager.Close(ViewType.HeroDesView);
    }
  }
}
