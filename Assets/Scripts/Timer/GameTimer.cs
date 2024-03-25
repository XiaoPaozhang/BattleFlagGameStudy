using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  public class GameTimer
  {
    private List<GameTimerData> timers;


    public GameTimer()
    {
      timers = new List<GameTimerData>();
    }

    public void RegisterTimer(float timer, Action callback)
    {
      GameTimerData data = new GameTimerData(timer, callback);
      timers.Add(data);
    }

    public void OnUpdate(float deltaTime)
    {
      // 从后往前遍历，防止删除元素导致索引变化
      for (int i = timers.Count - 1; i >= 0; i--)
      {
        if (timers[i].OnUpdate(deltaTime))
        {
          timers.RemoveAt(i);
        }
      }
    }

    /// <summary>
    /// 打断计时器
    /// </summary>
    public void Break()
    {
      timers.Clear();
    }

    public int GetTimerCount()
    {
      return timers.Count;
    }
  }
}
