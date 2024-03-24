using System;

namespace BattleFlagGameStudy
{
  /// <summary>
  /// 处理一些游戏通用ui的控制器(设置面板 提示面板 开始游戏面板等在这个控制器注册
  /// </summary>
  public class GameUIController : BaseController
  {
    public GameUIController() : base()
    {
      //注册视图

      //开始游戏视图
      GameApp.viewManager.Register(ViewType.StartView, new ViewInfo()
      {
        prefabName = "StartView",
        controller = this,
        parentTf = GameApp.viewManager.canvasTf
      });

      //设置面板视图
      GameApp.viewManager.Register(ViewType.SetView, new ViewInfo()
      {
        prefabName = "SetView",
        controller = this,
        Sorting_Order = 1,//挡住开始面板,比他层级高
        parentTf = GameApp.viewManager.canvasTf
      });

      //提示面板视图
      GameApp.viewManager.Register(ViewType.MessageView, new ViewInfo()
      {
        prefabName = "MessageView",
        controller = this,
        Sorting_Order = 999,//挡住设置面板,比他层级高
        parentTf = GameApp.viewManager.canvasTf
      });
      InitModuleEvent();   //初始化模板事件
      InitGlobalEvent();  //初始化全局事件
    }

    public override void InitModuleEvent()
    {
      RegisterFunc(Defines.OpenStartView, OpenStartView);
      RegisterFunc(Defines.OpenSetView, OpenSetView);
      RegisterFunc(Defines.OpenMessageView, OpenMessageView);
    }
    public override void InitGlobalEvent() { }

    private void OpenStartView(Object[] arg)
    {
      GameApp.viewManager.Open(ViewType.StartView, arg);
    }

    private void OpenSetView(Object[] arg)
    {
      GameApp.viewManager.Open(ViewType.SetView, arg);
    }

    public void OpenMessageView(Object[] arg)
    {
      GameApp.viewManager.Open(ViewType.MessageView, arg);
    }
  }
}
