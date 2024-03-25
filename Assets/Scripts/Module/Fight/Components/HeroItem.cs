using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BattleFlagGameStudy
{
  /// <summary>
  /// 处理英雄图标的脚本
  /// </summary>
  public class HeroItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
  {
    Dictionary<string, string> _data;
    void Start()
    {
      transform.Find("icon").GetComponent<Image>().SetIcon(_data["Icon"]);
    }

    public void Init(Dictionary<string, string> data)
    {
      _data = data;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
      GameApp.viewManager.Open(ViewType.DragHeroView, _data["Icon"]);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
      GameApp.viewManager.Close(ViewType.DragHeroView);
      //检测拖拽后 的 位置是否有block脚本
      Tools.ScreenPointToRay2D(eventData.pressEventCamera, eventData.position, (Collider2D Collider2D) =>
      {
        if (Collider2D != null)
        {
          Block b = Collider2D.GetComponent<Block>();
          if (b != null)
          {
            //有方块
            Destroy(gameObject);//剔除
            //创建英雄物体
            GameApp.fightWorldManager.AddHero(b, _data);
          }
        }
      });
    }

    public void OnDrag(PointerEventData eventData)
    {

    }
  }
}
