using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  public class BaseModel : MonoBehaviour
  {
    public BaseController controller;
    public BaseModel(BaseController ctrl)
    {
      this.controller = ctrl;
    }

    public virtual void Init() { }
  }
}
