using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace BattleFlagGameStudy
{
  /// <summary>
  /// 通过视图信息,打开或关闭视图
  /// </summary>
  public class ViewInfo
  {
    public string prefabName;
    public Transform parentTf;
    public BaseController controller;
    public int Sorting_Order;//显示层级 改变显示排序
  }

  public class ViewManager
  {
    public Transform canvasTf;
    public Transform worldCanvasTf;
    Dictionary<int, IBaseView> _openView;//开启中的视图
    Dictionary<int, IBaseView> _viewCache;//视图缓存
    Dictionary<int, ViewInfo> _views;//注册的视图信息

    public ViewManager()
    {
      canvasTf = GameObject.Find("Canvas").transform;
      worldCanvasTf = GameObject.Find("WorldCanvas").transform;
      _openView = new Dictionary<int, IBaseView>();
      _views = new Dictionary<int, ViewInfo>();
      _viewCache = new Dictionary<int, IBaseView>();
    }

    public void Register(int key, ViewInfo viewInfo)
    {
      if (!_views.ContainsKey(key))
        _views.Add(key, viewInfo);
    }

    public void Register(ViewType viewType, ViewInfo viewInfo)
    {
      Register((int)viewType, viewInfo);
    }

    public void UnRegister(int key)
    {
      if (_views.ContainsKey(key))
        _views.Remove(key);
    }

    public void RemoveView(int key)
    {
      _views.Remove(key);
      _viewCache.Remove(key);
      _openView.Remove(key);
    }

    public void RemoveViewByController(BaseController controller)
    {
      foreach (var item in _views)
      {
        if (item.Value.controller == controller)
        {
          RemoveView(item.Key);
        }
      }
    }

    public bool IsOpen(int key)
    {
      return _openView.ContainsKey(key);
    }

    public IBaseView GetView(int key)
    {
      if (_openView.ContainsKey(key))
        return _openView[key];
      if (_viewCache.ContainsKey(key))
        return _viewCache[key];

      return null;
    }

    public T GetView<T>(int key) where T : class, IBaseView
    {
      IBaseView view = GetView(key);
      if (view != null)
      {
        return view as T;
      }
      return null;
    }

    public void Destroy(int key)
    {
      IBaseView oldView = GetView(key);
      if (oldView != null)
      {
        UnRegister(key);
        oldView.DestroyView();//执行被销毁的视图的销毁方法
        _viewCache.Remove(key);
        _openView.Remove(key);
      }

    }

    public void Close(ViewType viewType, params object[] args)
    {
      Close((int)viewType, args);
    }

    public void Close(int key, params object[] args)
    {
      if (IsOpen(key) == false)
      {
        return;
      }

      IBaseView oldView = GetView(key);
      if (oldView != null)
      {
        _openView.Remove(key);
        oldView.Close(args);
        _views[key].controller.CloseView(oldView);
      }
    }

    public void Open(ViewType viewType, params object[] args)
    {
      Open((int)viewType, args);
    }
    public void Open(int key, params object[] args)
    {
      IBaseView view = GetView(key);
      ViewInfo viewInfo = this._views[key];
      if (view == null)
      {
        //不存在的视图进行资源加载,实例化
        GameObject uiObj = Object.Instantiate(Resources.Load<GameObject>($"View/{viewInfo.prefabName}"), viewInfo.parentTf);
        Canvas canvas = uiObj.GetComponent<Canvas>();
        if (canvas == null)
        {
          canvas = uiObj.AddComponent<Canvas>();
        }
        if (uiObj.GetComponent<GraphicRaycaster>() == null)
        {
          uiObj.AddComponent<GraphicRaycaster>();
        }
        canvas.overrideSorting = true;//可以设置层级
        canvas.sortingOrder = viewInfo.Sorting_Order;//设置层级
        //获取精确类型
        string typeName = ((ViewType)key).ToString();
        string namespaceName = typeof(ViewType).Namespace;
        string assemblyName = typeof(ViewType).Assembly.FullName;
        Type viewType = Type.GetType($"{namespaceName}.{typeName}, {assemblyName}");

        view = uiObj.AddComponent(viewType) as IBaseView;

        view.ViewId = key;
        view.Controller = viewInfo.controller;

        _viewCache.Add(key, view);
        viewInfo.controller.OnLoadView(view);
      }
      //如果已经打开了
      if (this._openView.ContainsKey(key))
      {
        return;
      }
      this._openView.Add(key, view);

      if (view.IsInit())
      {
        view.SetVisible(true);
        view.Open(args);
        viewInfo.controller.OpenView(view);
      }
      else
      {
        view.InitUI();
        view.InitData();
        view.Open(args);
        viewInfo.controller.OpenView(view);
      }
    }
  }
}


