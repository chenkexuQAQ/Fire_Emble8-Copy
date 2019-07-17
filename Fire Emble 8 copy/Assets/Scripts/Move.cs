using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//判断移动范围
public class Move : MonoBehaviour
{
    //地图。
    public static int[,] map = new int[10,15];
    //玩家可移动路径
    public List<MovePath> path = new List<MovePath>();
    //记录敌人路径的栈
    public List<MovePath> AiCanMovePath = new List<MovePath>();
    //检测实例
    Check CheckObject_Move = new Check();
    //敌人位置数组
    public List<Vector3> Enemy = new List<Vector3>();
    public List<Vector3> Friend = new List<Vector3>();
    //记录敌人移动的栈
    Stack<Point> AiMovePath = new Stack<Point>();
   
    //判断移动范围 -- 玩家控制 -- 1.根据移动力来判断范围
    public void MoveRange(int Mobility)
    {
        MakeMap();
        //把当前位置添加进可以移动的范围,并且变换颜色
        path.Add(new MovePath(-(int)transform.position.y, (int)transform.position.x));
        CheckObject_Move.TestMap(transform.position).transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 180, 255, 0.5f);
        for (int x = 0; x <= Mobility; x++)
        {
            for (int y = 0; y <= Mobility-x; y++)
            {
                JudgeMove(new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z), Mobility);
                JudgeMove(new Vector3(transform.position.x - x, transform.position.y - y, transform.position.z), Mobility);
                JudgeMove(new Vector3(transform.position.x + x, transform.position.y - y, transform.position.z), Mobility);
                JudgeMove(new Vector3(transform.position.x - x, transform.position.y + y, transform.position.z), Mobility);
            }
        }
    }
    //判断移动范围 -- AI控制 -- 1.根据移动力来判断范围
    public void MoveRange(int Mobility, GameObject Com, GameObject player)
    {
        MakeMap();
        path.Add(new MovePath(-(int)transform.position.y, (int)transform.position.x));
        for (int x = 0; x <= Mobility; x++)
        {
            for (int y = 0; y <= Mobility - x; y++)
            {
                JudgeMove(new Vector3(Com.transform.position.x + x, Com.transform.position.y + y, transform.position.z), Mobility, Com, player);
                JudgeMove(new Vector3(Com.transform.position.x - x, Com.transform.position.y - y, transform.position.z), Mobility, Com, player);
                JudgeMove(new Vector3(Com.transform.position.x + x, Com.transform.position.y - y, transform.position.z), Mobility, Com, player);
                JudgeMove(new Vector3(Com.transform.position.x - x, Com.transform.position.y + y, transform.position.z), Mobility, Com, player);
            }
        }
    }

    //判断移动范围-- 玩家控制 --2.通过地形进一步缩小移动范围
    public void JudgeMove(Vector3 pos,int mobility)
    {
        GameObject CurrentRole = CheckObject_Move.TestRole(pos);
        if (CurrentRole != null)
        {
            //设置为0就不可行走
            map[-(int)pos.y, (int)pos.x] = 0;
            return;
        }
        
        GameObject CurrentMap = CheckObject_Move.TestMap(pos);
        if (CurrentMap != null) {
            if (CurrentMap.GetComponent<Terrains>().CanMove)
            {
                int EndX = -(int)pos.y;
                int EndY = (int)pos.x;

                int StartX = -(int)transform.position.y;
                int StartY = (int)transform.position.x;

                if (DisplayMoveRange(map, StartX, StartY, EndX, EndY, mobility))
                {
                    CurrentMap.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 180, 255, 0.5f);
                    MovePath point = new MovePath(EndX, EndY);
                    path.Add(point);
                }
            }
        }
    }
    //判断移动范围-- AI控制 --2.通过地形进一步缩小移动范围
    public void JudgeMove(Vector3 pos, int mobility, GameObject Com, GameObject player)
    {
        GameObject CurrentRole = CheckObject_Move.TestRole(pos);
        if (CurrentRole != null && CurrentRole == player)
        {
            map[-(int)pos.y, (int)pos.x] = 0;
        }
        GameObject CurrentMap = CheckObject_Move.TestMap(pos);
        if (CurrentMap != null)
        {
            if (CurrentMap.GetComponent<Terrains>().CanMove)
            {
                int EndX = -(int)pos.y;
                int EndY = (int)pos.x;
                int StartX = -(int)Com.transform.position.y;
                int StartY = (int)Com.transform.position.x;
                if (DisplayMoveRange(map, StartX, StartY, EndX, EndY, mobility))
                {
                    MovePath point = new MovePath(EndX, EndY);
                    AiCanMovePath.Add(point);
                }
            }
        }
    }

    //判断是否超过行动力
    public bool DisplayMoveRange(int[,] map, int startX, int startY, int endX, int endY, int mobility)
    {
        int MoveLength = 0;
        AstarMove maze = new AstarMove(map);
        Point start = new Point(startX, startY);
        Point end = new Point(endX, endY);
        var parent = maze.FindPath(start, end);
        
        if ((parent.X == -1) && (parent.Y == -1))
        {
            return false;
        }
        while (parent != null)
        {
            MoveLength += 1;
            parent = parent.ParentPoint;
        }
     
        if (MoveLength <= mobility + 1)
        {
            return true;
        }
        else {
            return false;
        }
    }
    
    //最短路径的记录
    public void AiPath(int[,] map, int startX, int startY, int endX, int endY)
    {
        AstarMove maze = new AstarMove(map);
        Point start = new Point(startX, startY);
        Point end = new Point(endX, endY);
        var parent = maze.FindPath(start, end);
        
        while (parent != null)
        {
            AiMovePath.Push(parent);
            parent = parent.ParentPoint;
        }
    }

    //AI进行移动相关
    public void AiMove(GameObject Com, GameObject player, int AiMobility)
    {
        MoveRange(AiMobility, Com, player);

        int linshix = -(int)player.transform.position.y;
        int linshiy = (int)player.transform.position.x;

        map[linshix, linshiy] = 1;
        AiPath(map, -(int)Com.transform.position.y, (int)Com.transform.position.x, linshix, linshiy);

        while (AiMovePath.Count != 0)
        {
            Point linshi = AiMovePath.Pop();
            if (linshi.X == -player.transform.position.y && linshi.Y == player.transform.position.x)
            {
                if (AiMovePath.Count != 0)
                {
                    Point linshi2 = AiMovePath.Pop();
                    Com.transform.position = new Vector3(linshi2.Y, -linshi2.X, Com.transform.position.z);
                }                            
                break;
            }
            foreach (MovePath p in AiCanMovePath)
            {
                if (linshi.X == p.X && linshi.Y == p.Y)
                {
                    Com.transform.position = new Vector3(linshi.Y, -linshi.X, Com.transform.position.z);
                }
            }
        }
    }
    
    //关闭显示移动范围
    public void CloseMoveRange()
    {
        foreach (var p in path)
        {
            //Debug.Log(p.X + "," + p.Y);
            CheckObject_Move.TestMap(new Vector3(p.Y,-p.X,transform.position.z)).transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0f);
        }
        path.Clear();
    }

    //遍历生成地图
    public void MakeMap()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 15 ; y++)
            {
                if (CheckObject_Move.TestMap(new Vector3(y, -x, transform.position.z)).GetComponent<Terrains>().CanMove)
                {
                    map[x, y] = 1;
                }
                
            }
        }
    }
    
    //检测周围单位的方法
    public int TestAround(Vector3 pos)
    {
        Enemy.Clear();
        Friend.Clear();

        GameObject AroundUp = CheckObject_Move.TestRole(new Vector3(pos.x, pos.y + 1, pos.z));
        if (AroundUp != null && AroundUp.GetComponent<Role>().RoleCamp == 1)
        {
            Friend.Add(new Vector3(pos.x, pos.y + 1, pos.z));
        }
        else if (AroundUp != null && AroundUp.GetComponent<Role>().RoleCamp == 2)
        {
            Enemy.Add(new Vector3(pos.x, pos.y + 1, pos.z));
        }

        GameObject AroundDown = CheckObject_Move.TestRole(new Vector3(pos.x, pos.y - 1, pos.z));
        if (AroundDown != null && AroundDown.GetComponent<Role>().RoleCamp == 1)
        {
            Friend.Add(new Vector3(pos.x, pos.y - 1, pos.z));
        }
        else if (AroundDown != null && AroundDown.GetComponent<Role>().RoleCamp == 2)
        {
            Enemy.Add(new Vector3(pos.x, pos.y - 1, pos.z));
        }

        GameObject AroundLeft = CheckObject_Move.TestRole(new Vector3(pos.x - 1, pos.y, pos.z));
        if (AroundLeft != null && AroundLeft.GetComponent<Role>().RoleCamp == 1)
        {
            Friend.Add(new Vector3(pos.x - 1, pos.y, pos.z));
        }
        else if (AroundLeft != null && AroundLeft.GetComponent<Role>().RoleCamp == 2)
        {
            Enemy.Add(new Vector3(pos.x - 1, pos.y, pos.z));
        }

        GameObject AroundRight = CheckObject_Move.TestRole(new Vector3(pos.x + 1, pos.y, pos.z));
        if (AroundRight != null && AroundRight.GetComponent<Role>().RoleCamp == 1)
        {
            Friend.Add(new Vector3(pos.x + 1, pos.y, pos.z));
        }
        else if (AroundRight != null && AroundRight.GetComponent<Role>().RoleCamp == 2)
        {
            Enemy.Add(new Vector3(pos.x + 1, pos.y, pos.z));
        }

        if (Enemy.Count == 0 && Friend.Count == 0)
        {
            //周围没有敌方和友方单位
            return 1;
        }
        else if (Enemy.Count > 0 && Friend.Count == 0)
        {
            //周围只有敌方单位
            return 2;
        }
        else if (Friend.Count > 0 && Enemy.Count == 0)
        {
            //周围只有友方单位
            return 3;
        }
        else if (Friend.Count > 0 && Enemy.Count > 0)
        {
            //周围既有友方也有敌方
            return 4;
        }
        //基本不可能达到的情况。如果到这里就说明出现BUG 了
        return 5;
    }
}

//移动点的类
public class MovePath
{
    public int X { get; set; }
    public int Y { get; set; }
    //构造函数 给point赋予坐标位置
    public MovePath(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
}
