using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eric : Role {
    //头像预览
    public Sprite headPreview;
    //头像
    public Sprite head;
    //角色缩略图
    public Sprite characterPic;
    //角色阵营  1为友方，2为敌方，3为援军
    private int roleCamp = 1;
    //角色名字
    private string rolename = "艾瑞克";
    //角色职业
    private string vocation = "领主";
    //角色等级
    private float level = 1;
    //角色经验
    private float experience = 0;
    //角色最大血量
    private float hpMax = 16;
    //角色当前血量
    private float hp = 16;
    //力量
    private float power = 10;
    //技术
    private float technology = 8;
    //速度
    private float speed = 9;
    //幸运
    private float lucky = 5;
    //守备
    private float garrison = 3;
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
     //   Debug.Log("Eric 在扣血 ,当前血量" + Hp);
        Hp = Hp - dmg;
    }
    private void Update()
    {
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
