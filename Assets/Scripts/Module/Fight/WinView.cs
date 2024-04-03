
using UnityEngine.UI;

namespace BattleFlagGameStudy
{
  public class WinView : BaseView
  {
    protected override void OnStart()
    {
      base.OnStart();

      Find<Button>("bg/okBtn").onClick.AddListener(() =>
      {
        GameApp.fightWorldManager.ReLoadRes();
        GameApp.viewManager.CloseAll();

        //切换场景
        LoadingModel load = new LoadingModel();
        load.SceneName = "map";
        load.callback = () =>
        {
          GameApp.soundManager.PlayBGM("mapbgm");
          GameApp.viewManager.Open(ViewType.SelectLevelView);
        };

        Controller.ApplyControllerFunc(ControllerType.Loading, Defines.LoadingScene, load);
      });
    }
  }
}
