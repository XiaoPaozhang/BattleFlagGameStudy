using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BattleFlagGameStudy
{
  public class Enemy : ModelBase, ISkill
  {
    public SkillProperty skillProperty { get; set; }
    private Slider hpSlider;

    protected override void Start()
    {
      base.Start();

      hpSlider = transform.Find("hp/bg").GetComponent<Slider>();

      data = GameApp.configManager.GetConfigData("enemy").GetDataById(id);


      this.Type = int.Parse(data["Type"]);
      this.Attack = int.Parse(data["Attack"]);
      this.Step = int.Parse(data["Step"]);
      this.MaxHp = int.Parse(data["Hp"]);
      this.CurHp = this.MaxHp;
      this.skillProperty = new SkillProperty(int.Parse(data["Skill"]));
    }

    protected override void OnSelectCallback(object args)
    {
      if (GameApp.commandManager.IsRunningCommand)
        return;

      base.OnSelectCallback(args);

      GameApp.viewManager.Open(ViewType.EnemyDesView, this);
    }

    protected override void OnUnSelectCallback(object args)
    {
      base.OnUnSelectCallback(args);

      GameApp.viewManager.Close(ViewType.EnemyDesView);
    }

    public void ShowSkillArea()
    {

    }

    public void HideSkillArea()
    {

    }

    //受伤
    public override void GetHit(ISkill skill)
    {
      base.GetHit(skill);

      //播放受伤音效
      GameApp.soundManager.PlayEffect("hit", transform.position);

      //扣血
      CurHp -= skill.skillProperty.Attack;

      //显示伤害数字
      GameApp.viewManager.ShowHitNum($"-{skill.skillProperty.Attack}", Color.red, transform.position);

      //击中特效
      PlayEffect(skill.skillProperty.AttackEffect);

      //判断是否死亡
      if (CurHp <= 0)
      {
        CurHp = 0;
        PlayAni("die");
        Destroy(gameObject, 1.2f);

        //从敌人集合中移除
        GameApp.fightWorldManager.RemoveEnemy(this);
      }

      StopAllCoroutines();
      StartCoroutine(ChangeColor());
      StartCoroutine(UpdateHpSlider());
    }

    private IEnumerator ChangeColor()
    {
      bodySprite.material.SetFloat("_FlashAmount", 1);
      yield return new WaitForSeconds(0.25f);
      bodySprite.material.SetFloat("_FlashAmount", 0);
    }

    private IEnumerator UpdateHpSlider()
    {
      hpSlider.gameObject.SetActive(true);
      hpSlider.DOValue((float)CurHp / (float)MaxHp, 0.25f);
      yield return new WaitForSeconds(0.75f);
      hpSlider.gameObject.SetActive(false);
    }
  }
}
