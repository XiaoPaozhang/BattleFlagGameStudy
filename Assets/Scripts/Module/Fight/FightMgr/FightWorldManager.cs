using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  public enum GameState
  {
    Idle,
    FightEnter,
  }
  /// <summary>
  /// 战斗管理器（用于管理战斗相关的实体（敌人英雄地图格子等））
  /// </summary>
  public class FightWorldManager
  {
    public GameState state = GameState.Idle;

    private FightUnitBase current;// 当前战斗单位

    public List<Hero> heroes;// 战斗中的英雄列表
    public FightUnitBase Current => current;

    public void Update(float deltaTime)
    {
      if (current != null && current.Update(deltaTime))
      {
        //todo
      }
      else
      {
        current = null;
      }
    }


    public FightWorldManager()
    {
      heroes = new List<Hero>();
      ChangeState(GameState.Idle);
    }

    public void ChangeState(GameState state)
    {
      FightUnitBase _current = current;
      this.state = state;
      switch (this.state)
      {
        case GameState.Idle:
          _current = new FightIdle();
          break;
        case GameState.FightEnter:
          _current = new FightEnter();
          break;
        default:
          break;
      }
      _current.Init();
    }

    //添加英雄
    public void AddHero(Block b, Dictionary<string, string> data)
    {
      GameObject obj = Object.Instantiate(Resources.Load($"Model/{data["Model"]}")) as GameObject;
      Vector3 bPos = b.transform.position;
      obj.transform.position = new Vector3(bPos.x, bPos.y, -1);
      Hero hero = obj.AddComponent<Hero>();
      hero.Init(data, b.RowIndex, b.ColIndex);

      //这个位置呗占领了 设置方块的类型为障碍物
      b.blockType = BlockType.Obstacle;
      heroes.Add(hero);
    }
  }
}
