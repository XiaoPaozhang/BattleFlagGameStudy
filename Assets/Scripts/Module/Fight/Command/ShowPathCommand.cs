using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  //显示移动路径的命令
  public class ShowPathCommand : BaseCommand
  {
    Collider2D pre;//鼠标之前检测到的2d碰撞器
    Collider2D current;//鼠标当前的2d碰撞器
    AStar aStar;
    AStarPoint starPoint;//开始点
    AStarPoint endPoint;//结束点
    List<AStarPoint> prePaths;//之前检测到的路径集合 用来清空之前的路径
    public ShowPathCommand(ModelBase model) : base(model)
    {
      prePaths = new List<AStarPoint>();
      starPoint = new AStarPoint(model.RowIndex, model.ColIndex);
      aStar = new AStar(GameApp.mapManager.RowCount, GameApp.mapManager.ColCount);
    }
    public override bool Update(float dt)
    {
      //点击鼠标后 确定移动方向
      if (Input.GetMouseButtonDown(0))
      {
        if (prePaths.Count != 0 && this.model.Step >= prePaths.Count - 1)
        {
          GameApp.commandManager.AddCommand(new MoveCommand(this.model, prePaths));//移动
        }
        else
        {
          // GameApp.messageCenter.PostEvent(Defines.OnUnSelectEvent);
        }
        return true;
      }

      //检测当前鼠标是否在2d碰撞器上
      current = Tools.ScreenPointToRay2D(Camera.main, Input.mousePosition);

      if (current != null)
      {
        //之前的检测碰撞盒和当前的盒子不一致 才进行 路径检测
        if (current != pre)
        {
          pre = current;

          Block b = current.GetComponent<Block>();

          if (b != null)
          {
            //检测到block脚本的物体 进行寻路
            endPoint = new AStarPoint(b.RowIndex, b.ColIndex);
            aStar.FindPath(starPoint, endPoint, UpDatePath);
          }
          else
          {
            //没检测到 将之前的路径 清除
            for (int i = 0; i < prePaths.Count; i++)
            {
              GameApp.mapManager.mapArr[prePaths[i].RowIndex, prePaths[i].ColIndex].SetDirSp(null, Color.white);
            }

            prePaths.Clear();
          }
        }
      }
      return false;
    }

    private void UpDatePath(List<AStarPoint> paths)
    {
      //如果之前已经有路径了 要先清除
      if (prePaths.Count != 0)
      {
        for (int i = 0; i < prePaths.Count; i++)
        {
          GameApp.mapManager.mapArr[prePaths[i].RowIndex, prePaths[i].ColIndex].SetDirSp(null, Color.white);
        }
      }

      if (paths.Count > -2 && model.Step >= paths.Count - 1)
      {
        for (int i = 0; i < paths.Count; i++)
        {
          BlockDirection dir = BlockDirection.down;

          if (i == 0)
          {
            dir = GameApp.mapManager.GetDirection(paths[i], paths[i + 1]);
          }
          else if (i == paths.Count - 1)
          {
            dir = GameApp.mapManager.GetDirection2(paths[i], paths[i - 1]);
          }
          else
          {
            dir = GameApp.mapManager.GetDirection3(paths[i - 1], paths[i], paths[i + 1]);
          }

          GameApp.mapManager.SetBlockDir(paths[i].RowIndex, paths[i].ColIndex, dir, Color.yellow);
        }
      }
      prePaths = paths;
    }
  }
}
