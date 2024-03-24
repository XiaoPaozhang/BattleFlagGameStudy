using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BattleFlagGameStudy
{
  //选择关卡信息界面
  public class SelectLevelView : BaseView
  {
    protected override void OnStart()
    {
      base.OnStart();

      Find<Button>("close").onClick.AddListener(OnCloseBtn);
    }

    //返回开始场景
    private void OnCloseBtn()
    {
      GameApp.viewManager.Close(ViewId);//关掉自己

      LoadingModel loadingModel = new LoadingModel();
      loadingModel.SceneName = "game";
      loadingModel.callback = () =>
      {
        //打开开始界面
        Controller.ApplyControllerFunc(ControllerType.GameUI, Defines.OpenStartView);
      };
      Controller.ApplyControllerFunc(ControllerType.Loading, Defines.LoadingScene, loadingModel);
    }
  }
}
