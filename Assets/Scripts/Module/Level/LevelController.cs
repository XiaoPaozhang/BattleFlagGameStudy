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
      SetModel(new LevelModel());

      GameApp.viewManager.Register(ViewType.SelectLevelView, new ViewInfo()
      {
        prefabName = "SelectLevelView",
        parentTf = GameApp.viewManager.canvasTf,
        controller = this,
      });

      InitModuleEvent();
      InitGlobalEvent();
    }
    public override void Init()
    {
      base.Init();

      model.Init();
    }

    //注册全局事件
    public override void InitGlobalEvent()
    {
      base.InitGlobalEvent();

      GameApp.messageCenter.AddEvent(Defines.ShowLevelDesEvent, OnShowLevelDesCallback);
      GameApp.messageCenter.AddEvent(Defines.HideLevelDesEvent, OnHideLevelDesCallback);
    }


    //删除全局事件
    public override void RemoveGlobalEvent()
    {
      base.RemoveGlobalEvent();

      GameApp.messageCenter.RemoveEvent(Defines.ShowLevelDesEvent, OnShowLevelDesCallback);
      GameApp.messageCenter.RemoveEvent(Defines.HideLevelDesEvent, OnHideLevelDesCallback);
    }

    private void OnShowLevelDesCallback(object obj)
    {
      LevelModel levelModel = GetModel<LevelModel>();
      levelModel.currentLevelData = levelModel.GetLevel(int.Parse(obj.ToString()));

      SelectLevelView selectLevelView = GameApp.viewManager.GetView<SelectLevelView>((int)ViewType.SelectLevelView);
      selectLevelView.ShowLevelDes();
    }

    private void OnHideLevelDesCallback(object obj)
    {
      SelectLevelView selectLevelView = GameApp.viewManager.GetView<SelectLevelView>((int)ViewType.SelectLevelView);
      selectLevelView.HideLevelDes();
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
