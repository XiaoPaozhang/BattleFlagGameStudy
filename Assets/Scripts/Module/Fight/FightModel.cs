using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  //选项
  public class OptionData
  {
    public int Id;
    public string EventName;
    public string Name;
  }

  /// <summary>
  /// 战斗用的数据
  /// </summary>
  public class FightModel : BaseModel
  {
    public List<OptionData> options;
    public ConfigData optionConfigData;

    public FightModel(BaseController controller) : base(controller)
    {
      options = new List<OptionData>();
    }

    public override void Init()
    {
      base.Init();

      optionConfigData = GameApp.configManager.GetConfigData("option");
      foreach (KeyValuePair<int, Dictionary<string, string>> item in optionConfigData.GetLines())
      {
        OptionData opData = new OptionData();
        opData.Id = int.Parse(item.Value["Id"]);
        opData.EventName = item.Value["EventName"];
        opData.Name = item.Value["Name"];
        options.Add(opData);
      }
    }
  }
}
