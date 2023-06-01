using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu;

   

    void Update(){
        if (Input.GetKeyDown(KeyCode.P)) {
            pause();
        }
    }

    public void pause(){
        Time.timeScale = 0f; // pause timer
        pauseMenu.SetActive(true);

    }

    public void resume(){
        pauseMenu.SetActive(false); // close the pause menu
        Time.timeScale = 1f; // sest time back to normal

    }

    public void restart(){
        Time.timeScale = 1f; // time is normal 

        //restart the current level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  

    }

    public void menu(){
        Time.timeScale = 1f; //time is normal
        SceneManager.LoadScene("MainMenu");

    }
    
}
