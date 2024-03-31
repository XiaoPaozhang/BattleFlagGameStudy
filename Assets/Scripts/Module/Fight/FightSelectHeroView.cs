using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BattleFlagGameStudy
{
  /// <summary>
  /// 选择英雄面板
  /// </summary>
  public class FightSelectHeroView : BaseView
  {
    protected override void OnAwake()
    {
      base.OnAwake();

      Find<Button>("bottom/startBtn").onClick.AddListener(OnFightBtn);
    }

    //选择完英雄,开始进入到玩家回合
    private void OnFightBtn()
    {
      //如果一个英雄都没选,要提示玩家
      if (GameApp.fightWorldManager.heroes.Count == 0)
      {
        Debug.Log("未选择英雄");
        return;
      }
      else
      {
        GameApp.viewManager.Close(ViewId);

        //切换到 玩家回合
        GameApp.fightWorldManager.ChangeState(GameState.Player);
      }
    }

    public override void Open(params object[] args)
    {
      base.Open(args);

      GameObject prefabObj = Find("bottom/grid/item");
      Transform gridTf = Find("bottom/grid").transform;
      for (int i = 0; i < GameApp.gameDataManager.heros.Count; i++)
      {
        Dictionary<string, string> data = GameApp.configManager.GetConfigData("player").GetDataById(GameApp.gameDataManager.heros[i]);

        GameObject heroObj = Object.Instantiate(prefabObj, gridTf);

        heroObj.SetActive(true);

        HeroItem heroItem = heroObj.AddComponent<HeroItem>();
        heroItem.Init(data);
      }
    }
  }
}
