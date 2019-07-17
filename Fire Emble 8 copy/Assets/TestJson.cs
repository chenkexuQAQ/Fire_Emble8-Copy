using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJson : MonoBehaviour {

	// Use this for initialization
	void Start () {
        FromJson();

    }
    [SerializeField]
    class Character
    {
        public string Name;
    }

    [SerializeField]
    class CharacterAttribute
    {
        public List<Character> Attribute;
    }
    void FromJson()
    {
        TextAsset ta = Resources.Load<TextAsset>("CharacterAttribute");
        CharacterAttribute character = JsonUtility.FromJson<CharacterAttribute>(ta.text);
        Debug.Log(character.Attribute);
        foreach (Character c in character.Attribute)
        {
            Debug.Log(c.Name);
        }
    }
}
