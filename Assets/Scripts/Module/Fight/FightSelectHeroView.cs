using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  /// <summary>
  /// 选择英雄面板
  /// </summary>
  public class FightSelectHeroView : BaseView
  {
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
