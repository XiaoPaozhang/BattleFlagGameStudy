using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BattleFlagGameStudy
{
  public static class Tools
  {
    public static void SetIcon(this Image image, string res)
    {
      image.sprite = Resources.Load<Sprite>($"Icon/{res}");
    }

    // 检测鼠标位置是否有2d碰撞物体
    public static void ScreenPointToRay2D(Camera camera, Vector2 mousePos, Action<Collider2D> callback)
    {
      Vector3 worldPos = camera.ScreenToWorldPoint(mousePos);
      Collider2D collider2D = Physics2D.OverlapCircle(worldPos, 0.02f);
      callback?.Invoke(collider2D);

    }
  }
}
