using UnityEngine;
using UnityEngine.SceneManagement;

namespace BattleFlagGameStudy
{
  //加载场景的控制类
  public class LoadingController : BaseController
  {
    AsyncOperation asyncOperation;
    public LoadingController() : base()
    {
      GameApp.viewManager.Register(ViewType.LoadingView, new ViewInfo()
      {
        prefabName = "LoadingView",
        controller = this,
        parentTf = GameApp.viewManager.canvasTf
      });

      InitModuleEvent();
    }

    public override void InitModuleEvent()
    {
      RegisterFunc(Defines.LoadingScene, LoadSceneCallBack);
    }

    //加载场景的回调函数
    private void LoadSceneCallBack(System.Object[] args)
    {
      LoadingModel loadingModel = args[0] as LoadingModel;

      SetModel(loadingModel);

      //打开加载界面
      GameApp.viewManager.Open(ViewType.LoadingView);

      //加载场景
      asyncOperation = SceneManager.LoadSceneAsync(loadingModel.SceneName);

      asyncOperation.completed += OnLoadedEndCallBack;
    }

    private void OnLoadedEndCallBack(AsyncOperation operation)
    {
      asyncOperation.completed -= OnLoadedEndCallBack;
      GameApp.viewManager.Close(ViewType.LoadingView); //关闭加载界面

      GetModel<LoadingModel>().callback?.Invoke(); //执行回调

    }
  }
}
