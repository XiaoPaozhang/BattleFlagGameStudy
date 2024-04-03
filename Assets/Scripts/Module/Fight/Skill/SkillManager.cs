using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

namespace BattleFlagGameStudy
{
  public class SkillManager
  {
    private GameTimer timer;
    private Queue<(ISkill skill, List<ModelBase> targets, Action callback)> skills;//技能队列
    public SkillManager()
    {
      timer = new GameTimer();
      skills = new Queue<(ISkill, List<ModelBase>, Action)>();

    }

    //添加技能
    public void AddSkill(ISkill skill, List<ModelBase> targets = null, Action callback = null)
    {
      skills.Enqueue((skill, targets, callback));
    }

    //使用技能
    public void UseSkill(ISkill skill, List<ModelBase> targets, Action callback)
    {
      ModelBase current = (ModelBase)skill;
      //看向一个目标
      if (targets.Count > 0)
      {
        current.LookAtModel(targets[0]);
      }
      current.PlaySound(skill.skillProperty.Sound);
      current.PlayAni(skill.skillProperty.AniName);

      //延迟攻击
      timer.RegisterTimer(skill.skillProperty.AttackTime, () =>
      {
        //技能的最多作用个数
        int atkCount = skill.skillProperty.AttackCount >= targets.Count ? targets.Count : skill.skillProperty.AttackCount;

        for (int i = 0; i < atkCount; i++)
        {
          targets[i].GetHit(skill);
        }
      });

      //技能的持续时长
      timer.RegisterTimer(skill.skillProperty.Time, () =>
      {
        //回到待机
        current.PlayAni("idle");
        callback?.Invoke();
      }
      );
    }

    public void Update(float dt)
    {
      timer.OnUpdate(dt);

      if (timer.GetTimerCount() == 0 && skills.Count > 0)
      {
        //下一个使用的技能
        var nextSkill = skills.Dequeue();
        if (nextSkill.targets != null)
        {
          UseSkill(nextSkill.skill, nextSkill.targets, nextSkill.callback);//使用技能
        }
      }
    }

    //检测技能是否正在跑
    public bool IsRunningSkill()
    {
      if (timer.GetTimerCount() == 0 && skills.Count == 0)
      {
        return false;
      }
      else
      {
        return true;
      }
    }

    //清空技能
    public void ClearSkills()
    {
      timer.Break();
      skills.Clear();
    }
  }
}
