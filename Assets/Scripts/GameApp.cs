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
    public static ConfigManager configManager;
    public static CameraManager cameraManager;
    public static MessageCenter messageCenter;
    public override void OnInit()
    {
      base.OnInit();

      soundManager = new SoundManager();
      controllerManager = new ControllerManager();
      viewManager = new ViewManager();
      configManager = new ConfigManager();
      cameraManager = new CameraManager();
      messageCenter = new MessageCenter();
    }
  }
}
