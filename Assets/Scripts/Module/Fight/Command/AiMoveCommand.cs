using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  /// <summary>
  /// 敌人AI移动指令
  /// </summary>
  public class AiMoveCommand : BaseCommand
  {
    Enemy enemy;
    _BFS bfs;
    List<_BFS.Point> paths;
    _BFS.Point currentPoint;
    int pathIndex;
    ModelBase target;//移动到的目标
    public AiMoveCommand(Enemy enemy) : base(enemy)
    {
      this.enemy = enemy;
      bfs = new _BFS(GameApp.mapManager.RowCount, GameApp.mapManager.ColCount);
      paths = new List<_BFS.Point>();
    }

    public override void Do()
    {
      base.Do();

      target = GameApp.fightWorldManager.GetMinDisHero(enemy);//获得最近的英雄

      if (target == null)
      {
        //没有目标了
        isFinish = true;
      }
      else
      {
        paths = bfs.FindMinPath(this.enemy, this.enemy.Step, target.RowIndex, target.ColIndex);

        if (paths == null)
        {
          //没路 这里可以随机 一个点做移动
          isFinish = true;
        }
        else
        {
          //将当前敌人的位置设置成null
          GameApp.mapManager.ChangeBlockType(this.enemy.RowIndex, this.enemy.ColIndex, BlockType.Null);

        }
      }
    }

    public override bool Update(float dt)
    {
      if (paths.Count == 0)
      {
        return base.Update(dt);
      }
      else
      {
        currentPoint = paths[pathIndex];
        if (model.MoveToSpecifiedBlock(currentPoint.RowIndex, currentPoint.ColIndex, dt * 5) == true)
        {
          pathIndex++;
          if (pathIndex > paths.Count - 1)
          {
            enemy.PlayAni("idle");

            GameApp.mapManager.ChangeBlockType(enemy.RowIndex, enemy.ColIndex, BlockType.Obstacle);
            return true;
          }
        }
      }

      model.PlayAni("move");

      return false;
    }
  }
}
