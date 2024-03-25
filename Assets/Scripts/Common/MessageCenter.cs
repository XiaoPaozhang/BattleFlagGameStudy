using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  public class MessageCenter
  {
    private Dictionary<string, Action<object>> messageDic;  //存储普通的消息字典
    private Dictionary<string, Action<object>> tempMsgDic;  //存储临时消息字典,施行后置空
    private Dictionary<object, Dictionary<string, Action<object>>> objMsgDic;  //存储对象消息字典

    public MessageCenter()
    {
      messageDic = new Dictionary<string, Action<object>>();
      tempMsgDic = new Dictionary<string, Action<object>>();
      objMsgDic = new Dictionary<object, Dictionary<string, Action<object>>>();
    }

    public void AddEvent(string msgName, Action<object> callback)
    {
      if (messageDic.ContainsKey(msgName))
      {
        messageDic[msgName] += callback;
      }
      else
      {
        messageDic.Add(msgName, callback);
      }
    }
    //添加对象事件
    public void AddEvent(object listenerObj, string eventName, Action<object> callback)
    {
      if (objMsgDic.TryGetValue(listenerObj, out Dictionary<string, Action<object>> dic))
      {
        if (dic.ContainsKey(eventName))
        {
          dic[eventName] += callback;
        }
        else
        {
          dic.Add(eventName, callback);
        }
      }
      else
      {
        Dictionary<string, Action<object>> _tempDic = new Dictionary<string, Action<object>>();
        _tempDic.Add(eventName, callback);
        objMsgDic.Add(listenerObj, _tempDic);
      }
    }

    //删除事件,如果有事件就删除,如果事件为空,就从字典里删除
    public void RemoveEvent(string msgName, Action<object> callback)
    {
      if (messageDic.ContainsKey(msgName))
      {
        messageDic[msgName] -= callback;
        if (messageDic[msgName] == null)
        {
          messageDic.Remove(msgName);
        }
      }
    }

    public void RemoveEvent(object listenerObj, string eventName, Action<object> callback)
    {
      if (objMsgDic.TryGetValue(listenerObj, out Dictionary<string, Action<object>> dic))
      {
        if (dic.ContainsKey(eventName))
        {
          dic[eventName] -= callback;
          if (dic[eventName] == null)
          {
            dic.Remove(eventName);
          }
        }
      }
    }

    public void RemoveObjAllEvent(object listenerObj)
    {
      if (objMsgDic.ContainsKey(listenerObj))
      {
        objMsgDic.Remove(listenerObj);
      }
    }

    //执行事件
    public void PostEvent(string eventName, object arg = null)
    {
      if (messageDic.TryGetValue(eventName, out Action<object> action))
      {
        action?.Invoke(arg);
      }
    }



    //执行对象的监听事件
    public void PostEvent(object listenerObj, string eventName, object arg = null)
    {
      if (objMsgDic.TryGetValue(listenerObj, out Dictionary<string, Action<object>> dic))
      {
        if (dic.TryGetValue(eventName, out Action<object> action))
        {
          action?.Invoke(arg);
        }
      }
    }

    public void AddTempEvent(string msgName, Action<object> callback)
    {
      if (tempMsgDic.ContainsKey(msgName))
      {
        tempMsgDic[msgName] += callback;
      }
      else
      {
        tempMsgDic.Add(msgName, callback);
      }
    }

    //执行临时事件
    public void PostTempEvent(string eventName, object arg = null)
    {
      if (tempMsgDic.TryGetValue(eventName, out Action<object> action))
      {
        action?.Invoke(arg);
        tempMsgDic.Clear();//执行完后清空临时消息字典
      }
    }
  }
}
