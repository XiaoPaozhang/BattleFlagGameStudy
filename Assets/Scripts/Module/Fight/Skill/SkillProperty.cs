using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

namespace BattleFlagGameStudy
{
  //技能属性
  public class SkillProperty
  {
    public int Id;
    public string Name;
    public int Attack;
    public int AttackCount;
    public int AttackRange;
    public int Target;
    public int TargetType;
    public string Sound;
    public string AniName;
    public float Time;//技能持续时长
    public float AttackTime;//检测攻击的时长
    public string AttackEffect;
    public SkillProperty(int id)
    {
      Dictionary<string, string> skillData = GameApp.configManager.GetConfigData("skill").GetDataById(id);

      Id = int.Parse(skillData["Id"]);
      Name = skillData["Name"];
      Attack = int.Parse(skillData["Atk"]);
      AttackCount = int.Parse(skillData["AtkCount"]);
      AttackRange = int.Parse(skillData["Range"]);
      Target = int.Parse(skillData["Target"]);
      TargetType = int.Parse(skillData["TargetType"]);
      Sound = skillData["Sound"];
      AniName = skillData["AniName"];
      Time = float.Parse(skillData["Time"]) * 0.001f;
      AttackTime = float.Parse(skillData["AttackTime"]) * 0.001f;
      AttackEffect = skillData["AttackEffect"];
    }
  }
}
