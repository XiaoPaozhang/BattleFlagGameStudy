using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BattleFlagGameStudy
{
  public class DragHeroView : BaseView
  {
    void Update()
    {
      //拖拽种跟随鼠标移动 显示的时候才进行移动
      if (!canvas.enabled) return;

      //鼠标坐标转换为世界坐标
      Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      transform.position = worldPos;
    }

    public override void Open(params object[] args)
    {
      base.Open(args);

      transform.GetComponent<Image>().SetIcon(args[0].ToString());
    }
  }
}
