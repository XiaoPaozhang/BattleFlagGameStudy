using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  public class FightController : BaseController
  {
    public FightController() : base()
    {
      SetModel(new FightModel(this));

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

      GameApp.viewManager.Register(ViewType.TipView, new ViewInfo()
      {
        prefabName = "TipView",
        controller = this,
        parentTf = GameApp.viewManager.canvasTf,
        Sorting_Order = 2,
      });

      GameApp.viewManager.Register(ViewType.HeroDesView, new ViewInfo()
      {
        prefabName = "HeroDesView",
        controller = this,
        parentTf = GameApp.viewManager.canvasTf,
        Sorting_Order = 2,
      });

      GameApp.viewManager.Register(ViewType.EnemyDesView, new ViewInfo()
      {
        prefabName = "EnemyDesView",
        controller = this,
        parentTf = GameApp.viewManager.canvasTf,
        Sorting_Order = 2,
      });

      GameApp.viewManager.Register(ViewType.SelectOptionView, new ViewInfo()
      {
        prefabName = "SelectOptionView",
        controller = this,
        parentTf = GameApp.viewManager.canvasTf,
      });

      GameApp.viewManager.Register(ViewType.FightOptionDesView, new ViewInfo()
      {
        prefabName = "FightOptionDesView",
        controller = this,
        parentTf = GameApp.viewManager.canvasTf,
        Sorting_Order = 3
      });

      GameApp.viewManager.Register(ViewType.WinView, new ViewInfo()
      {
        prefabName = "WinView",
        controller = this,
        parentTf = GameApp.viewManager.canvasTf,
        Sorting_Order = 3
      });

      GameApp.viewManager.Register(ViewType.LossView, new ViewInfo()
      {
        prefabName = "LossView",
        controller = this,
        parentTf = GameApp.viewManager.canvasTf,
        Sorting_Order = 3
      });

      InitModuleEvent();
    }

    public override void Init()
    {
      base.Init();

      model.Init();
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
