using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemController : MonoBehaviour {

    [Header("===== 判断各种菜单是否显示的Flag  =====")]
    //人物菜单显示
    public static bool IsDisplayRoleMenu;
    //系统菜单显示
    public static bool IsDisplayMenu;
    //是否显示战斗数据预览
    public static bool IsDisplayBattleData;
    //是否显示战斗界面
    public static bool IsDisplayBattle;
    //显示移动范围
    public static bool IsDisplayMoveRange = false;
    //角色操作
    public static bool PlayerCanMove = true;
    //电脑操作
    public static bool ComputerCanMove;
    //己方动画是否播放完毕
    public static bool IsPlayFriendAnim;
    //地方动画是否播放完毕
    public static bool IsPlayEnemyAnim;
    // Use this for initialization
    public GameObject Eric;
    public GameObject Oneill;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Eric == null)
        {
            if (IsDisplayBattle == false)
            {
                SceneManager.LoadScene(3);
            }
        }
        if (Oneill == null)
        {
            if (IsDisplayBattle == false)
            {
                SceneManager.LoadScene(2);
            }
        }
	}
}
