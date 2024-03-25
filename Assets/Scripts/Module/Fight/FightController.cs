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

      GameApp.viewManager.Register(ViewType.DragHeroView, new ViewInfo()
      {
        prefabName = "DragHeroView",
        controller = this,
        parentTf = GameApp.viewManager.worldCanvasTf,//设置到世界画布
        Sorting_Order = 2,
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
      GameApp.fightWorldManager.ChangeState(GameState.FightEnter);

      GameApp.viewManager.Open(ViewType.FightView);
      GameApp.viewManager.Open(ViewType.FightSelectHeroView);
    }
  }
}
