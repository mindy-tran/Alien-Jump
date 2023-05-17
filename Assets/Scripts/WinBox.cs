using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinBox : MonoBehaviour
{
    // when player enters box, the timer stops and turns red
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        GameObject.Find("EventSystem").SendMessage("Finnish");
    }
}
