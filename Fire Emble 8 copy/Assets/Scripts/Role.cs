using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role : MonoBehaviour {
   
    //头像预览
    public Sprite HeadPreview;
    //头像
    public Sprite Head;
    //角色缩略图
    public Sprite CharacterPic;

    //角色阵营  1为友方，2为敌方，3为援军
    public int RoleCamp ;

    //角色名字
    public string RoleName;
    //角色职业
    public string Vocation;
    //角色等级
    public float Level;
    //角色经验

    public float Experience;
    //角色最大血量
    public float HpMax;
    //角色当前血量
    public float Hp;

    //力量
    public float Power;
    //技术
    public float Technology;
    //速度
    public float Speed;
    //幸运
    public float Lucky;
    //守备
    public float Garrison;
    //移动
    public float Move;
        
}
