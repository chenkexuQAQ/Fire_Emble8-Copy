using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//光标控制
public class CursorController : MonoBehaviour {

    public bool canMove = true;


    private CellManager cellManager = new CellManager();

    void Start () {
    }
	
	void Update () {
        MoveCursor();
    }
    //移动光标
    public void MoveCursor()
    {
        if (canMove)
        {
            if (Input.GetKeyDown(KeyCode.D) && transform.position.x < cellManager.length - 1)
            {
                transform.position = transform.position + new Vector3(1, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.A) && transform.position.x > 0)
            {
                transform.position = transform.position + new Vector3(-1, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.W) && transform.position.y < 0)
            {
                transform.position = transform.position + new Vector3(0, 1, 0);
            }
            if (Input.GetKeyDown(KeyCode.S) && transform.position.y > -cellManager.height + 1)
            {
                transform.position = transform.position + new Vector3(0, -1, 0);
            }
        }
    }

}
