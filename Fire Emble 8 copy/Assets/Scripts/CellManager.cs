using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//创建地图相关
public class CellManager : MonoBehaviour {

    public GameObject map;
    public Sprite[] sprites;

    public int length = 15;
    public int height = 10;

    void Awake() {

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < length; x++)
            {
                Instantiate(map, new Vector3(x, y >= 1 ? -y : y, 0), Quaternion.identity, transform);
            }
        }
    }

    void Start () {
        SpriteRenderer[] maps = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < maps.Length; i++)
        {
            maps[i].sprite = sprites[i];
        }
	}
}
