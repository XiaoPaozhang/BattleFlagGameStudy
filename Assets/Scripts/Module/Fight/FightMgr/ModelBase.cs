using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace BattleFlagGameStudy
{
  public class ModelBase : MonoBehaviour
  {
    public int id;
    public Dictionary<string, string> data;
    public int Step;//行动力
    public int Attack;//攻击力
    public int Type;//类型
    public int MaxHp;//最大生命值
    public int CurHp;//当前生命值
    public int RowIndex;//行索引
    public int ColIndex;//列索引
    public SpriteRenderer bodySprite;//身体图片渲染组件
    public GameObject stopObj;//停止行动的标记物体
    public Animator ani;//动画组件

    void Awake()
    {
      bodySprite = transform.Find("body").GetComponent<SpriteRenderer>();
      stopObj = transform.Find("stop").gameObject;
      ani = transform.Find("body").GetComponent<Animator>();
    }

    protected virtual void Start()
    {
      AddEvents();
    }

    protected virtual void OnDestroy()
    {
      RemoveEvents();
    }

    protected virtual void AddEvents()
    {
      GameApp.messageCenter.AddEvent(gameObject, Defines.OnSelectEvent, OnSelectCallback);
      GameApp.messageCenter.AddEvent(Defines.OnUnSelectEvent, OnUnSelectCallback);
    }

    protected virtual void RemoveEvents()
    {
      GameApp.messageCenter.RemoveEvent(gameObject, Defines.OnSelectEvent, OnSelectCallback);
      GameApp.messageCenter.RemoveEvent(Defines.OnUnSelectEvent, OnUnSelectCallback);
    }

    protected virtual void OnSelectCallback(object args)
    {
      Debug.Log($"<color=yellow>{gameObject.name}OnSelectCallback</color>");
      //执行未选中
      GameApp.messageCenter.PostEvent(Defines.OnUnSelectEvent);

      bodySprite.color = Color.blue;
    }

    protected virtual void OnUnSelectCallback(object args)
    {
      Debug.Log($"<color=red>{gameObject.name}OnUnSelectCallback</color>");
      bodySprite.color = Color.white;
    }
  }
}
