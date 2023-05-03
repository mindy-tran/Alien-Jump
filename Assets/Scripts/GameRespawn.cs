 using UnityEngine;
 using System.Collections;
 
 public class GameRespawn : MonoBehaviour {
     public float threshold;
 
     void FixedUpdate () {
         if (transform.position.y < threshold)
             transform.position = new Vector3(0, 0, 0);
     }
 }