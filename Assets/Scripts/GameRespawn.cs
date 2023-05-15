 using UnityEngine;
 using System.Collections;
 
 public class GameRespawn : MonoBehaviour {
     public float threshold;
     public GameObject saveText;
     public AudioSource checkpointSound;
     
     private Vector3 spawnpoint = new Vector3(0f,0f,0f); //spawn on the center platform
 

     void Start(){
        saveText.SetActive(false);
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
     }

     private void EndSaveText(){
        saveText.SetActive(false);
     }
           
 }