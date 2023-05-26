using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Time : MonoBehaviour
{
    public int timeHour;
    public bool newInterval;
    public Text timeText;
    public Text dayText;
    public int day;
    // Start is called before the first frame update
    void Start()
    {
        newInterval = true;
        day = 1;
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = timeHour +" hours left today";
        dayText.text = "Day: " + day;
        if(newInterval == true)
        {
            StartCoroutine(TimeDeduce());
        }
        if (timeHour <= 0)
        {
            day++;
            timeHour = timeHour + 24;
        }
        if (day == 8)
        {
           
        }
    }
    IEnumerator TimeDeduce()
    {
        newInterval = false;
        yield return new WaitForSeconds(15);
        timeHour--;
        newInterval = true;
    }
    public void ActivityTimeDeduce(int timeCost)
    {
        timeHour = timeHour - timeCost;
    }
}
