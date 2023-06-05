using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Win : MonoBehaviour
{

    public TMP_Text timerText;
    public GameObject s_1; // colored in stars
    public GameObject s_2;
    public GameObject s_3;

    public float time1star; // in seconds
    public float  time2star; 
    public float time3star; 




    void OnTriggerEnter(Collider other){
        win();
    }
    


    public void win(){
        // display the stars :)
        int mins = Int32.Parse(timerText.text.Split(":")[0]);
        float secs = float.Parse(timerText.text.Split(":")[1]);

        float time = (60 * mins) + secs;
        print(time);
        
        s_1.SetActive(false);
        s_2.SetActive(false);
        s_3.SetActive(false);

        print(time1star);

        if ((time < time1star)){
            // under time get one star
            s_1.SetActive(true);
            if ((time < time2star)){
                // under the time get 2 stars
                s_2.SetActive(true);
                if ((time < time3star)) {
                    //under time get 3 stars
                    s_3.SetActive(true);
                }
            }
        }


    }
    public void menu(){
        SceneManager.LoadScene("MainMenu");

    }

   public void replay(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  
   }

   public void next(){
        string[] parts = SceneManager.GetActiveScene().name.Split(' ');
        int Nextlevel = Int32.Parse(parts[1]) + 1;

        SceneManager.LoadScene("Lvl " + Nextlevel.ToString());
   }
}
