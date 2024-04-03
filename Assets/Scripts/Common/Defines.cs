namespace BattleFlagGameStudy
{
  /// <summary>
  /// 常量类
  /// </summary>
  public class Defines
  {
    //控制器相关的事件字符串
    public static readonly string OpenStartView = "OpenStartView";
    public static readonly string OpenSetView = "OpenSetView";
    public static readonly string OpenMessageView = "OpenMessageView";
    public static readonly string OpenSelectLevelView = "OpenSelectLevelView";
    public static readonly string OnBeginFight = "OnBeginFight";
    public static readonly string LoadingScene = "LoadingScene";


    //全局事件相关
    public static readonly string ShowLevelDesEvent = "ShowLevelDesEvent";//展示关卡详情ui部分
    public static readonly string HideLevelDesEvent = "HideLevelDesEvent";//隐藏关卡详情ui部分

    public static readonly string OnSelectEvent = "OnSelectEvent";//选中事件
    public static readonly string OnUnSelectEvent = "OnUnSelectEvent";//未选中事件

    //option

    public static readonly string OnAttackEvent = "OnAttackEvent";//攻击
    public static readonly string OnIdleEvent = "OnIdleEvent"; //待机
    public static readonly string OnCancelEvent = "OnCancelEvent";//待机
    public static readonly string OnRemoveHeroToSceneEvent = "OnRemoveHeroToSceneEvent";//撤销

  }
}
