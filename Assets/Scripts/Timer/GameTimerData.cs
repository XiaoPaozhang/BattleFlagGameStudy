using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  public class GameTimerData
  {
    private float _time;
    private Action _callback;
    public GameTimerData(float time, Action callback)
    {
      _time = time;
      _callback = callback;
    }

    public bool OnUpdate(float deltaTime)
    {
      _time -= deltaTime;
      if (_time <= 0)
      {
        _callback?.Invoke();
        return true;
      }
      return false;
    }
  }
}
