using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BattleFlagGameStudy
{
  /// <summary>
  /// 地图管理器, 存储地图网格信息
  /// </summary>
  public class MapManager
  {
    private Tilemap tilemap;
    public Block[,] mapArr;
    public int RowCount;//地图行
    public int ColCount;//地图列

    public void Init()
    {
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
        // b.transform.position = tilemap.GetCellCenterWorld(tempPosArr[i]);
        b.transform.position = tilemap.CellToWorld(tempPosArr[i]) + new Vector3(0.5f, 0.5f, 0);
        mapArr[row, col] = b;
      }
    }

  }
}