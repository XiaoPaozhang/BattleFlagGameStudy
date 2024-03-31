using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{

  public enum BlockType
  {
    Null,//空
    Obstacle,//障碍物
  }

  //地图单元格仔
  public class Block : MonoBehaviour
  {
    public int RowIndex;//行索引
    public int ColIndex;//列索引
    public BlockType blockType;//格子类型
    private SpriteRenderer selectSp;//选中的角色图片
    private SpriteRenderer gridSp;//格子图片
    private SpriteRenderer dirSp;//移动方向图片

    private void Awake()
    {
      selectSp = transform.Find("select").GetComponent<SpriteRenderer>();
      gridSp = transform.Find("grid").GetComponent<SpriteRenderer>();
      dirSp = transform.Find("dir").GetComponent<SpriteRenderer>();

      GameApp.messageCenter.AddEvent(gameObject, Defines.OnSelectEvent, OnSelectCallback);
      GameApp.messageCenter.AddEvent(Defines.OnUnSelectEvent, OnUnSelectCallback);
    }

    void OnDestroy()
    {
      GameApp.messageCenter.RemoveEvent(gameObject, Defines.OnSelectEvent, OnSelectCallback);
      GameApp.messageCenter.RemoveEvent(Defines.OnUnSelectEvent, OnUnSelectCallback);
    }

    public void ShowGrid(Color color)
    {
      gridSp.enabled = true;
      gridSp.color = color;
    }

    public void HideGrid()
    {
      gridSp.enabled = false;
    }



    //注意:当瓦片被选中时
    void OnSelectCallback(object obj)
    {
      //执行所有英雄的取消选中
      GameApp.messageCenter.PostEvent(Defines.OnUnSelectEvent);
    }

    void OnUnSelectCallback(object obj)
    {
      dirSp.sprite = null;
    }

    private void OnMouseEnter()
    {

      selectSp.enabled = true;
    }

    private void OnMouseExit()
    {
      selectSp.enabled = false;
    }


    //设置箭头方向的图片资源 和 颜色
    public void SetDirSp(Sprite sp, Color color)
    {
      dirSp.sprite = sp;
      dirSp.color = color;
    }

  }
}

