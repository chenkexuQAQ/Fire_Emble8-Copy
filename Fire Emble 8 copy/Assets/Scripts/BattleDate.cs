using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDate : MonoBehaviour {

    public Move M;
    public DisplayUI Dp;
    public CursorController CC;
    public Battle B;
    Check CheckObject = new Check();

    public float EnemyDmg;
    public float FriendDmg;
    public float EnemyHit;
    public float FriendHit;
    public float EnemyCrt;
    public float FriendCrt;
    public float EnemyHp;
    public float FriendHP;

    Vector3[] Enemy = new Vector3[4];

    public void GetEnemy()
    {
        //敌人的位置数据
        for (int i = 0; i < M.Enemy.Count; i++)
        {
            Enemy[i] = M.Enemy[i];
        }
    }

    public void SetDate()
    {
        EnemyDmg = B.Dmg(Enemy[0], CC.transform.position);
        FriendDmg = B.Dmg(CC.transform.position, Enemy[0]);
        EnemyHit = B.Hit(CC.transform.position, Enemy[0]);
        FriendHit = B.Hit(Enemy[0], CC.transform.position);
        EnemyCrt = B.Crt(Enemy[0]);
        FriendCrt = B.Crt(CC.transform.position);
        EnemyHp = CheckObject.TestRole(CC.transform.position).GetComponent<Role>().Hp;
        FriendHP = CheckObject.TestRole(Enemy[0]).GetComponent<Role>().Hp;
    }


}
