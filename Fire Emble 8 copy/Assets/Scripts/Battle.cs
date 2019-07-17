using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    Check CheckObject =new  Check();

    //攻击公式: 最终攻击力-最终防御力
    //最终攻击力 = 角色的力（魔力）+ 武器相克补正 + 武器攻击力 + 支援 + 武器附加能力补正 + 持有特殊道具；
    //    物理最终防御力 = 角色物理防御力 + 武器相克补正 + 支援 + 地形补正 + 武器附加能力补正 + 持有特殊道具；
    //    魔法最终防御力 = 角色魔法防御力 + 武器相克补正 + 支援 + 地形补正 + 武器附加能力补正 + 持有特殊道具 + 魔防杖/圣水（两者取一）；
    public float Dmg(Vector3 attacker,Vector3 byattacker)
    {
        //攻击方的攻击
        float attackerAttack = CheckObject.TestRole(attacker).GetComponent<Role>().Power;
        //float byattackerAttack = CheckObject.TestRole(byattacker).GetComponent<Role>().Power;
        //被攻击方的防御
        //float attackerGarrison = CheckObject.TestRole(attacker).GetComponent<Role>().Garrison;
        float byattackerGarrison = CheckObject.TestRole(byattacker).GetComponent<Role>().Garrison;
        //被攻击地形的防御
        //float attackerMapDef = CheckObject.TestMap(attacker).GetComponent<Terrains>().def;
        float byattackerMapDef = CheckObject.TestMap(byattacker).GetComponent<Terrains>().def;


        float Damage = attackerAttack - byattackerGarrison - byattackerMapDef;
        return Damage > 0? Damage:0 ;
    }

    //最终命中率： 命中率 - 回避率
    //     命中率 = 武器命中率 + 武器相克补正 + 技 * 2 + 幸运 / 2 + 支援 + 武器S级补正（5%）
    //     回避率 = 攻速*2 + 幸运 + 支援 + 地形 + 持有特殊道具
    //         杖的命中率 = 30 + 魔力 * 5 + 技 - 敌方魔防 * 5 - 距离 * 2
    public float Hit(Vector3 attacker,Vector3 byattacker)
    {
        //攻击者的命中率
        float attackerhit = CheckObject.TestRole(attacker).GetComponent<Role>().Technology * 2 +
            CheckObject.TestRole(attacker).GetComponent<Role>().Lucky / 2 + 85;
        //被攻击者的回避率
        float byattackeravo = CheckObject.TestRole(byattacker).GetComponent<Role>().Speed * 2 +
            CheckObject.TestRole(attacker).GetComponent<Role>().Lucky;
        //被攻击者的地形回避
        float byattackermapavo = CheckObject.TestMap(byattacker).GetComponent<Terrains>().avo;

        int hit = (int)(attackerhit - byattackeravo - byattackermapavo);

        return hit > 0 ? hit : 0;
    }

    //必杀技率 :武器必杀率 + 角色技/2 + 武器S级补正（5%）+支援 + 职业补正（15%）+ 持有特殊道具；
    public float Crt(Vector3 attacker)
    {
        //攻击者的必杀技率
        float attackercrt = CheckObject.TestRole(attacker).GetComponent<Role>().Technology / 2;

        return (int)attackercrt;
    }
}
