using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator anim;
	private CharacterController controller;


    public float minWaitSec; 
    public float maxWaitSec; 

    private bool isDead;
    private Vector3 moveDirection = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        isDead = false; // enemy is not dead
        controller = GetComponent <CharacterController>();
		anim = gameObject.GetComponentInChildren<Animator>();


        StartCoroutine(JumpLogic());
  
    }

    private IEnumerator JumpLogic(){
        // multithreading :)
        while(true){
            //wait a random amount of time between min and max
            float waitTime = Random.Range(minWaitSec, maxWaitSec);
            yield return new WaitForSeconds(waitTime);


            // time is now up, so we can now do something
            if (!isDead) {
                //character is not dead, so now we jump
                Jump();
            }

        }

    }
    
    void Jump(){
        moveDirection.y = 8;

    }

    void Update() {

        controller.Move(moveDirection * Time.deltaTime);
        moveDirection.y -= 32.0f * Time.deltaTime;

    }

    

}
