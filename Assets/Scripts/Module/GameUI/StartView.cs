

using UnityEngine;
using UnityEngine.UI;

namespace BattleFlagGameStudy
{
  public class StartView : BaseView
  {
    protected override void OnAwake()
    {
      base.OnAwake();

      Find<Button>("startBtn").onClick.AddListener(OnStartBtn);
      Find<Button>("setBtn").onClick.AddListener(OnSetBtn);
      Find<Button>("quitBtn").onClick.AddListener(OnQuitBtn);
    }

    //开始游戏
    private void OnStartBtn()
    {
      GameApp.viewManager.Close(ViewId);//关掉自己

      LoadingModel loadingModel = new LoadingModel();
      loadingModel.SceneName = "map";
      loadingModel.callback = () =>
      {
        //打开选择界面
        Controller.ApplyControllerFunc(ControllerType.Level, Defines.OpenSelectLevelView);
      };
      Controller.ApplyControllerFunc(ControllerType.Loading, Defines.LoadingScene, loadingModel);

    }

    //打开设置
    private void OnSetBtn()
    {
      ApplyFunc(Defines.OpenSetView);
    }

    //退出游戏
    private void OnQuitBtn()
    {
      Controller.ApplyControllerFunc(ControllerType.GameUI, Defines.OpenMessageView, new MessageInfo()
      {
        okCallback = () =>
        {
          Application.Quit();
        },
        msgTxt = "确定退出游戏吗？"
      });
    }
  }
}
