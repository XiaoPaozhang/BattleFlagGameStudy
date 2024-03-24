using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  public class CameraManager
  {
    private Transform cameraTransform;
    private Vector3 prePosition;
    public CameraManager()
    {
      cameraTransform = Camera.main.transform;
      prePosition = cameraTransform.position;
    }

    public void SetPosition(Vector3 position)
    {
      //不修改z轴
      position.z = cameraTransform.position.z;
      cameraTransform.position = position;
    }

    public void ResetPosition()
    {
      cameraTransform.position = prePosition;
    }
  }
}
