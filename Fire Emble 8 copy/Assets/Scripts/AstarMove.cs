using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

class AstarMove
{
    //直线行走为10，每一步G = G+STEP；
    public const int STEP = 10;
    //二位数组的地图
    public int[,] MapArray { get; private set; }
    //开启列表
    List<Point> OpenList;
    //关闭列表
    List<Point> CloseList;

    //构造函数，将地图赋值
    public AstarMove(int[,] map)
    {
        MapArray = map;
        //初始化开启列表，关闭列表
        OpenList = new List<Point>();
        CloseList = new List<Point>();
    }

    //寻找路径 
    public Point FindPath(Point start, Point end)
    {

        if (MapArray[end.X, end.Y] == 0)
        {
            return new Point(-1,- 1);
        }
        //将起点加入到开启列表
        OpenList.Add(start);
        while (OpenList.Count != 0)
        {
            //限定条件，如果不可到达则退出循环
            if (CloseList.Count > 10)
            {
                return new Point(-1, -1);
            }

            //寻找开启列表中F值最低的格子, 我们称它为当前格. 
            var tempStart = OpenList.MinPoint();
            //Debug.Log("被选择的点" + tempStart.X + "," + tempStart.Y);
            //在开启列表中移除当前格
            OpenList.Remove(tempStart);
            //当前格子加入到关闭列表
            CloseList.Add(tempStart);
            //找出相邻的点
            var surroundPoints = SurroundPoints(tempStart);
            //遍历相邻可达到的点
            foreach (Point point in surroundPoints)
            {
                //Debug.Log("周围点" + point.X + "," + point.Y);
                if (OpenList.Exists(point))
                {
                    //计算周围点的新的G值，如果比原来的大，就什么都不做，否则设置它的父节点为当前点，并且更新G和F
                    FoundPoint(tempStart, point);
                }
                else
                {
                    NotFoundPoint(tempStart, end, point);
                }
            }
            #region
            //for (int i = 0; i < CloseList.Count; i++)
            //{
            //    Debug.Log("CloseList中的点" + CloseList[i].X + "," + CloseList[i].Y);
            //}
            //for (int i = 0; i < OpenList.Count; i++)
            //{
            //    Debug.Log("Openlist中的点" + OpenList[i].X + "," + OpenList[i].Y);
            //    Debug.Log("Openlist中的点的F值" + OpenList[i].F);
            //}
            #endregion //测试BUG 用的
            //如果能在开启列表中找到结束点，则输出结束点
            if (OpenList.Get(end) != null)
            {
                return OpenList.Get(end);
            }

        }
        return new Point(-1, -1); 
    }

    //该点如果在开启列表中则进行计算新的G值，如果比原来的大，就什么都不做，否则设置它的父节点为当前点，并且更新G和F
    private void FoundPoint(Point tempStart, Point point)
    {
        var G = CalcG(tempStart, point);
        if (G < point.G)
        {
            point.ParentPoint = tempStart;
            point.G = G;
            point.CalcF();
        }
    }

    //该点如果不在开启列表中，则计算他的FGH值
    private void NotFoundPoint(Point tempStart, Point end, Point point)
    {
        point.ParentPoint = tempStart;
        point.G = CalcG(tempStart, point);
        point.H = CalcH(end, point);
        point.CalcF();
        OpenList.Add(point);
    }

    //计算G值
    //G表示从起点 移动到网格上指定方格移动的耗费
    private int CalcG(Point start, Point point)
    {

        int G = (Mathf.Abs(point.X - start.X) + Mathf.Abs(point.Y - start.Y));
        int parentG = point.ParentPoint != null ? point.ParentPoint.G : 0;
        return G + parentG;
    }

    //计算H值
    //H表示从制定方格 移动到 终点的预计耗费
    private int CalcH(Point end, Point point)
    {
        int step = Mathf.Abs(point.X - end.X) + Mathf.Abs(point.Y - end.Y);
        return step;
    }

    //寻找周围点，并且加入到集合
    public List<Point> SurroundPoints(Point point)
    {
        var surroundPoints = new List<Point>(4);
        //判断周围点是否可以移动，并且是否在关闭列表中
        if (point.X + 1 >= 0 && point.Y >= 0)
        {
            if (CanReach(point, point.X + 1, point.Y))
            {
                surroundPoints.Add(point.X + 1, point.Y);
            }
        }

        if (point.X - 1 >= 0 && point.Y >= 0)
        {
            if (CanReach(point, point.X - 1, point.Y))
            {
                surroundPoints.Add(point.X - 1, point.Y);
            }
        }
        if (point.X >= 0 && point.Y + 1 >= 0)
        {
            if (CanReach(point, point.X, point.Y + 1))
            {
                surroundPoints.Add(point.X, point.Y + 1);
            }
        }
        if (point.X >= 0 && point.Y - 1 >= 0)
        {
            if (CanReach(point, point.X, point.Y - 1))
            {
                surroundPoints.Add(point.X, point.Y - 1);
            }
        }

        return surroundPoints;
    }

    //等于0则为障碍不可行走
    private bool NoCanReach(int x, int y)
    {
        //x 和 y 的限定条件为 地图数组大小的极限
        if (x > 9 || y > 14)
            return false;
        return MapArray[x, y] == 0;
    }

    //判断该点是否可以行走
    public bool CanReach(Point start, int x, int y)
    {
        //如果这个地方不为障碍物 或者在关闭列表中存在
        if (NoCanReach(x, y) || CloseList.Exists(x, y))
        {
            //if (NoCanReach(x, y))
            //{
            //    Debug.Log(x + "," + y + "为障碍不可加入其中");
            //}
            //if (CloseList.Exists(x, y))
            //{
            //    Debug.Log(x + "," + y + "为关闭列表中，不可加入其中");
            //}
            return false;
        }
        else
        {
            return true;
        }
    }
}
//Pooint 类型
public class Point
{
    public Point ParentPoint { get; set; }
    //F = G + H 
    public int F { get; set; }
    //G 表示从起点 移动到网格上制定放个移动的耗费
    public int G { get; set; }
    //H 表示从指定的放个移动到 终点 的预计耗费 （似乎是无视障碍）
    public int H { get; set; }
    // Point的坐标点
    public int X { get; set; }
    public int Y { get; set; }
    //构造函数 给point赋予坐标位置
    public Point(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
    //F = G + H 计算F
    public void CalcF()
    {
        this.F = this.G + this.H;
    }
}

//对 List<Point> 的一些扩展方法 
public static class ListHelper
{
    public static bool Exists(this List<Point> points, Point point)
    {
        foreach (Point p in points)
            if ((p.X == point.X) && (p.Y == point.Y))
                return true;
        return false;
    }
    public static bool Exists(this List<Point> points, int x, int y)
    {
        foreach (Point p in points)
            if ((p.X == x) && (p.Y == y))
                return true;
        return false;
    }
    public static Point MinPoint(this List<Point> points)
    {
        points = points.OrderBy(p => p.F).ToList();
        return points[0];
    }
    public static void Add(this List<Point> points, int x, int y)
    {
        Point point = new Point(x, y);
        points.Add(point);
    }
    public static Point Get(this List<Point> points, Point point)
    {
        foreach (Point p in points)
            if ((p.X == point.X) && (p.Y == point.Y))
                return p;
        return null;
    }
    public static void Remove(this List<Point> points, int x, int y)
    {
        foreach (Point point in points)
        {
            if (point.X == x && point.Y == y)
                points.Remove(point);
        }
    }
}
