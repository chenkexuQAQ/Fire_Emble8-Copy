using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuKeyOptions : MonoBehaviour {

    public PlayerController Pc;
    public Move M;
    public MakeMenu MM;
    public CursorController CC;
    public DisplayUI Dp;
    public Battle B;
    public Animator BattleAnim;
    Check CheckObject = new Check();
    
    private void Awake()
    { }

    /// <summary>
    /// 待机
    /// </summary>
    public void Standby()
    {
        MM.SetNullMenu();
        SystemController.IsDisplayRoleMenu = false;       
        Pc.ClearController();
        SystemController.ComputerCanMove = true;
    }

    /// <summary>
    /// 攻击
    /// </summary>
    public void Attack()
    {
        MM.MakeBattleDataPreview();
        MM.MakeBattlePreview();
        Dp.MoveBattleDataPreview();
        SystemController.IsDisplayBattleData = true;
    }
    public void AiAttack(GameObject Ai,GameObject mb)
    {
        SystemController.IsDisplayBattle = true;
        MM.MakeBattlePreview(Ai,mb);
        Dp.BattlePreview.SetActive(true);
        BattleAnim.SetBool("CanAttack", true);
        SystemController.IsDisplayRoleMenu = false;
        SystemController.IsDisplayBattleData = false;
    }

    /// <summary>
    /// 救出
    /// </summary>
    public void Rescue()
    {
        //TODO
        Debug.Log("救出");
    }
    /// <summary>
    /// 物品
    /// </summary>
    public void Goods()
    {
        //TODO
        Debug.Log("物品");
    }
    /// <summary>
    /// 交换
    /// </summary>
    public void Exchange()
    {
        //TODO
        Debug.Log("交换");
    }
    /// <summary>
    /// 对话
    /// </summary>
    public void Dialogue()
    {
        //TODO
        Debug.Log("对话");
    }
    /// <summary>
    /// 接受
    /// </summary>
    public void Accept()
    {
        //TODO
        Debug.Log("接受");
    }
    /// <summary>
    /// 访问
    /// </summary>
    public void Visit()
    {
        //TODO
        Debug.Log("访问");
    }
    /// <summary>
    /// 支援
    /// </summary>
    public void Support()
    {
        //TODO
        Debug.Log("支援");
    }    
}
