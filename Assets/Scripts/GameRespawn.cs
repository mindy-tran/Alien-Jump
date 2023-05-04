 using UnityEngine;
 using System.Collections;
 
 public class GameRespawn : MonoBehaviour {
     public float threshold;
     
     private Vector3 spawnpoint = new Vector3(0f,0f,0f);
 
     void FixedUpdate () {
         if (transform.position.y < threshold)
             transform.position = spawnpoint;
     }

     private void OnTriggerEnter(Collider other){
			if(other.gameObject.CompareTag("Respawn"))
            {
                print("spawn set");
                spawnpoint = transform.position;
            }
     }
           
 }