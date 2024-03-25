using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  /// <summary>
  /// 存放玩家基本的游戏信息
  /// </summary>
  public class GameDataManager
  {
    public List<int> heros;//英雄集合
    public int moneys;//金币数量
    public GameDataManager()
    {
      heros = new List<int>();

      //默认三个英雄id 预先存起来
      heros.Add(10001);
      heros.Add(10002);
      heros.Add(10003);
    }
  }
}
