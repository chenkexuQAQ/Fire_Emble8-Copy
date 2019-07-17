using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Check
{
    public Check()
    { }

    //检测当前位置是否有角色
    public GameObject TestRole(Vector3 pos)
    {
        RaycastHit hit;
        bool IsTestRole = Physics.Raycast(pos, new Vector3(0, 0, -1), out hit);
        if (IsTestRole)
        {
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }

    //检测地形
    public GameObject TestMap(Vector3 pos)
    {
        RaycastHit hit;
        bool Map = Physics.Raycast(pos, new Vector3(0, 0, 1), out hit);
        if (Map)
        {
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }
}
