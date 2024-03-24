using UnityEngine;

namespace BattleFlagGameStudy
{
  //选关界面人物简单的控制器脚本
  public class PlayerController : MonoBehaviour
  {
    public float moveSpeed = 6f; //移动速度
    public float maxMoveRange = 24f; //人物最大移动范围

    public float minMoveRange = -32f; //人物最小移动范围
    public float scaleX; //人物缩放比例

    private float h; //水平方向输入

    public Animator ani;
    void Start()
    {
      ani = GetComponent<Animator>();
      scaleX = transform.localScale.x;
    }

    void Update()
    {
      InputReload();//输入记录

      TryFlip();//尝试翻转

      Move();//移动

      PlayAnimation();//播放动画
    }
    void LateUpdate()
    {
      CameraFollow();//摄像机跟随
    }

    private void Move()
    {
      Vector3 unitMovement = Vector3.right * h * moveSpeed * Time.deltaTime;//单位移动向量
      Vector3 mewPosition = transform.position + unitMovement;//移动后的新位置
      mewPosition.x = Mathf.Clamp(mewPosition.x, minMoveRange, maxMoveRange);//限制移动范围
      transform.position = mewPosition;//移动
    }

    public void InputReload()
    {
      h = Input.GetAxis("Horizontal");
    }
    public void PlayAnimation()
    {
      if (h == 0)
      {
        ani.Play("idle");
      }
      else
      {
        ani.Play("move");
      }
    }
    private void TryFlip()
    {
      if ((h > 0 && scaleX < 0) || (h < 0 && scaleX > 0))
      {
        Vector3 scale = transform.localScale;
        scaleX *= -1;
        scale.x = scaleX;
        transform.localScale = scale;
      }
    }

    private void CameraFollow()
    {
      GameApp.cameraManager.SetPosition(transform.position);//设置摄像机位置为玩家位置
    }
  }
}
