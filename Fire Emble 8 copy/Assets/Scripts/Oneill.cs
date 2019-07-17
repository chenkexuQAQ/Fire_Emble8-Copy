using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oneill : Role {

    //头像预览
    public Sprite headPreview;
    //头像
    public Sprite head;
    //角色缩略图
    public Sprite characterPic;
    //角色阵营  1为友方，2为敌方，3为援军
    private int roleCamp = 2;
    //角色名字
    private string rolename = "奥尼尔";
    //角色职业
    private string vocation = "山贼";
    //角色等级
    private float level = 4;
    //角色经验
    private float experience = 0;
    //角色最大血量
    private float hpMax = 23;
    //角色当前血量
    private float hp = 23;
    //力量
    private float power =6;
    //技术
    private float technology = 4;
    //速度
    private float speed = 7;
    //幸运
    private float lucky = 0;
    //守备
    private float garrison = 2;
    //移动
    private float move = 5;

    void Init()
    {
        HeadPreview = headPreview;
        Head = head;
        CharacterPic = characterPic;
        RoleCamp = roleCamp;
        RoleName = rolename;
        Vocation = vocation;
        Level = level;
        Experience = experience;
        HpMax = hpMax;
        Hp = hp;
        Power = power;
        Technology = technology;
        Speed = speed;
        Lucky = lucky;
        Garrison = garrison;
        Move = move;
    }
    private void Awake()
    {
        Init();
    }
    public void ReduceHp(int dmg)
    {
      //  Debug.Log("Oneill 在扣血,当前血量" + Hp);

        Hp = Hp -dmg;
    }
    private void Update()
    {
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
