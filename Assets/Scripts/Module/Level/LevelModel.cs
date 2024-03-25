using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  public class LevelData
  {
    public int Id;
    public string Name;
    public string SceneName;
    public string Des;//基本描述
    public bool IsFinished;//是否通关
    public LevelData(Dictionary<string, string> data)
    {
      Id = int.Parse(data["Id"]);
      Name = data["Name"];
      SceneName = data["SceneName"];
      Des = data["Des"];
      IsFinished = false;
    }

  }
  public class LevelModel : BaseModel
  {
    private ConfigData levelConfig;
    Dictionary<int, LevelData> levels;//存放关卡数据
    public LevelData currentLevelData;//当前关卡数据

    public LevelModel()
    {
      levels = new Dictionary<int, LevelData>();
    }

    public override void Init()
    {
      base.Init();

      levelConfig = GameApp.configManager.GetConfigData("level");
      foreach (var item in levelConfig.GetLines())
      {
        LevelData _levelData = new LevelData(item.Value);
        levels.Add(_levelData.Id, _levelData);
      }
    }

    public LevelData GetLevel(int id)
    {
      return levels[id];
    }
  }
}
