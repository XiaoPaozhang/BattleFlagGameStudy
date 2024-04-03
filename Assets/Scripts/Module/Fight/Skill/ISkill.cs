using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  public interface ISkill
  {
    SkillProperty skillProperty { get; set; }

    void ShowSkillArea();

    void HideSkillArea();
  }
}
