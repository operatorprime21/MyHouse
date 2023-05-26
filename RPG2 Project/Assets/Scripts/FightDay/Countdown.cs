using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
    public Text timeText;
    public int timeHour = 24;
    public bool newInterval;
    // Start is called before the first frame update
    void Start()
    {
        newInterval = true;
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = timeHour + " hours left. Overcome or survive the day";
        if (newInterval == true)
        {
            Debug.Log("Hour passed");
            StartCoroutine(TimeDeduce());
        }
        if (timeHour <= 0)
        {
            SceneManager.LoadScene(3);
        }
    }
    IEnumerator TimeDeduce()
    {
        newInterval = false;
        yield return new WaitForSeconds(15);
        timeHour = timeHour - 1;
        newInterval = true;
    }
    
}
