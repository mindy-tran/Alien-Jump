 using UnityEngine;
 using System.Collections;
 
 public class GameRespawn : MonoBehaviour {
     public float threshold;
     public GameObject saveText;
     public GameObject tutorialControls;
     public AudioSource checkpointSound;
     
     private Vector3 spawnpoint = new Vector3(0f,1f,0f); //spawn on the center platform
 

     void Start(){
        saveText.SetActive(false);
        tutorialControls.SetActive(false);
     }


     void FixedUpdate () {
         if (transform.position.y < threshold)
             transform.position = spawnpoint;
     }

     private void OnTriggerEnter(Collider other){
			if(other.gameObject.CompareTag("Respawn")){
                //character reached a checkpoint, change the spawnpoint 
                if (Vector3.Distance(spawnpoint, transform.position) >= 4) {
                    // we are at a new spawn point so we need to update it
                    spawnpoint = transform.position;
                    saveText.SetActive(true);
                    checkpointSound.Play();
                    Invoke("EndSaveText", 3f);
                

                }
            }

            //tutorial controls will pop up when character is at Lv1 start
			if (other.gameObject.CompareTag("Start")){
				tutorialControls.SetActive(true);
			}
     }

     private void EndSaveText(){
        saveText.SetActive(false);
     }
           
 }