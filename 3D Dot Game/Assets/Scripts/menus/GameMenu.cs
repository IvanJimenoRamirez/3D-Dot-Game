using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public static bool shown = false;
    public GameObject gameMenu;
    public GameObject fadeManage;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (shown) continueGame();
            else show();
        }
    }
    public void continueGame()
    {
        gameMenu.SetActive(false);
        Time.timeScale = 1f;
        shown = false;
    }

    private void show()
    {
        gameMenu.SetActive(true);
        Time.timeScale = 0f;
        shown = true;
    }
    public void goToMenu()
    {
        Time.timeScale = 1f;
        fadeManage.GetComponent<FadeManage>().fadeOut(0);
    }

    public void exit()
    {
        //Debug.Log("Exit game");
        //Application.Quit();
        fadeManage.GetComponent<FadeManage>().fadeOut(-1);
    }

}
