using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MakeMenu : MonoBehaviour
{
    //事件系统
    public EventSystem ES;
    //光标移动脚本
    public CursorController CC;
    //显示UI
    public DisplayUI Dp;
    //移动脚本
    public Move M;
    //战斗公式计算脚本
    public Battle B;
    //检测脚本
    Check CheckObject = new Check();
    //攻击
    public GameObject AttackPrefab;
    //交换
    public GameObject ExchangePrefab;
    //物品
    public GameObject GoodsPrefab;
    //待机
    public GameObject StandbyPrefab;

    [Header("===== 攻击,被攻击者  =====")]
    public static GameObject Attacker;
    public static GameObject ByAttacker;

    private void Awake()
    {
        AttackPrefab.SetActive(false);
        ExchangePrefab.SetActive(false);
        GoodsPrefab.SetActive(false);
        StandbyPrefab.SetActive(false);
    }
    //根据传入数据的不同对菜单进行不同的生成
    public void MakeRoleMenu(int around)
    {
        switch (around)
        {
            //生成 只带 物品和待机的菜单
            //条件：周围不存在敌方、友方
            case 1:
                GoodsPrefab.SetActive(true);
                StandbyPrefab.SetActive(true);
                Dp.MoveRoleMenu();
                ES.SetSelectedGameObject(GoodsPrefab);
                CC.canMove = false;
                break;
            //生成 带 攻击，物品，待机的菜单
            //条件：周围只有敌方单位
            case 2:
                AttackPrefab.SetActive(true);
                GoodsPrefab.SetActive(true);
                StandbyPrefab.SetActive(true);
                Dp.MoveRoleMenu();
                ES.SetSelectedGameObject(AttackPrefab);
                CC.canMove = false;
                break;
            //生成 带 物品，交换，待机 
            //条件：周围只有友方单位
            case 3:
                ExchangePrefab.SetActive(true);
                GoodsPrefab.SetActive(true);
                StandbyPrefab.SetActive(true);
                Dp.MoveRoleMenu();
                ES.SetSelectedGameObject(ExchangePrefab);
                CC.canMove = false;
                break;
            //生成带攻击，交换，物品，待机的菜单
            //条件：周围既有敌方也有友方单位
            case 4:
                AttackPrefab.SetActive(true);
                ExchangePrefab.SetActive(true);
                GoodsPrefab.SetActive(true);
                StandbyPrefab.SetActive(true);
                Dp.MoveRoleMenu();
                ES.SetSelectedGameObject(AttackPrefab);
                CC.canMove = false;
                break;
            default:
                break;
        }

    }
    //返回操作界面
    public void SetNullMenu()
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        CC.canMove = true;
        Dp.MoveRoleMenuOut();
    }
    //制作战斗数据预览
    public void MakeBattleDataPreview()
    {
        Vector3[] Enemy = new Vector3[4];
        //TODO
        for (int i = 0; i < M.Enemy.Count; i++)
        {
            Enemy[i] = M.Enemy[i];
        }

        float EnemyDmgNum = B.Dmg(Enemy[0], CC.transform.position);
        float FriendDmgNum = B.Dmg(CC.transform.position, Enemy[0]);
        float EnemyHitNum = B.Hit(CC.transform.position, Enemy[0]);
        float FriendHitNum = B.Hit(Enemy[0], CC.transform.position);
        float EnemyCrtNum = B.Crt(Enemy[0]);
        float FriendCrtNum = B.Crt(CC.transform.position);

        //预览中的名字和血量
        GameObject FriendName = Dp.BattleDataPreview.transform.GetChild(0).gameObject;
        GameObject EnemyName = Dp.BattleDataPreview.transform.GetChild(1).gameObject;
        GameObject FriendHP = Dp.BattleDataPreview.transform.GetChild(2).gameObject;
        GameObject EnemyHP = Dp.BattleDataPreview.transform.GetChild(3).gameObject;
        //预览中的伤害
        GameObject FriendAttack = Dp.BattleDataPreview.transform.GetChild(4).gameObject;
        GameObject EnemyAttack = Dp.BattleDataPreview.transform.GetChild(5).gameObject;
        //预览中的命中
        GameObject FriendHit = Dp.BattleDataPreview.transform.GetChild(6).gameObject;
        GameObject EnemyHit = Dp.BattleDataPreview.transform.GetChild(7).gameObject;
        //预览中的必杀几率
        GameObject FriendCrt = Dp.BattleDataPreview.transform.GetChild(8).gameObject;
        GameObject EnemyCrt = Dp.BattleDataPreview.transform.GetChild(9).gameObject;

        //获取到友方和敌方的名字
        EnemyName.GetComponent<Text>().text = CheckObject.TestRole(Enemy[0]).GetComponent<Role>().RoleName;
        FriendName.GetComponent<Text>().text = CheckObject.TestRole(CC.transform.position).GetComponent<Role>().RoleName;
        //获取到敌方和友方的血量
        FriendHP.GetComponent<Text>().text = CheckObject.TestRole(CC.transform.position).GetComponent<Role>().Hp.ToString();
        EnemyHP.GetComponent<Text>().text = CheckObject.TestRole(Enemy[0]).GetComponent<Role>().Hp.ToString();

        //获取达到友方和敌方的威力
        FriendAttack.GetComponent<Text>().text = FriendDmgNum.ToString();
        EnemyAttack.GetComponent<Text>().text = EnemyDmgNum.ToString();

        //获取命中率
        FriendHit.GetComponent<Text>().text = FriendHitNum.ToString();
        EnemyHit.GetComponent<Text>().text = EnemyHitNum.ToString();

        //获取必杀率
        FriendCrt.GetComponent<Text>().text = FriendCrtNum.ToString();
        EnemyCrt.GetComponent<Text>().text = EnemyCrtNum.ToString();
    }
    //友方角色制作战斗界面
    public void MakeBattlePreview()
    {
        Vector3[] Enemy = new Vector3[4];
        //TODO
        for (int i = 0; i < M.Enemy.Count; i++)
        {
            Enemy[i] = M.Enemy[i];
        }

        float EnemyDmgNum = B.Dmg(Enemy[0], CC.transform.position);
        float FriendDmgNum = B.Dmg(CC.transform.position, Enemy[0]);
        float EnemyHitNum = B.Hit(CC.transform.position, Enemy[0]);
        float FriendHitNum = B.Hit(Enemy[0], CC.transform.position);
        float EnemyCrtNum = B.Crt(Enemy[0]);
        float FriendCrtNum = B.Crt(CC.transform.position);

        GameObject EnemyName = Dp.BattlePreview.transform.GetChild(1).transform.GetChild(0).gameObject;
        GameObject FriendName = Dp.BattlePreview.transform.GetChild(2).transform.GetChild(0).gameObject;
        GameObject EnemyHit = Dp.BattlePreview.transform.GetChild(3).transform.GetChild(0).gameObject;
        GameObject EnemyDmg = Dp.BattlePreview.transform.GetChild(3).transform.GetChild(1).gameObject;
        GameObject EnemyCrt = Dp.BattlePreview.transform.GetChild(3).transform.GetChild(2).gameObject;

        GameObject FriendHit = Dp.BattlePreview.transform.GetChild(4).transform.GetChild(0).gameObject;
        GameObject FriendDmg = Dp.BattlePreview.transform.GetChild(4).transform.GetChild(1).gameObject;
        GameObject FriendCrt = Dp.BattlePreview.transform.GetChild(4).transform.GetChild(2).gameObject;

        GameObject EnemyHP = Dp.BattlePreview.transform.GetChild(5).transform.GetChild(0).gameObject;
        GameObject FriendHP = Dp.BattlePreview.transform.GetChild(6).transform.GetChild(0).gameObject;


        //获取到友方和敌方的名字
        EnemyName.GetComponent<Text>().text = CheckObject.TestRole(Enemy[0]).GetComponent<Role>().RoleName;
        FriendName.GetComponent<Text>().text = CheckObject.TestRole(CC.transform.position).GetComponent<Role>().RoleName;
        //获取到敌方和友方的血量
        FriendHP.GetComponent<Text>().text = CheckObject.TestRole(CC.transform.position).GetComponent<Role>().Hp.ToString();
        EnemyHP.GetComponent<Text>().text = CheckObject.TestRole(Enemy[0]).GetComponent<Role>().Hp.ToString();

        //获取达到友方和敌方的威力
        FriendDmg.GetComponent<Text>().text = FriendDmgNum.ToString();
        EnemyDmg.GetComponent<Text>().text = EnemyDmgNum.ToString();

        //获取命中率
        FriendHit.GetComponent<Text>().text = FriendHitNum.ToString();
        EnemyHit.GetComponent<Text>().text = EnemyHitNum.ToString();

        //获取必杀率
        FriendCrt.GetComponent<Text>().text = FriendCrtNum.ToString();
        EnemyCrt.GetComponent<Text>().text = EnemyCrtNum.ToString();

        Attacker = CheckObject.TestRole(CC.transform.position);
        ByAttacker = CheckObject.TestRole(Enemy[0]);


    }

    public void MakeBattlePreview(GameObject Ai ,GameObject mb)
    {
        float EnemyDmgNum = B.Dmg(new Vector3(Ai.transform.position.x, Ai.transform.position.y,-1),new Vector3( mb.transform.position.x, mb.transform.position.y,-1));
        float FriendDmgNum = B.Dmg(new Vector3(mb.transform.position.x, mb.transform.position.y, -1), new Vector3(Ai.transform.position.x, Ai.transform.position.y, -1));
        float EnemyHitNum = B.Hit(new Vector3(mb.transform.position.x, mb.transform.position.y, -1), new Vector3(Ai.transform.position.x, Ai.transform.position.y, -1));
        float FriendHitNum = B.Hit(new Vector3(Ai.transform.position.x, Ai.transform.position.y, -1), new Vector3(mb.transform.position.x, mb.transform.position.y, -1));
        float EnemyCrtNum = B.Crt(new Vector3(Ai.transform.position.x, Ai.transform.position.y, -1));
        float FriendCrtNum = B.Crt(new Vector3(mb.transform.position.x, mb.transform.position.y, -1));

        GameObject EnemyName = Dp.BattlePreview.transform.GetChild(1).transform.GetChild(0).gameObject;
        GameObject FriendName = Dp.BattlePreview.transform.GetChild(2).transform.GetChild(0).gameObject;
        GameObject EnemyHit = Dp.BattlePreview.transform.GetChild(3).transform.GetChild(0).gameObject;
        GameObject EnemyDmg = Dp.BattlePreview.transform.GetChild(3).transform.GetChild(1).gameObject;
        GameObject EnemyCrt = Dp.BattlePreview.transform.GetChild(3).transform.GetChild(2).gameObject;

        GameObject FriendHit = Dp.BattlePreview.transform.GetChild(4).transform.GetChild(0).gameObject;
        GameObject FriendDmg = Dp.BattlePreview.transform.GetChild(4).transform.GetChild(1).gameObject;
        GameObject FriendCrt = Dp.BattlePreview.transform.GetChild(4).transform.GetChild(2).gameObject;

        GameObject EnemyHP = Dp.BattlePreview.transform.GetChild(5).transform.GetChild(0).gameObject;
        GameObject FriendHP = Dp.BattlePreview.transform.GetChild(6).transform.GetChild(0).gameObject;


        //获取到友方和敌方的名字
        EnemyName.GetComponent<Text>().text = CheckObject.TestRole(new Vector3(Ai.transform.position.x, Ai.transform.position.y, -1)).GetComponent<Role>().RoleName;
        FriendName.GetComponent<Text>().text = CheckObject.TestRole(new Vector3(mb.transform.position.x, mb.transform.position.y, -1)).GetComponent<Role>().RoleName;
        //获取到敌方和友方的血量
        FriendHP.GetComponent<Text>().text = CheckObject.TestRole(new Vector3(mb.transform.position.x, mb.transform.position.y, -1)).GetComponent<Role>().Hp.ToString();
        EnemyHP.GetComponent<Text>().text = CheckObject.TestRole(new Vector3(Ai.transform.position.x, Ai.transform.position.y, -1)).GetComponent<Role>().Hp.ToString();

        //获取达到友方和敌方的威力
        FriendDmg.GetComponent<Text>().text = FriendDmgNum.ToString();
        EnemyDmg.GetComponent<Text>().text = EnemyDmgNum.ToString();

        //获取命中率
        FriendHit.GetComponent<Text>().text = FriendHitNum.ToString();
        EnemyHit.GetComponent<Text>().text = EnemyHitNum.ToString();

        //获取必杀率
        FriendCrt.GetComponent<Text>().text = FriendCrtNum.ToString();
        EnemyCrt.GetComponent<Text>().text = EnemyCrtNum.ToString();

        Attacker = CheckObject.TestRole(new Vector3(mb.transform.position.x, mb.transform.position.y, -1));
        ByAttacker = CheckObject.TestRole(new Vector3(Ai.transform.position.x, Ai.transform.position.y, -1));

    }
}
