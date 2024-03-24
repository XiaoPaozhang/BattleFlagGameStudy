using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BattleFlagGameStudy
{
  //跳转场景后,当前脚本不删除
  public class GameScene : MonoBehaviour
  {
    public Texture2D mouseTxt;//鼠标图片
    float deltaTime;
    private static bool isLoaded = false;
    void Awake()
    {
      if (isLoaded)
      {
        Destroy(gameObject);
      }
      else
      {
        isLoaded = true;

        //手动添加eventSystem
        GameObject eventSystem = new GameObject("EventSystem");
        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<StandaloneInputModule>();
        eventSystem.transform.SetParent(transform);

        DontDestroyOnLoad(gameObject);
        GameApp.Instance.OnInit();
      }
    }

    void Start()
    {
      Cursor.SetCursor(mouseTxt, Vector2.zero, CursorMode.Auto);//设置鼠标图片

      //注册配置表
      RegisterConfig();

      //加载所有配置
      GameApp.configManager.LoadAllConfig();

      //测试配置表
      // ConfigData tempData = GameApp.configManager.GetConfigData("enemy");
      // string name = tempData.GetDataById(10001)["Name"];
      // Debug.Log(name);

      GameApp.soundManager.PlayBGM("login");//播放登录音乐

      RegisterModule();//注册游戏中的控制器

      InitModule();//初始化所有游戏模块
    }

    //注册控制器
    void RegisterModule()
    {
      GameApp.controllerManager.Register(ControllerType.GameUI, new GameUIController());
      GameApp.controllerManager.Register(ControllerType.Game, new GameController());
      GameApp.controllerManager.Register(ControllerType.Loading, new LoadingController());
      GameApp.controllerManager.Register(ControllerType.Level, new LevelController());
    }

    void InitModule()
    {
      GameApp.controllerManager.InitAllModules();
    }

    //注册配置表
    void RegisterConfig()
    {
      GameApp.configManager.Register("enemy", new ConfigData("enemy"));
      GameApp.configManager.Register("level", new ConfigData("level"));
      GameApp.configManager.Register("option", new ConfigData("option"));
      GameApp.configManager.Register("player", new ConfigData("player"));
      GameApp.configManager.Register("role", new ConfigData("role"));
      GameApp.configManager.Register("skill", new ConfigData("skill"));
    }
    void Update()
    {
      deltaTime = Time.deltaTime;
      GameApp.Instance.OnUpdate(deltaTime);
    }
  }
}
