using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("===== 需要调用的其他的脚本  =====")]
    //移动脚本
    public Move M1;
    //显示UI脚本
    public DisplayUI Dp;
    //检测脚本
    Check Check_Controller = new Check();
    //制作菜单
    public MakeMenu MM;
    //动画脚本
    public Animator BattleAnim;
    [Header("===== 存储变量  =====")]
    //存储当前角色的移动力
    public int playerMove;
    //当前角色
    public GameObject CurrentRole;
    //旧位置
    public Vector3 oldPos;
    //人物标识
    public string role;
    //判断是否正在对某一角色进行操控；
    public bool IsControl;
    [Header("===== 储存角色  =====")]
    public GameObject Eric;
    public GameObject Oneill;

    int X, Y;
    void Start () {
        M1.MakeMap();
	}
	
	void Update () {
        //每次检测需重置地图数组
        M1.MakeMap();
        //获取当前检测的目标，如果是友方单位则存储起来,接下来队该角色进行操作。
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Eric.GetComponent<Eric>().ReduceHp(2);
            Oneill.GetComponent<Oneill>().ReduceHp(1);
        }

        if (Input.GetKeyDown(KeyCode.J) && CurrentRole == null && IsControl == false)
        {
            CurrentRole = Check_Controller.TestRole(transform.position);
            if (CurrentRole != null  && CurrentRole.tag =="Friend")
            {
                IsControl = true;
                playerMove = (int)CurrentRole.GetComponent<Role>().Move;
                oldPos = CurrentRole.transform.position;
            }
        }

        //按下J显示 移动路径
        //条件:菜单UI未显示,角色菜单UI未显示,角色控制权可用,战斗界面未显示
        if (Input.GetKeyUp(KeyCode.J) && SystemController.IsDisplayMenu == false &&
            SystemController.IsDisplayRoleMenu == false && 
            IsControl == true && SystemController.IsDisplayBattle == false)
        {
            //1-1.判断是人物还是空地，人物则显示移动范围，地形则显示整体菜单
            if ((CurrentRole != null) && (SystemController.IsDisplayMoveRange == false))
            {
                //判断移动范围并且显示移动范围
                M1.MoveRange(playerMove);
                SystemController.IsDisplayMoveRange = true;
            }
        }
        
        //1-2.如果显示完移动范围，则可以进行移动
        if (SystemController.IsDisplayMoveRange == true && Input.GetKeyDown(KeyCode.J))
        {            
            foreach (var p in M1.path)
            {
                if (p.X == -transform.position.y && p.Y == transform.position.x)
                {
                    oldPos = CurrentRole.transform.position;
                    CurrentRole.transform.position = new Vector3(p.Y, -p.X, -2);
                    M1.CloseMoveRange();
                    SystemController.IsDisplayMoveRange = false;
                    MM.MakeRoleMenu(M1.TestAround(transform.position));
                    SystemController.IsDisplayRoleMenu = true;
                   // SystemController.ComputerCanMove = true;
                    break;
                }
            }
        }

        //按下J 如果显示了战斗数值预览,则进入战斗界面
        if (Input.GetKeyDown(KeyCode.J) && SystemController.IsDisplayBattleData == true)
        {
            SystemController.IsDisplayBattle = true;
            Dp.BattlePreview.SetActive(true);
            Dp.MoveBattleDataPreviewOut();
            Dp.MoveRoleMenuOut();            
            BattleAnim.SetInteger("role",1);
            SystemController.IsDisplayRoleMenu = false;
            SystemController.IsDisplayBattleData = false;
        }
        //按下K返回等功能
        if (Input.GetKeyDown(KeyCode.K))
        {
            //1.如果显示菜单，移动范围等功能则返回。反之没有。
            if (SystemController.IsDisplayMoveRange == true)
            {
                M1.CloseMoveRange();
                SystemController.IsDisplayMoveRange = false;
                ClearController();
            }

            if (SystemController.IsDisplayRoleMenu == true && SystemController.IsDisplayBattleData == false)
            {
                MM.SetNullMenu();
                SystemController.IsDisplayRoleMenu = false;
                CurrentRole.transform.position = oldPos;
                transform.position = new Vector3(oldPos.x, oldPos.y, transform.position.z);
                ClearController();
                //TODO 显示原来移动范围
            }
            if (SystemController.IsDisplayBattleData == true)
            {
                MM.MakeRoleMenu(M1.TestAround(transform.position));
                Dp.MoveBattleDataPreviewOut();
                SystemController.IsDisplayBattleData = false;
            }
        }
    }


    public void ClearController()
    {
        IsControl = false;
        oldPos = new Vector3(0, 0, 0);
        CurrentRole = null;
    }
}
