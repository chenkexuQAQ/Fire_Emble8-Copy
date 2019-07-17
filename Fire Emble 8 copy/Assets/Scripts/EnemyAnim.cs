using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    
    public DisplayUI Dp;
    public FriendAnim friendAnim;
    public Animator Enemy;
    public Animator Friend;
    public CursorController CC;
    public PlayerController PC;
    public MakeMenu MM;
    public Battle B;

    private void Awake(){
        friendAnim = GameObject.Find("FriendAnim").GetComponent<FriendAnim>();
    }
    // Update is called once per frame
    void Update() {    }

    public void AttackAction()
    {
        Vector3 AttackerPos = new Vector3(MakeMenu.Attacker.transform.position.x, MakeMenu.Attacker.transform.position.y, -1);
        Vector3 ByAttackerPos = new Vector3(MakeMenu.ByAttacker.transform.position.x, MakeMenu.ByAttacker.transform.position.y, -1);
        int Attack = (int)B.Dmg(ByAttackerPos, AttackerPos);
        friendAnim.BeHited(Attack);
    }
    public void StartAnimation()
    {
        GetComponent<Animator>().speed = 1;
    }
    public void BeHited(int dmg)
    {
       if (MakeMenu.ByAttacker.name == "Oneill")
        {
            MakeMenu.ByAttacker.GetComponent<Oneill>().ReduceHp(dmg);
        }
    }
    public void ExchangeAnimation()
    {
        if (SystemController.IsPlayFriendAnim == false && SystemController.IsPlayEnemyAnim == false)
        {
            Debug.Log("我是敌人动画,切换角色动画");
            GetComponent<Animator>().speed = 0f;
            Friend.SetInteger("role", 1);
            SystemController.IsPlayEnemyAnim = true;
        }

        if (MakeMenu.Attacker == null)
        {
            OutAnim();
        }
        if (SystemController.IsPlayEnemyAnim == false && SystemController.IsPlayFriendAnim == true)
        {
            OutAnim();
        }
       
        //Enemy.SetBool("CanAttack", false);
    }
    public void OutAnim()
    {
        CC.canMove = true;
        Enemy.SetBool("CanAttack", false);
        Dp.BattlePreview.SetActive(false);
        PC.ClearController();
        MM.SetNullMenu();
        SystemController.IsDisplayBattle = false;
        SystemController.IsPlayEnemyAnim = false;
        SystemController.IsPlayFriendAnim = false;
    }
}
