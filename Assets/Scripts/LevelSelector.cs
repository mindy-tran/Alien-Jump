using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* SUBSCRIBING TO MY YOUTUBE CHANNEL: 'VIN CODES' WILL HELP WITH MORE VIDEOS AND CODE SHARING IN THE FUTURE :) THANK YOU */

public class LevelSelector : MonoBehaviour
{
    public int level;
    // public Text levelText;

    // Start is called before the first frame update
    void Start()
    {
        // levelText.text = level.ToString();
    }

    public void OpenScene()
    {
        SceneManager.LoadScene("Lvl " + level.ToString());
    }

}
