using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace BattleFlagGameStudy
{
  public class TimerManager
  {
    GameTimer timer;
    public TimerManager()
    {
      timer = new GameTimer();
    }

    public void Register(float time, Action callback)
    {
      timer.RegisterTimer(time, callback);
    }

    public void OnUpdate(float dt)
    {
      timer.OnUpdate(dt);
    }
  }
}
