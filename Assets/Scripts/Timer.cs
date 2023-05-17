using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    private float startTime;
    private bool finnished = false;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(finnished){
            return;
        }

        float t = Time.time - startTime;

        string mins = ((int) t / 60).ToString();
        string sec =(t % 60).ToString("f2");

        timerText.text = mins + ":" + sec;    
    }

    public void Finnish()
    {
        finnished = true;
        timerText.color = Color.red;
    }
}
