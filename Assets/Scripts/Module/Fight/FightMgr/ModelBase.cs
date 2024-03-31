using System.Reflection.Emit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace BattleFlagGameStudy
{
  /// <summary>
  /// 敌人和英雄等的模型基类
  /// </summary>
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
    public SpriteRenderer bodySprite;//身体渲染的组件
    public GameObject stopObj;//停止行动的标记物体
    public Animator ani;//动画组件

    private bool hasMovementCompleted;//是否移动完成
    public bool HasMovementCompleted
    {
      get => hasMovementCompleted;
      set
      {
        stopObj.SetActive(!value);

        if (value)
        {
          bodySprite.color = Color.gray;
        }
        else
        {
          bodySprite.color = Color.white;
        }

        hasMovementCompleted = value;
      }
    }
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

      GameApp.mapManager.ShowStepGrid(this, Step);
    }

    protected virtual void OnUnSelectCallback(object args)
    {
      Debug.Log($"<color=red>{gameObject.name}OnUnSelectCallback</color>");

      GameApp.mapManager.HideStepGrid(this, Step);
    }

    //转向
    public void Flip()
    {
      Vector3 scale = transform.localScale;
      scale.x *= -1;
      transform.localScale = scale;
    }

    public virtual bool MoveToSpecifiedBlock(int rowIndex, int colIndex, float dt)
    {
      //要去的终点位置
      Vector3 toPos = GameApp.mapManager.GetBlockPos(rowIndex, colIndex);

      toPos.z = transform.position.z;

      if (transform.position.x > toPos.x && transform.lossyScale.x > 0)
      {
        Flip();
      }
      if (transform.position.x < toPos.x && transform.lossyScale.x < 0)
      {
        Flip();
      }

      //如果离目的地很近 返回 true
      if (Vector3.Distance(transform.position, toPos) < 0.02f)
      {
        this.RowIndex = rowIndex;
        this.ColIndex = colIndex;
        transform.position = toPos;
        return true;
      }

      transform.position = Vector3.MoveTowards(transform.position, toPos, dt);
      return false;
    }

    public void PlayAni(string name)
    {
      ani.Play(name);
    }
  }
}
