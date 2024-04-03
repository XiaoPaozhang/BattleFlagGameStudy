using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  public class DestroyObj : MonoBehaviour
  {
    public float timer;
    void Start()
    {
      Destroy(gameObject, timer);
    }
  }
}
