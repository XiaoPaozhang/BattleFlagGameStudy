using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TerrainTools;

namespace BattleFlagGameStudy
{
  public class MoveCommand : BaseCommand
  {
    private List<AStarPoint> paths;
    private AStarPoint currentPoint;
    private int pathIndex;

    //移动前的行列坐标 撤销用
    private int oldRow;
    private int oldCol;
    public MoveCommand(ModelBase model) : base(model)
    {

    }

    public MoveCommand(ModelBase model, List<AStarPoint> path) : base(model)
    {
      this.paths = path;
      pathIndex = 0;
    }

    public override void Do()
    {
      base.Do();

      this.oldRow = this.model.RowIndex;
      this.oldCol = this.model.ColIndex;

      //移动前先将当前格子设置为null
      GameApp.mapManager.ChangeBlockType(this.model.RowIndex, this.model.ColIndex, BlockType.Null);
    }

    public override bool Update(float dt)
    {

      this.currentPoint = this.paths[pathIndex];
      if (this.model.MoveToSpecifiedBlock(this.currentPoint.RowIndex, this.currentPoint.ColIndex, dt * 5))
      {
        pathIndex++;
        if (pathIndex >= this.paths.Count)
        {
          //到达目的地
          this.model.PlayAni("idle");
          GameApp.mapManager.ChangeBlockType(this.currentPoint.RowIndex, this.currentPoint.ColIndex, BlockType.Obstacle);

          return true;
        }
      }
      this.model.PlayAni("move");

      return false;
    }

    //撤销
    public override void UnDo()
    {
      base.UnDo();

      //回到之前的位置
      Vector3 pos = GameApp.mapManager.GetBlockPos(oldRow, oldCol);
      pos.z = this.model.transform.position.z;
      this.model.transform.position = pos;

      GameApp.mapManager.ChangeBlockType(this.model.RowIndex, this.model.ColIndex, BlockType.Null);
      this.model.RowIndex = oldRow;
      this.model.ColIndex = oldCol;
      GameApp.mapManager.ChangeBlockType(this.model.RowIndex, this.model.ColIndex, BlockType.Obstacle);
    }
  }
}
