using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  /// <summary>
  /// 检测点，用于触发关卡信息显示
  /// </summary>
  public class BuildPoint : MonoBehaviour
  {

    public int levelId;
    void OnTriggerEnter2D(Collider2D other)
    {
      GameApp.messageCenter.PostEvent(Defines.ShowLevelDesEvent, levelId);
    }
    void OnTriggerExit2D(Collider2D other)
    {
      GameApp.messageCenter.PostEvent(Defines.HideLevelDesEvent);
    }
  }
}
