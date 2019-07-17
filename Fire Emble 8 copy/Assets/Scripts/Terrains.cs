using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrains : MonoBehaviour {

    public enum _Terrains {
        平地,道路,村庄,已关闭,民家,武器店,道具店,斗技场,要塞,城门,
        森林,沙漠,河,高山,桥,地板,围墙,墙壁,破壁,柱子,门,宝座,
        空宝箱,宝箱,屋顶,教会,废墟,悬崖,弩,破屋,楼梯,
    }
    

    public _Terrains terrain = _Terrains.平地;

    public int def=0;
    public int avo = 0;
    public bool CanMove = true;
}
