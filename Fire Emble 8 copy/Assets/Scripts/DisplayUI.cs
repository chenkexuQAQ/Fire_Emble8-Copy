using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUI : MonoBehaviour
{
    #region
    //地形预览
    public GameObject TerrainPreview;
    //目的预览
    public GameObject PurposePreview;
    //角色预览
    public GameObject RolePreview;
    //角色菜单预览
    public GameObject RoleMenuPreview;
    //详细信息显示
    public GameObject Information;
    //系统菜单
    public GameObject Menu;
    //战斗数据预览面板
    public GameObject BattleDataPreview;
    //战斗画面
    public GameObject BattlePreview;

    //是否显示角色预览
    public bool DisplayRolePreview;
    //是否检测到有角色
    public bool IsCharacter;
    //检测物体
    Check CheckObject_DisplayUI = new Check();
    #endregion//各种变量
    private void Awake()
    {
        TerrainPreview = GameObject.Find("TerrainPreview");
        PurposePreview = GameObject.Find("PurposePreview");
        RolePreview = GameObject.Find("RolePreview");
        RoleMenuPreview = GameObject.Find("RoleMenuPreview");
        Menu = GameObject.Find("Menu");
        BattleDataPreview = GameObject.Find("BattleDataPreview");

    }
    void Start()
    { 
        RolePreview.SetActive(false);
        BattlePreview.SetActive(false);
        JudgeTerrainPosition();
        JudgePurposePosition();
    }
    void Update()
    {
        JudgeTerrainPosition();
        JudgePurposePosition();
        MapInformation();
        RoleInformation();
    }
    //将角色信息显示在UI中
    public void RoleInformation()
    {
        if (CheckObject_DisplayUI.TestRole(transform.position) != null)
        {
            GameObject Currentrole = CheckObject_DisplayUI.TestRole(transform.position);
            IsCharacter = true;
            RolePreview.SetActive(true);
            RolePreview.transform.GetChild(0).transform.GetComponent<Image>().sprite = Currentrole.GetComponent<Role>().HeadPreview;
            RolePreview.transform.GetChild(1).transform.GetComponent<Text>().text = Currentrole.GetComponent<Role>().RoleName;
            RolePreview.transform.GetChild(2).transform.GetComponent<Text>().text ="  Hp." +  Currentrole.GetComponent<Role>().Hp + " / " + Currentrole.GetComponent<Role>().HpMax;

            DisplayRolePreview = true;
            JudgeRolePreview();
            
        }
        else {
            RolePreview.SetActive(false);
            DisplayRolePreview = false;
            IsCharacter = false;
        }
    }
    //将地形信息显示在UI中
    public void MapInformation()
    {
        if (CheckObject_DisplayUI.TestMap(transform.position) != null)
        {
            GameObject Currentterrain = CheckObject_DisplayUI.TestMap(transform.position);
            //地形类型
            TerrainPreview.transform.GetChild(0).GetComponent<Text>().text = Currentterrain.GetComponent<Terrains>().terrain.ToString();
            //地形DEF数据
            TerrainPreview.transform.GetChild(1).GetComponent<Text>().text = "DEF .  " + Currentterrain.GetComponent<Terrains>().def.ToString();
            //地形AVO数据
            TerrainPreview.transform.GetChild(2).GetComponent<Text>().text = "AVO . " + Currentterrain.GetComponent<Terrains>().avo.ToString(); 
        }
    }
    //判断地形预览位置
    public void JudgeTerrainPosition()
    {
        if (this.gameObject.transform.position.x > 7)
        {
            TerrainPreview.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
            TerrainPreview.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            TerrainPreview.GetComponent<RectTransform>().anchoredPosition = new Vector2(80, 80);
        }
        else
        {
            TerrainPreview.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0);
            TerrainPreview.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0);
            TerrainPreview.GetComponent<RectTransform>().anchoredPosition = new Vector2(-80, 80);
        }
    }
    //判断目的预览位置
    public void JudgePurposePosition()
    {
        if (this.gameObject.transform.position.y > -5 && this.gameObject.transform.position.x > 7)
        {
            PurposePreview.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0);
            PurposePreview.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0);
            PurposePreview.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, 50);
        }
        else
        {
            PurposePreview.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            PurposePreview.GetComponent<RectTransform>().anchorMin = new Vector2(1, 1);
            PurposePreview.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, -50);
        }
    }
    //判断角色预览位置
    public void JudgeRolePreview()
    {
        if (this.gameObject.transform.position.y >- 5 && this.gameObject.transform.position.x < 7)
        {
            RolePreview.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
            RolePreview.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            RolePreview.GetComponent<RectTransform>().anchoredPosition = new Vector2(135, 70);
        }
        else
        {
            RolePreview.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
            RolePreview.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
            RolePreview.GetComponent<RectTransform>().anchoredPosition = new Vector2(135, -70);
        }
    }
    //把角色菜单移动到中间位置
    public void MoveRoleMenu()
    {
        if (this.gameObject.transform.position.y > -5 && this.gameObject.transform.position.x > 7)
        {
            RoleMenuPreview.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.5f);
            RoleMenuPreview.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0.5f);
            RoleMenuPreview.GetComponent<RectTransform>().anchoredPosition = new Vector2(-690, 30);
        }
        else
        {

            RoleMenuPreview.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.5f);
            RoleMenuPreview.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0.5f);
            RoleMenuPreview.GetComponent<RectTransform>().anchoredPosition = new Vector2(-190, 30);
        }
    }
    //把角色菜单移动出视野范围
    public void MoveRoleMenuOut()
    {
        RoleMenuPreview.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.5f);
        RoleMenuPreview.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0.5f);
        RoleMenuPreview.GetComponent<RectTransform>().anchoredPosition = new Vector2(190,100);
    }
    //将菜单移入
    public void MoveMenu()
    {
        if (this.gameObject.transform.position.y > -5 && this.gameObject.transform.position.x > 7)
        {
            Menu.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.5f);
            Menu.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0.5f);
            Menu.GetComponent<RectTransform>().anchoredPosition = new Vector2(-690, 30);
        }
        else
        {

            Menu.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.5f);
            Menu.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0.5f);
            Menu.GetComponent<RectTransform>().anchoredPosition = new Vector2(-190, 30);
        }
    }
    //把菜单移动出视野范围
    public void MoveMenuOut()
    {

        Menu.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.5f);
        Menu.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0.5f);
        Menu.GetComponent<RectTransform>().anchoredPosition = new Vector2(400, 80);
    }
    //把战斗数据预览移动到中间位置
    public void MoveBattleDataPreview()
    {
        if (this.gameObject.transform.position.y > -5 && this.gameObject.transform.position.x > 7)
        {
            BattleDataPreview.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.5f);
            BattleDataPreview.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0.5f);
            BattleDataPreview.GetComponent<RectTransform>().anchoredPosition = new Vector2(-690, 30);
        }
        else
        {
            BattleDataPreview.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.5f);
            BattleDataPreview.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0.5f);
            BattleDataPreview.GetComponent<RectTransform>().anchoredPosition = new Vector2(-190, 30);
        }
    }
    //将战斗数值预览移除屏幕
    public void MoveBattleDataPreviewOut()
    {
        BattleDataPreview.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.5f);
        BattleDataPreview.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0.5f);
        BattleDataPreview.GetComponent<RectTransform>().anchoredPosition = new Vector2(400, 80);
    }

}