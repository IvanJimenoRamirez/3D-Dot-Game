using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject fadeManage;

    public void Play()
    {
        fadeManage.GetComponent<FadeManage>().fadeOut(1);
    }

    public void Exit()
    {
        fadeManage.GetComponent<FadeManage>().fadeOut(-1);
    }

    public void goToInstructions()
    {
        fadeManage.GetComponent<FadeManage>().fadeOut(2);
    }

    public void goToCredits()
    {
        fadeManage.GetComponent<FadeManage>().fadeOut(3);
    }

    public void goBackToMainMenu()
    {
        fadeManage.GetComponent<FadeManage>().fadeOut(4);
    }

}
