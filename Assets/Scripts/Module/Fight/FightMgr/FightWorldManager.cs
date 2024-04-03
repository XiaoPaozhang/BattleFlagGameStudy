
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  public enum GameState
  {
    Idle,
    FightEnter,
    Player,
    Enemy,
    GameOver,
  }
  /// <summary>
  /// 战斗管理器（用于管理战斗相关的实体（敌人英雄地图格子等））
  /// </summary>
  public class FightWorldManager
  {
    public GameState state = GameState.Idle;

    private FightUnitBase current;// 当前战斗单位

    public List<Hero> heroes;// 战斗中的英雄列表
    public List<Enemy> enemies;// 战斗中的敌人列表
    public int RoundCount;// 当前回合数
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
      enemies = new List<Enemy>();
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
        case GameState.Player:
          _current = new FightPlayerUnit();
          break;
        case GameState.Enemy:
          _current = new FightEnemyUnit();
          break;
        case GameState.GameOver:
          _current = new FightGameOverUnit();
          break;
        default:
          break;
      }
      _current.Init();
    }
    //进入战斗,初始化一些数据,敌人信息,回合数等
    public void EnterFight()
    {
      RoundCount = 1;
      heroes = new List<Hero>();
      enemies = new List<Enemy>();

      //将场景中的 敌人脚本 进行存储
      GameObject[] enemyObjs = GameObject.FindGameObjectsWithTag("Enemy");
      Debug.Log("enemy:" + enemyObjs.Length);

      foreach (GameObject enemyObj in enemyObjs)
      {
        Enemy enemy = enemyObj.GetComponent<Enemy>();

        //当前位置被占用了 要把对应的方块设置为障碍物
        GameApp.mapManager.ChangeBlockType(enemy.RowIndex, enemy.ColIndex, BlockType.Obstacle);
        enemies.Add(enemy);
      }
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

    //移除敌人
    public void RemoveEnemy(Enemy enemy)
    {
      enemies.Remove(enemy);

      GameApp.mapManager.ChangeBlockType(enemy.RowIndex, enemy.ColIndex, BlockType.Null);

      if (enemies.Count == 0)
      {
        ChangeState(GameState.GameOver);
      }
    }

    //移除英雄
    public void RemoveHero(Hero hero)
    {
      heroes.Remove(hero);
      GameApp.mapManager.ChangeBlockType(hero.RowIndex, hero.ColIndex, BlockType.Null);

      if (heroes.Count == 0)
      {
        ChangeState(GameState.GameOver);
      }
    }

    //重置英雄行动
    public void ResetHeros()
    {
      for (int i = 0; i < heroes.Count; i++)
      {
        heroes[i].HasMovementCompleted = false;
      }
    }

    //重置敌人行动
    public void ResetEnemies()
    {
      for (int i = 0; i < enemies.Count; i++)
      {
        enemies[i].HasMovementCompleted = false;
      }
    }

    /// <summary>
    /// 获得离目标最近的英雄
    /// </summary>
    /// <param name="targetModel"></param>
    /// <returns></returns>
    public ModelBase GetMinDisHero(ModelBase targetModel)
    {
      if (heroes.Count == 0)
        return null;

      Hero hero = heroes[0];
      float min_dis = hero.GetDis(targetModel);

      for (int i = 1; i < heroes.Count; i++)
      {
        float dis = heroes[i].GetDis(targetModel);
        if (dis < min_dis)
        {
          min_dis = dis;
          hero = heroes[i];
        }
      }
      return hero;
    }

    //卸载资源
    public void ReLoadRes()
    {
      heroes.Clear();
      enemies.Clear();
      GameApp.mapManager.clear();
    }
  }
}
