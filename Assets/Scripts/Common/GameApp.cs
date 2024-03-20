namespace BattleFlagGameStudy
{
  /// <summary>
  /// 游戏管理器都放在这
  /// </summary>
  public class GameApp : Singleton<GameApp>
  {
    public static SoundManager soundManager;
    public static ControllerManager controllerManager;
    public static ViewManager viewManager;
    public override void OnInit()
    {
      soundManager = new SoundManager();
      controllerManager = new ControllerManager();
      viewManager = new ViewManager();
    }
  }
}
