using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonA : MonoBehaviour {

    private void Awake()
    {
        Screen.SetResolution(620,420, false);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
