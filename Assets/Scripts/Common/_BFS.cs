
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleFlagGameStudy
{
  /// <summary>
  /// 广度优先搜索算法
  /// </summary>
  public class _BFS
  {
    //搜索点
    public class Point
    {
      public int RowIndex;
      public int ColIndex;
      public Point Father;//父节点 用来查找路径用
      public Point(int rowIndex, int colIndex)
      {
        this.RowIndex = rowIndex;
        this.ColIndex = colIndex;
      }

      public Point(int row, int col, Point father)
      {
        this.RowIndex = row;
        this.ColIndex = col;
        this.Father = father;
      }
    }

    public int RowCount;//行总数
    public int ColCount;//列总数
    public Dictionary<string, Point> finds;//存储查找到的点的字典 (key:点的行列拼接的字符串 value:搜索点)

    public _BFS(int rowCount, int colCount)
    {
      finds = new Dictionary<string, Point>();
      RowCount = rowCount;
      ColCount = colCount;
    }


    //搜索行走区域
    public List<Point> Search(int startRow, int startCol, int step)
    {
      //定义搜索集合
      List<Point> searches = new List<Point>();

      //开始点
      Point startPoint = new Point(startRow, startCol);
      //将开始点存在搜索集合中
      searches.Add(startPoint);
      //开始点数默认开始已经找到 存储到已经存到的字典
      finds.Add($"{RowCount}_{ColCount}", startPoint);

      // 遍历参数 相当于可搜索的次数
      for (int i = 0; i < step; i++)
      {
        //定义一个临时的集合 用于存储目前找到满足条件的点
        List<Point> tempSearches = new List<Point>();
        //遍历搜索集合
        for (int j = 0; j < searches.Count; j++)
        {
          Point current = searches[j];
          //查找当前点四周 的点
          FindAroundPoints(current, tempSearches);
        }

        if (tempSearches.Count == 0)
        {
          //临时集合一个点都找不到 相当于死路了 没必要继续搜索了
          break;
        }

        //搜索的集合要清空
        searches.Clear();
        //将临时集合的点添加到搜索集合
        searches.AddRange(tempSearches);
      }

      //将找到的点转换成集合 返回
      return finds.Values.ToList();
    }

    //找周围的点 上下左右四个方向(可以扩展查找斜方向的点)
    public void FindAroundPoints(Point current, List<Point> temps)
    {
      if (current.RowIndex - 1 >= 0)
      {
        AddFinds(current.RowIndex - 1, current.ColIndex, current, temps);
      }
      if (current.RowIndex + 1 < RowCount)
      {
        AddFinds(current.RowIndex + 1, current.ColIndex, current, temps);
      }
      if (current.ColIndex - 1 >= 0)
      {
        AddFinds(current.RowIndex, current.ColIndex - 1, current, temps);
      }
      if (current.ColIndex + 1 < ColCount)
      {
        AddFinds(current.RowIndex, current.ColIndex + 1, current, temps);
      }
    }

    //添加到查找到的字典中
    public void AddFinds(int row, int col, Point father, List<Point> temps)
    {
      //不是已经查找过的节点 且 对应地图格子的类似不是障碍物 才能加入 查找字典
      if (finds.ContainsKey($"{row}_{col}") == false && GameApp.mapManager.GetBlockType(row, col) != BlockType.Obstacle)
      {
        Point p = new Point(row, col, father);
        finds.Add($"{row}_{col}", p);
        temps.Add(p);
      }
    }

    //寻找可移动的点 离终点最近的点的路径集合
    public List<Point> FindMinPath(ModelBase model, int step, int endRowIndex, int endColIndex)
    {
      List<Point> result = Search(model.RowIndex, model.ColIndex, step);

      if (result.Count == 0)
      {
        return null;
      }
      else
      {
        Point minPoint = result[0];//默认一个点为离目标点距离最近
        int mid_dis = Mathf.Abs(minPoint.RowIndex - endColIndex) + Mathf.Abs(minPoint.ColIndex - endColIndex);

        for (int i = 1; i < result.Count; i++)
        {
          int temp_dis = Mathf.Abs(result[i].RowIndex - endRowIndex) + Mathf.Abs(result[i].ColIndex - endColIndex);
          if (temp_dis < mid_dis)
          {
            mid_dis = temp_dis;
            minPoint = result[i];
          }
        }

        //找到了离目标点最近的点 开始回溯路径
        List<Point> paths = new List<Point>();
        Point current = minPoint.Father;
        while (current != null)
        {
          paths.Add(current);
          current = current.Father;
        }
        paths.Reverse();
        return paths;
      }
    }
  }
}

