using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  public class AStarPoint
  {
    public int RowIndex;
    public int ColIndex;
    public int G;//当前点到起点的距离
    public int H;//当前点到终点的距离
    public int F;//F=G+H
    public AStarPoint Parent;//找到当前点的父节点
    public AStarPoint(int row, int col)
    {
      this.RowIndex = row;
      this.ColIndex = col;
      this.Parent = null;
    }

    public AStarPoint(int row, int col, AStarPoint parent)
    {
      this.RowIndex = row;
      this.ColIndex = col;
      this.Parent = parent;
    }

    //计算当前点到起点的距离
    //根据行走路径的长度来计算
    public int GetG()
    {
      int _g = 0;
      AStarPoint parent = this.Parent;

      while (parent != null)
      {
        _g++;
        parent = parent.Parent;
      }

      return _g;
    }

    //计算当前点到终点的距离
    //根据曼哈顿距离来计算
    public int GetH(AStarPoint end)
    {
      return Mathf.Abs(RowIndex - end.RowIndex) + Mathf.Abs(ColIndex - end.ColIndex);
    }
  }

  public class AStar
  {
    private int rowCount;
    private int columnCount;
    private List<AStarPoint> open; //open表
    private Dictionary<string, AStarPoint> close;//close表 已经查找过的路径点会存在这个表中
    private AStarPoint startPoint;//起点
    private AStarPoint endPoint;//终点
    public AStar(int rowCount, int columnCount)
    {
      this.rowCount = rowCount;
      this.columnCount = columnCount;
      open = new List<AStarPoint>();
      close = new Dictionary<string, AStarPoint>();
    }

    //找到open表的路径点
    public AStarPoint IsInOpen(int RowIndex, int ColIndex)
    {
      for (int i = 0; i < open.Count; i++)
      {
        if (open[i].RowIndex == RowIndex && open[i].ColIndex == ColIndex)
        {
          return open[i];
        }
      }
      return null;
    }

    //某个点是否已经在close表中
    public bool IsInClose(int rowIndex, int columnIndex)
    {
      return close.ContainsKey($"{rowIndex}_{columnIndex}");
    }

    public bool FindPath(AStarPoint start, AStarPoint end, Action<List<AStarPoint>> findCallback)
    {
      this.startPoint = start;
      this.endPoint = end;
      open = new List<AStarPoint>();
      close = new Dictionary<string, AStarPoint>();

      //起点添加到open表
      open.Add(start);
      while (true)
      {
        //从open表中取出F值最小的点 并将其加入close表
        AStarPoint current = GetMinFromOpen();
        if (current == null)
        {
          //没有路了
          return false;
        }
        else
        {
          //从open列表挪动到close列表
          open.Remove(current);
          close.Add($"{current.RowIndex}_{current.ColIndex}", current);

          //将当前路径点周围的点加入open表
          AddAroundInOpen(current);

          //判断终点是否在open表中
          AStarPoint endPoint = IsInOpen(end.RowIndex, end.ColIndex);
          if (endPoint != null)
          {
            //找到路径,调用找到的回调,并返回整个路径的点 列表
            findCallback(GetPath(endPoint));
            return true;
          }

          //将open表排序
          open.Sort(OpenSort);
        }
      }
    }

    public int OpenSort(AStarPoint a, AStarPoint b)
    {
      return a.F - b.F;
    }
    public List<AStarPoint> GetPath(AStarPoint point)
    {
      List<AStarPoint> paths = new List<AStarPoint>();
      paths.Add(point);
      AStarPoint parent = point.Parent;
      while (parent != null)
      {
        paths.Add(parent);
        parent = parent.Parent;
      }

      //反转路径
      paths.Reverse();
      return paths;
    }

    private void AddAroundInOpen(AStarPoint current)
    {
      if (current.RowIndex - 1 >= 0) AddOpen(current, current.RowIndex - 1, current.ColIndex);
      if (current.RowIndex + 1 < rowCount) AddOpen(current, current.RowIndex + 1, current.ColIndex);
      if (current.ColIndex - 1 >= 0) AddOpen(current, current.RowIndex, current.ColIndex - 1);
      if (current.ColIndex + 1 < columnCount) AddOpen(current, current.RowIndex, current.ColIndex + 1);
    }

    public void AddOpen(AStarPoint current, int row, int col)
    {
      //不在open表中 且 不在close表中 且 不是障碍物 才能加入open表
      if (IsInClose(row, col) == false && IsInOpen(row, col) == null
       && GameApp.mapManager.GetBlockType(row, col) == BlockType.Null)
      {
        AStarPoint newPoint = new AStarPoint(row, col, current);
        newPoint.G = newPoint.GetG();
        newPoint.H = newPoint.GetH(endPoint);
        newPoint.F = newPoint.G + newPoint.H;
        open.Add(newPoint);
      }
    }
    public AStarPoint GetMinFromOpen()
    {
      if (open.Count == 0)
      {
        return null;
      }
      return open[0];//open 表会排序, 最小f值在第一位 所以返回第一个点
    }
  }
}
