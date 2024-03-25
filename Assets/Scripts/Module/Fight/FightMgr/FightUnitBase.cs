using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  public class FightUnitBase
  {
    public virtual void Init() { }

    public virtual bool Update(float deltaTime) { return false; }
  }
}
