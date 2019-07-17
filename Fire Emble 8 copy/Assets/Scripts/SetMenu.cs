using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetNullMenu : MonoBehaviour {

    //事件系统
    public EventSystem ES;
    //光标移动脚本
    public CursorController CC;
    //显示UI
    public DisplayUI Dp;


    //返回操作界面
    public void Back()
    {
        int childCount = transform.childCount;
        Debug.Log(childCount);
        for (int i = 0; i < childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        ES.SetSelectedGameObject(null);
        CC.enabled = true;
        Dp.MoveRoleMenuOut();
    }
}
