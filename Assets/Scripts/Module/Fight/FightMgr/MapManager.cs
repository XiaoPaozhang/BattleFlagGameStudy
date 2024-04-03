using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BattleFlagGameStudy
{

  //格子显示方向的枚举 枚举字符串跟资源图片路径一致
  public enum BlockDirection
  {
    none = -1,
    down,
    horizontal,
    left,
    left_down,
    left_up,
    right,
    right_down,
    right_up,
    up,
    vertical,
    max,
  }

  /// <summary>
  /// 地图管理器, 存储地图网格信息
  /// </summary>
  public class MapManager
  {
    private Tilemap tilemap;
    public Block[,] mapArr;
    public int RowCount;//地图行
    public int ColCount;//地图列

    public List<Sprite> dirSpArr;//存储箭头方向的图片集合

    public void Init()
    {
      dirSpArr = new List<Sprite>();
      for (int i = 0; i < (int)BlockDirection.max; i++)
      {
        dirSpArr.Add(Resources.Load<Sprite>($"Icon/{(BlockDirection)i}"));
      }

      tilemap = GameObject.Find("Grid/ground").GetComponent<Tilemap>();

      RowCount = 12;
      ColCount = 20;

      mapArr = new Block[RowCount, ColCount];

      List<Vector3Int> tempPosArr = new List<Vector3Int>();//用来临时存放瓦片格子的坐标
      //遍历所有的瓦片格子, 并将含有地面瓦片的坐标存入tempPosArr
      foreach (var pos in tilemap.cellBounds.allPositionsWithin) if (tilemap.HasTile(pos)) tempPosArr.Add(pos);

      GameObject prefabObj = Resources.Load("Model/block") as GameObject;
      for (int i = 0; i < tempPosArr.Count; i++)
      {
        int row = i / ColCount;
        int col = i % ColCount;
        Block b = Object.Instantiate(prefabObj).AddComponent<Block>();
        b.RowIndex = row;
        b.ColIndex = col;
        b.transform.position = tilemap.CellToWorld(tempPosArr[i]) + new Vector3(0.5f, 0.5f, 0);
        mapArr[row, col] = b;
      }
    }

    public Vector3 GetBlockPos(int row, int col)
    {
      return mapArr[row, col].transform.position;
    }

    //获取瓦片的类型
    public BlockType GetBlockType(int row, int col)
    {
      return mapArr[row, col].blockType;
    }

    public void ChangeBlockType(int row, int col, BlockType blockType)
    {
      mapArr[row, col].blockType = blockType;
    }

    //显示移动的区域
    public void ShowStepGrid(ModelBase model, int step)
    {
      _BFS bfs = new _BFS(RowCount, ColCount);
      List<_BFS.Point> points = bfs.Search(model.RowIndex, model.ColIndex, step);

      for (int i = 0; i < points.Count; i++)
      {
        mapArr[points[i].RowIndex, points[i].ColIndex].ShowGrid(Color.green);
      }
    }

    //隐藏移动的区域
    public void HideStepGrid(ModelBase model, int step)
    {
      _BFS bfs = new _BFS(RowCount, ColCount);
      List<_BFS.Point> points = bfs.Search(model.RowIndex, model.ColIndex, step);

      for (int i = 0; i < points.Count; i++)
      {
        mapArr[points[i].RowIndex, points[i].ColIndex].HideGrid();
      }
    }

    //根据方向枚举 设置格子的方向图标和颜色
    public void SetBlockDir(int row, int col, BlockDirection dir, Color color)
    {
      mapArr[row, col].SetDirSp(dirSpArr[(int)dir], color);
    }


    //开始点 和 终点 算出 方向 
    public BlockDirection GetDirection(AStarPoint starPoint, AStarPoint next)
    {
      int row_offset = next.RowIndex - starPoint.RowIndex;
      int col_offset = next.ColIndex - starPoint.ColIndex;
      if (row_offset == 0)
      {
        return BlockDirection.horizontal;
      }
      else if (col_offset == 0)
      {
        return BlockDirection.vertical;
      }
      return BlockDirection.none;
    }

    //终点 和 前一个点 算出 方向 
    public BlockDirection GetDirection2(AStarPoint end, AStarPoint pre)
    {
      int row_offset = end.RowIndex - pre.RowIndex;
      int col_offset = end.ColIndex - pre.ColIndex;
      if (row_offset == 0 && col_offset > 0)
      {
        return BlockDirection.right;
      }
      else if (row_offset == 0 && col_offset < 0)
      {
        return BlockDirection.left;
      }
      else if (row_offset > 0 && col_offset == 0)
      {
        return BlockDirection.up;
      }
      else if (row_offset < 0 && col_offset == 0)
      {
        return BlockDirection.down;
      }
      else
      {
        return BlockDirection.none;
      }

    }

    //三个点 算出 方向
    public BlockDirection GetDirection3(AStarPoint pre, AStarPoint current, AStarPoint end)
    {
      BlockDirection dir = BlockDirection.none;

      int row_offset_1 = pre.RowIndex - current.RowIndex;
      int col_offset_1 = pre.ColIndex - current.ColIndex;

      int row_offset_2 = end.RowIndex - current.RowIndex;
      int col_offset_2 = end.ColIndex - current.ColIndex;

      int sum_row_offset = row_offset_1 + row_offset_2;
      int sum_col_offset = col_offset_1 + col_offset_2;

      if (sum_row_offset == 1 && sum_col_offset == -1)
      {
        dir = BlockDirection.left_up;
      }
      else if (sum_row_offset == 1 && sum_col_offset == 1)
      {
        dir = BlockDirection.right_up;
      }
      else if (sum_row_offset == -1 && sum_col_offset == -1)
      {
        dir = BlockDirection.left_down;
      }
      else if (sum_row_offset == -1 && sum_col_offset == 1)
      {
        dir = BlockDirection.right_down;
      }
      else
      {
        if (row_offset_1 == 0)
        {
          dir = BlockDirection.horizontal;
        }
        else
        {
          dir = BlockDirection.vertical;
        }
      }

      return dir;
    }

    public void SHowAttackStep(ModelBase model, int attackStep, Color color)
    {
      int minRow = model.RowIndex - attackStep >= 0 ? model.RowIndex - attackStep : 0;
      int minCol = model.ColIndex - attackStep >= 0 ? model.ColIndex - attackStep : 0;
      int maxRow = model.RowIndex + attackStep > RowCount - 1 ? RowCount - 1 : model.RowIndex + attackStep;
      int maxCol = model.ColIndex + attackStep > ColCount - 1 ? ColCount - 1 : model.ColIndex + attackStep;

      for (int i = minRow; i <= maxRow; i++)
      {
        for (int j = minCol; j <= maxCol; j++)
        {
          if (Mathf.Abs(model.RowIndex - i) + Mathf.Abs(model.ColIndex - j) <= attackStep)
          {
            mapArr[i, j].ShowGrid(color);
          }
        }
      }
    }

    public void HideAttackStep(ModelBase model, int attackStep)
    {
      int minRow = model.RowIndex - attackStep >= 0 ? model.RowIndex - attackStep : 0;
      int minCol = model.ColIndex - attackStep >= 0 ? model.ColIndex - attackStep : 0;
      int maxRow = model.RowIndex + attackStep > RowCount - 1 ? RowCount - 1 : model.RowIndex + attackStep;
      int maxCol = model.ColIndex + attackStep > ColCount - 1 ? ColCount - 1 : model.ColIndex + attackStep;

      for (int i = minRow; i <= maxRow; i++)
      {
        for (int j = minCol; j <= maxCol; j++)
        {
          if (Mathf.Abs(model.RowIndex - i) + Mathf.Abs(model.ColIndex - j) <= attackStep)
          {
            mapArr[i, j].HideGrid();
          }
        }
      }
    }

    //清空
    public void clear()
    {
      mapArr = null;
      dirSpArr.Clear();
    }
  }
}