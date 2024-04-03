using System.Diagnostics.Contracts;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BattleFlagGameStudy
{
  public static class SkillHelper
  {
    public static bool IsModelInSkillArea(this ISkill skill, ModelBase targetModelBase)
    {
      ModelBase currentModel = (ModelBase)skill;
      if (currentModel.GetDis(targetModelBase) <= skill.skillProperty.AttackRange)
        return true;

      return false;
    }

    //获得技能的作用目标
    public static List<ModelBase> GetTarget(this ISkill skill)
    {
      //0：以鼠标指向的目标为目标
      //1：在攻击范围内的所有目标
      //2.在攻击范围内的英雄的目标

      switch (skill.skillProperty.Target)
      {
        case 0:
          return GetTarget_0(skill);
        case 1:
          return GetTarget_1(skill);
        case 2:
          return GetTarget_2(skill);
      }

      return null;
    }

    //0：以鼠标指向的目标为目标
    public static List<ModelBase> GetTarget_0(ISkill skill)
    {
      List<ModelBase> results = new List<ModelBase>();
      Collider2D collider2d = Tools.ScreenPointToRay2D(Camera.main, Input.mousePosition);
      if (collider2d != null)
      {
        ModelBase target = collider2d.GetComponent<ModelBase>();
        if (target != null)
        {
          //技能的目标类型 跟 技能指向的目标类型 和配置表一致
          if (skill.skillProperty.TargetType == target.Type)
          {
            results.Add(target);
          }
        }
      }

      return results;
    }

    //1:在攻击范围内的所有目标
    public static List<ModelBase> GetTarget_1(ISkill skill)
    {
      List<ModelBase> results = new List<ModelBase>();
      for (int i = 0; i < GameApp.fightWorldManager.heroes.Count; i++)
      {
        //找到技能范围内的目标
        if (skill.IsModelInSkillArea(GameApp.fightWorldManager.heroes[i]))
        {
          results.Add(GameApp.fightWorldManager.heroes[i]);
        }
      }

      for (int i = 0; i < GameApp.fightWorldManager.enemies.Count; i++)
      {
        //找到技能范围内的目标
        if (skill.IsModelInSkillArea(GameApp.fightWorldManager.enemies[i]))
        {
          results.Add(GameApp.fightWorldManager.enemies[i]);
        }
      }
      return results;
    }

    //2.在攻击范围内的英雄的目标
    public static List<ModelBase> GetTarget_2(ISkill skill)
    {
      List<ModelBase> results = new List<ModelBase>();
      for (int i = 0; i < GameApp.fightWorldManager.heroes.Count; i++)
      {
        //找到技能范围内的目标
        if (skill.IsModelInSkillArea(GameApp.fightWorldManager.heroes[i]))
        {
          results.Add(GameApp.fightWorldManager.heroes[i]);
        }
      }

      return results;
    }
  }
}
