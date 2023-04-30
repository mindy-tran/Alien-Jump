// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
 
// [RequireComponent(typeof(CharacterController))]
// public class DoubleJump : MonoBehaviour
// {
//     private CharacterController controller;

//     public float jumpSpeed = 2.0f;
//     public float gravity = 10.0f;
//     private Vector3 movingDirection = Vector3.zero;

//     void Update() {
//         if (controller.isGrounded && Input.GetButton("Jump"))
//         {
//             movingDirection.y = jumpSpeed;
//         }
//         movingDirection.y -= gravity * Time.deltaTime;
//         controller.Move(movingDirection * Time.deltaTime);
//     }
// }