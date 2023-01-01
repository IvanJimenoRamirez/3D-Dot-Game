using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManage : MonoBehaviour
{
    public Animator animator;
    public GameObject MainMenu, InstructionsMenu, CreditsMenu;

    private int option;
    
    // Start is called before the first frame update
    void Start()
    {
        option = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Starts the fadeOut of the scene
    public void fadeOut(int op)
    {
        option = op;
        animator.SetTrigger("FadeOutTrigger");
    }

    //When the fade out of the scene finishes, changes to game Scene
    public void fadeOutFinished()
    {
        switch(option)
        {
            case -1: //exit game
                Debug.Log("Exit game");
                Application.Quit();
                break;
            case 0: //go to main menu scene
            case 1: //go to game scene
                SceneManager.LoadScene(option);
                break;
            case 2: //go to instructions fadein
                MainMenu.SetActive(false);
                InstructionsMenu.SetActive(true);
                animator.SetTrigger("FadeInTrigger");
                break;
            case 3: //go to credits fadein
                MainMenu.SetActive(false);
                CreditsMenu.SetActive(true);
                animator.SetTrigger("FadeInTrigger");
                break;
            case 4: //go back to main menu
                InstructionsMenu.SetActive(false);
                CreditsMenu.SetActive(false);
                MainMenu.SetActive(true);
                animator.SetTrigger("FadeInTrigger");
                break;
        }
    }
}
