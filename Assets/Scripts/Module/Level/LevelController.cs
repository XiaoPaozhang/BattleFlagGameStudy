using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  //关卡控制器
  public class LevelController : BaseController
  {
    public LevelController() : base()
    {

      GameApp.viewManager.Register(ViewType.SelectLevelView, new ViewInfo()
      {
        prefabName = "SelectLevelView",
        parentTf = GameApp.viewManager.canvasTf,
        controller = this,
      });

      InitModuleEvent();
    }

    public override void InitModuleEvent()
    {
      RegisterFunc(Defines.OpenSelectLevelView, OnOpenSelectLevelView);
    }

    private void OnOpenSelectLevelView(System.Object[] args)
    {
      GameApp.viewManager.Open(ViewType.SelectLevelView, args);
    }
  }
}
