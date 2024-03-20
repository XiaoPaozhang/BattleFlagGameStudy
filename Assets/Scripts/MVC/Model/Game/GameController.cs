using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;

namespace BattleFlagGameStudy
{
  /// <summary>
  /// 游戏主控制器
  /// </summary>
  public class GameController : BaseController
  {
    public GameController() : base()
    {
      InitModuleEvent();
      InitGlobalEvent();
    }

    public override void Init()
    {
      //调用GameUIController开发面板事件
      ApplyControllerFunc(ControllerType.GameUI, Defines.OpenStartView);
    }
  }
}
