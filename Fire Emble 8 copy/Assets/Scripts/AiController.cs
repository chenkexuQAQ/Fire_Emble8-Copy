using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour {

    
    public Move M;
    public MenuKeyOptions Mko;
    //人物
    public GameObject Eric;
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
        if(SystemController.ComputerCanMove) {
            int mobility =(int)GetComponent<Role>().Move;
            M.AiMove(gameObject, Eric, mobility);
            SystemController.ComputerCanMove = false;
            if (Mathf.Abs((Eric.transform.position.x - transform.position.x) + Mathf.Abs(Eric.transform.position.y - transform.position.y)) <= 1)
            {
                // Debug.Log("可以攻击");
                Mko.AiAttack(gameObject,Eric);
            }
        }
	}


    
}
