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
      Find<Button>("level/fightBtn").onClick.AddListener(OnFightBtnClick);
    }

    //返回开始场景
    private void OnCloseBtn()
    {
      GameApp.viewManager.Close(ViewId);//关掉自己

      LoadingModel loadingModel = new LoadingModel();
      loadingModel.SceneName = "game";
      loadingModel.callback = () =>
      {
        GameApp.soundManager.PlayBGM("login");//播放登录音乐
        //打开开始界面
        Controller.ApplyControllerFunc(ControllerType.GameUI, Defines.OpenStartView);
      };
      Controller.ApplyControllerFunc(ControllerType.Loading, Defines.LoadingScene, loadingModel);
    }

    //显示关卡详情ui部分
    public void ShowLevelDes()
    {
      Find("level").SetActive(true);
      LevelData current = Controller.GetModel<LevelModel>().currentLevelData;
      Find<Text>("level/name/txt").text = current.Name;
      Find<Text>("level/des/txt").text = current.Des;
    }

    //隐藏
    public void HideLevelDes()
    {
      Find("level").SetActive(false);
    }

    //进行战斗,切换为战斗场景
    public void OnFightBtnClick()
    {
      GameApp.viewManager.Close(ViewId);//关掉自己


      LoadingModel loadingModel = new LoadingModel();
      loadingModel.SceneName = Controller.GetModel<LevelModel>().currentLevelData.SceneName;//切换到战斗场景
      loadingModel.callback = () =>
      {
        GameApp.soundManager.PlayBGM("fightbgm");//播放战斗音乐

        //摄像机位置重置
        GameApp.cameraManager.ResetPosition();

        //打开战斗界面之后执行
        Controller.ApplyControllerFunc(ControllerType.Fight, Defines.OnBeginFight);
      };
      Controller.ApplyControllerFunc(ControllerType.Loading, Defines.LoadingScene, loadingModel);
    }
  }
}
