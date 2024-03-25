using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BattleFlagGameStudy
{
  public class UserInputManager
  {
    public void Update(float deltaTime)
    {
      if (Input.GetMouseButtonDown(0))
      {
        if (EventSystem.current.IsPointerOverGameObject())
        {
          //点击到了ui
        }
        else
        {
          Tools.ScreenPointToRay2D(Camera.main, Input.mousePosition, (Collider2D collider2D) =>
          {
            if (collider2D != null)
            {
              Debug.Log($"<color=yellow>点击到了：{collider2D.gameObject.name}</color>");
              //检测到有碰撞物体
              GameApp.messageCenter.PostEvent(collider2D.gameObject, Defines.OnSelectEvent);
            }
            else
            {
              Debug.Log($"<color=white>未选中任何物体</color>");
              //执行未选中
              GameApp.messageCenter.PostEvent(Defines.OnUnSelectEvent);
            }
          });
        }
      }
    }
  }
}
