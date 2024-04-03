using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BattleFlagGameStudy
{
  /// <summary>
  /// 选项按钮的界面(比如:攻击,待机,取消)
  /// </summary>
  public class OptionItem : MonoBehaviour
  {
    OptionData op_data;

    public void Init(OptionData data)
    {
      op_data = data;
    }
    void Start()
    {
      GetComponent<Button>().onClick.AddListener(() =>
      {
        GameApp.messageCenter.PostTempEvent(op_data.EventName);
        GameApp.viewManager.Close(ViewType.SelectOptionView);
      });

      transform.Find("txt").GetComponent<Text>().text = op_data.Name;
    }
  }
}
