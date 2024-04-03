using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BattleFlagGameStudy
{
  public class SelectOptionView : BaseView
  {
    Dictionary<int, OptionItem> opItems;//存储选项
    public override void InitData()
    {
      base.InitData();

      opItems = new Dictionary<int, OptionItem>();
      FightModel fightModel = Controller.GetModel<FightModel>();

      List<OptionData> ops = fightModel.options;

      Transform tf = Find("bg/grid").transform;
      GameObject prefabObj = Find("bg/grid/item");

      foreach (OptionData op in ops)
      {
        GameObject obj = Instantiate(prefabObj, tf);
        obj.SetActive(false);

        OptionItem item = obj.AddComponent<OptionItem>();
        item.Init(op);

        opItems.Add(op.Id, item);
      }
    }

    public override void Open(params object[] args)
    {
      base.Open(args);

      //传入两个参数 一个是英雄的Event字符串 对应的 id 需要切割
      //第二个参数 是 角色的位置
      //Event 1001-1002-1005
      string[] evtArr = args[0].ToString().Split('-');
      Find("bg/grid").transform.position = (Vector2)args[1];

      foreach (KeyValuePair<int, OptionItem> item in opItems)
      {
        item.Value.gameObject.SetActive(false);
      }

      for (int i = 0; i < evtArr.Length; i++)
      {
        opItems[int.Parse(evtArr[i])].gameObject.SetActive(true);
      }
    }
  }
}
