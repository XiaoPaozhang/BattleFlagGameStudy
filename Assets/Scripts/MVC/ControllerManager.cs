using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BattleFlagGameStudy
{
  public class ControllerManager
  {
    private Dictionary<int, BaseController> _controllerDic;
    public ControllerManager()
    {
      _controllerDic = new Dictionary<int, BaseController>();
    }

    public void Register(ControllerType type, BaseController ctrl)
    {
      Register((int)type, ctrl);
    }

    public void Register(int controllerKey, BaseController controller)
    {
      if (!_controllerDic.ContainsKey(controllerKey))
      {
        _controllerDic.Add(controllerKey, controller);
      }
    }

    public void InitAllModules()
    {
      foreach (var item in _controllerDic)
      {
        item.Value.Init();
      }
    }

    public void UnRegister(int controllerKey)
    {
      if (_controllerDic.ContainsKey(controllerKey))
      {
        _controllerDic.Remove(controllerKey);
      }
    }

    public void Clear()
    {
      _controllerDic.Clear();
    }

    public void ClearAllControllers()
    {
      List<int> keys = _controllerDic.Keys.ToList();
      foreach (int i in keys)
      {
        _controllerDic[keys[i]].Destroy();
        _controllerDic.Remove(keys[i]);
      }
    }

    public void ApplyFunc(int controllerKey, string eventName, Object[] args)
    {
      if (_controllerDic.ContainsKey(controllerKey))
      {
        _controllerDic[controllerKey].ApplyFunc(eventName, args);
      }
    }

    public BaseModel GetControllerModel(int controllerKey)
    {
      if (_controllerDic.ContainsKey(controllerKey))
      {
        return _controllerDic[controllerKey].GetModel();
      }
      else
      {
        return null;
      }
    }
  }
}
