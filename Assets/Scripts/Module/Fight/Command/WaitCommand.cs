using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  public class WaitCommand : BaseCommand
  {
    private float time;
    Action callback;
    public WaitCommand(float t, Action callback = null)
    {
      this.time = t;
      this.callback = callback;
    }

    public override bool Update(float dt)
    {
      this.time -= dt;
      if (this.time <= 0)
      {
        this.callback?.Invoke();
        return true;
      }
      return false;
    }
  }
}
