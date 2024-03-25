using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  public class FightController : BaseController
  {
    public FightController() : base()
    {
      GameApp.viewManager.Register(ViewType.FightView, new ViewInfo()
      {
        prefabName = "FightView",
        controller = this,
        parentTf = GameApp.viewManager.canvasTf
      });

      GameApp.viewManager.Register(ViewType.FightSelectHeroView, new ViewInfo()
      {
        prefabName = "FightSelectHeroView",
        controller = this,
        parentTf = GameApp.viewManager.canvasTf,
        Sorting_Order = 1,
      });

      InitModuleEvent();
    }

    public override void InitModuleEvent()
    {
      base.InitModuleEvent();

      RegisterFunc(Defines.OnBeginFight, OnBeginFight);
    }

    private void OnBeginFight(object arg)
    {
      GameApp.viewManager.Open(ViewType.FightView);
      GameApp.viewManager.Open(ViewType.FightSelectHeroView);
    }
  }
}
