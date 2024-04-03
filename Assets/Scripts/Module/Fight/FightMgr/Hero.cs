using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BattleFlagGameStudy
{
  public class Hero : ModelBase, ISkill
  {
    public SkillProperty skillProperty { get; set; }
    private Slider hpSlider;
    protected override void Start()
    {
      base.Start();

      hpSlider = transform.Find("hp/bg").GetComponent<Slider>();
    }
    public void Init(Dictionary<string, string> data, int row, int col)
    {
      this.data = data;
      this.RowIndex = row;
      this.ColIndex = col;
      this.id = int.Parse(data["Id"]);
      this.Type = int.Parse(data["Type"]);
      this.Attack = int.Parse(data["Attack"]);
      this.Step = int.Parse(data["Step"]);
      this.MaxHp = int.Parse(data["Hp"]);
      this.CurHp = this.MaxHp;
      skillProperty = new SkillProperty(int.Parse(this.data["Skill"]));
    }

    protected override void OnSelectCallback(object args)
    {
      //玩家回合 才能选中角色
      if (GameApp.fightWorldManager.state == GameState.Player)
      {
        if (GameApp.commandManager.IsRunningCommand)
        {
          return;
        }

        //执行未选中
        GameApp.messageCenter.PostEvent(Defines.OnUnSelectEvent);


        if (!HasMovementCompleted)
        {  //显示路径
          GameApp.mapManager.ShowStepGrid(this, Step);

          //添加显示路径的指令
          GameApp.commandManager.AddCommand(new ShowPathCommand(this));

          //添加选项事件
          AddOptionEvent();

        }

        Debug.Log($"<color=yellow>{gameObject.name}OnSelectCallback</color>");

        // base.OnSelectCallback(args);

        GameApp.viewManager.Open(ViewType.HeroDesView, this);

      }
    }

    private void AddOptionEvent()
    {
      GameApp.messageCenter.AddTempEvent(Defines.OnAttackEvent, OnAttackEvent);
      GameApp.messageCenter.AddTempEvent(Defines.OnIdleEvent, OnIdleEvent);
      GameApp.messageCenter.AddTempEvent(Defines.OnCancelEvent, OnCancelEvent);
    }


    private void OnAttackEvent(object obj)
    {
      GameApp.commandManager.AddCommand(new ShowSkillRangeCommand(this));
    }
    private void OnIdleEvent(object obj)
    {
      HasMovementCompleted = true;
    }

    private void OnCancelEvent(object obj)
    {
      GameApp.commandManager.UnDo();
    }

    protected override void OnUnSelectCallback(object args)
    {
      base.OnUnSelectCallback(args);

      GameApp.viewManager.Close(ViewType.HeroDesView);
    }


    //技能区域 显示与隐藏
    public void ShowSkillArea()
    {
      GameApp.mapManager.SHowAttackStep(this, skillProperty.AttackRange, Color.red);
    }

    public void HideSkillArea()
    {
      GameApp.mapManager.HideAttackStep(this, skillProperty.AttackRange);
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
        GameApp.fightWorldManager.RemoveHero(this);
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
