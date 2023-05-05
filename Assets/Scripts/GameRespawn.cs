 using UnityEngine;
 using System.Collections;
 
 public class GameRespawn : MonoBehaviour {
     public float threshold;
     
     private Vector3 spawnpoint = new Vector3(0f,0f,0f); //spawn on the center platform
 
     void FixedUpdate () {
         if (transform.position.y < threshold)
             transform.position = spawnpoint;
     }

     private void OnTriggerEnter(Collider other){
			if(other.gameObject.CompareTag("Respawn")){
                //character reached a checkpoint, change the spawnpoint 
                spawnpoint = transform.position;
            }
     }
           
 }