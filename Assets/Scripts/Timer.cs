using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time - startTime;

        string mins = ((int) t / 60).ToString();
        string sec =(t % 60).ToString("f0");

        timerText.text = mins + ":" + sec;
        
    }
}
