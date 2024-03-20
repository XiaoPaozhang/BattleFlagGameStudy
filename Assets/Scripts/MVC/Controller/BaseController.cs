using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  public class BaseController
  {
    protected BaseModel model;
    private Dictionary<string, Action<object[]>> _eventDic;//事件字典

    public BaseController()
    {
      _eventDic = new Dictionary<string, Action<object[]>>();
    }

    //注册后调用的初始化函数(要所有控制器初始化后执行)
    public virtual void Init()
    {

    }

    public virtual void OnLoadView(IBaseView view) { }

    public virtual void OpenView(IBaseView view) { }
    public virtual void CloseView(IBaseView view) { }

    public void RegisterFunc(string eventName, Action<object[]> callback)
    {
      if (_eventDic.ContainsKey(eventName))
      {
        _eventDic[eventName] += callback;
      }
      else
      {
        _eventDic.Add(eventName, callback);
      }
    }
    public void UnRegisterFunc(string eventName)
    {
      if (_eventDic.ContainsKey(eventName))
      {
        _eventDic.Remove(eventName);
      }
      else
      {
        Debug.LogError("Event not found: " + eventName);
      }
    }

    public void ApplyFunc(string eventName, params object[] args)
    {
      if (_eventDic.TryGetValue(eventName, out Action<object[]> callback))
      {
        callback?.Invoke(args);
      }
      else
      {
        Debug.LogError("Event not found: " + eventName);
      }
    }

    public void ApplyControllerFunc(int controllerKey, string eventName, params object[] args)
    {
      GameApp.controllerManager.ApplyFunc(controllerKey, eventName, args);
    }

    public void ApplyControllerFunc(ControllerType type, string eventName, params object[] args)
    {
      ApplyControllerFunc((int)type, eventName, args);
    }

    public void SetModel(BaseModel model)
    {
      this.model = model;
    }

    public BaseModel GetModel()
    {
      return model;
    }

    public T GetModel<T>() where T : BaseModel
    {
      return model as T;
    }

    public BaseModel GetControllerModel(int controllerKey)
    {
      // 实现控制器间通信
      return GameApp.controllerManager.GetControllerModel(controllerKey);
    }

    public virtual void Destroy()
    {
      // 实现销毁逻辑
      RemoveModuleEvent();
      RemoveGlobalEvent();
    }

    //初始化模板事件
    public virtual void InitModuleEvent()
    {
    }

    public virtual void RemoveModuleEvent()
    { }

    public virtual void InitGlobalEvent() { }

    public virtual void RemoveGlobalEvent() { }
  }
}
