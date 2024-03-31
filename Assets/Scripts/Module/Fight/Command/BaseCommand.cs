using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  /// <summary>
  /// 命令基类 (可派生 移动 使用技能 等)
  /// </summary>
  public class BaseCommand
  {
    public ModelBase model;//命令的对象
    protected bool isFinish;//命令是否完成
    public BaseCommand(ModelBase model)
    {
      this.model = model;
      isFinish = false;
    }

    public virtual bool Update(float dt)
    {
      return isFinish;
    }

    //执行命令
    public virtual void Do()
    {
    }

    //撤销命令
    public virtual void UnDo()
    {
    }
  }
}