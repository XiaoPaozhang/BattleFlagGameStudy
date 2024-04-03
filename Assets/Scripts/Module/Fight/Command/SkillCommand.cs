using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  public class SkillCommand : BaseCommand
  {
    ISkill skill;
    public SkillCommand(ModelBase model) : base(model)
    {
      skill = model as ISkill;
    }

    public override void Do()
    {
      base.Do();
      List<ModelBase> results = skill.GetTarget();
      if (results.Count > 0)
      {
        //有目标
        GameApp.skillManager.AddSkill(skill, results);
      }
    }

    public override bool Update(float dt)
    {
      if (GameApp.skillManager.IsRunningSkill() == false)
      {
        model.HasMovementCompleted = true;
        return true;
      }
      else
      {
        return false;
      }
    }
  }
}
