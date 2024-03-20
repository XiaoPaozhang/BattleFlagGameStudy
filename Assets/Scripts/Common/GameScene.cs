using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;

namespace BattleFlagGameStudy
{
  public class GameScene : MonoBehaviour
  {
    public Texture2D mouseTxt;//鼠标图片
    float deltaTime;
    void Awake()
    {
      GameApp.Instance.OnInit();
    }

    void Start()
    {
      Cursor.SetCursor(mouseTxt, Vector2.zero, CursorMode.Auto);//设置鼠标图片
      GameApp.soundManager.PlayBGM("login");//播放登录音乐

      RegisterModule();//注册游戏中的控制器

      InitModule();//初始化所有游戏模块
    }

    //注册控制器
    void RegisterModule()
    {
      GameApp.controllerManager.Register(ControllerType.GameUI, new GameUIController());
      GameApp.controllerManager.Register(ControllerType.Game, new GameController());
    }

    void InitModule()
    {
      GameApp.controllerManager.InitAllModules();
    }

    void Update()
    {
      deltaTime = Time.deltaTime;
      GameApp.Instance.OnUpdate(deltaTime);
    }
  }
}
