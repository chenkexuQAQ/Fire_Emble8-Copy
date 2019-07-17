using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendAnim : MonoBehaviour {
    
    public DisplayUI Dp;
    public EnemyAnim enemyAnim;
    public Animator Enemy;
    public CursorController CC;
    public Animator Friend;
    public Battle B;
    public PlayerController PC;
    public MakeMenu MM;

    

    void Awake()
    {
        enemyAnim = GameObject.Find("EnemyAnim").GetComponent<EnemyAnim>();
    }
    // Use this for initialization
    void Start ()  {     }
	// Update is called once per frame
	void Update () {    }

    //攻击
    public void AttackAction()
    {
        Vector3 AttackerPos = new Vector3(MakeMenu.Attacker.transform.position.x, MakeMenu.Attacker.transform.position.y, -1);
        Vector3 ByAttackerPos = new Vector3(MakeMenu.ByAttacker.transform.position.x, MakeMenu.ByAttacker.transform.position.y, -1);
        int Attack = (int)B.Dmg(AttackerPos,ByAttackerPos);
        enemyAnim.BeHited(Attack);
    }
    //被攻击
    public void BeHited(int dmg)
    {
        if (MakeMenu.Attacker.name == "Eric")
        {
            MakeMenu.Attacker.GetComponent<Eric>().ReduceHp(dmg);
        }        
    }

    public void StartAnimation()
    {
        GetComponent<Animator>().speed = 1;
    }

    public void ExchangeAnimation()
    {
        if (SystemController.IsPlayFriendAnim == false && SystemController.IsPlayEnemyAnim == false)
        {
            GetComponent<Animator>().speed = 0;
            Debug.Log("调用了这个");
            Debug.Log(Enemy.speed);
            Enemy.SetBool("CanAttack", true);
            SystemController.IsPlayFriendAnim = true;
        }
        //IsPlayFriendAnim
        if (MakeMenu.ByAttacker == null)
        {
            OutAnim();
        }

        if (SystemController.IsPlayFriendAnim == false && SystemController.IsPlayEnemyAnim == true)
        {
            OutAnim();            
        }
        
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
