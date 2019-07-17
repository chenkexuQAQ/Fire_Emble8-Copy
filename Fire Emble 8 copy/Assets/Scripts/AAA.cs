using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAA : MonoBehaviour {

	// Use this for initialization
	void Start () {
        a();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    static void a()
    {
        int[,] array = {
                { 1,0,1,0,1,0,0,0,0,0,0,0,0,0,0 },
                { 0,1,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,1,0,1,0,0,0,0,0,0,0,0,0,0,0 },
                { 1,1,1,1,1,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,1,1,1,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,1,0,1,1,1,1,0,0,0,0,1 },
                { 0,0,0,0,0,0,0,0,1,1,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,1,1,1,1,1,1 },
                { 0,0,0,0,0,0,0,0,0,0,1,1,1,1,1 },
                { 0,0,0,0,0,0,0,0,0,0,0,1,1,1,1 }
            };
        AstarMove maze = new AstarMove(array);
        Point start = new Point(7 , 14);
        Point end = new Point(5 ,14);
        var parent = maze.FindPath(start, end);
        Debug.Log("Print path:");

        while (parent != null)
        {
            Debug.Log(parent.X + ", " + parent.Y);
            parent = parent.ParentPoint;
        }
    }
}
