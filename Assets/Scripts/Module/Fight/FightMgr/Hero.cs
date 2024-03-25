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
  }
}
