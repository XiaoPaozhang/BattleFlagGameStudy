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
    public static GameTimer gameTimer;
    public static FightWorldManager fightWorldManager;
    public static MapManager mapManager;
    public static GameDataManager gameDataManager;
    public static UserInputManager userInputManager;
    public static CommandManager commandManager;
    public static SkillManager skillManager;

    public override void OnInit()
    {
      base.OnInit();

      soundManager = new SoundManager();
      controllerManager = new ControllerManager();
      viewManager = new ViewManager();
      configManager = new ConfigManager();
      cameraManager = new CameraManager();
      messageCenter = new MessageCenter();
      gameTimer = new GameTimer();
      fightWorldManager = new FightWorldManager();//战斗状态机
      mapManager = new MapManager(); //地图管理器
      gameDataManager = new GameDataManager();
      userInputManager = new UserInputManager();
      commandManager = new CommandManager(); //命令管理器
      skillManager = new SkillManager(); //技能管理器
    }

    public override void OnUpdate(float deltaTime)
    {
      base.OnUpdate(deltaTime);

      userInputManager.Update(deltaTime);
      gameTimer.OnUpdate(deltaTime);
      fightWorldManager.Update(deltaTime);
      commandManager.Update(deltaTime);
      skillManager.Update(deltaTime);

    }
  }
}
