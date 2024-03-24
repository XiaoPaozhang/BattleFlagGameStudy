using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  public class LoadingModel : BaseModel
  {
    public string SceneName;//加载场景名称
    public Action callback;//加载完成回调
    public LoadingModel() { }
  }
}
