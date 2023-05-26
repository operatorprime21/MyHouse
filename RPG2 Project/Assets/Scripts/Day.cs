using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Day : MonoBehaviour
{
    public Time seeDay;

    void Start()
    {
        seeDay = GameObject.Find("TimeHolder").GetComponent<Time>();
    }
    // Start is called before the first frame update
    void Update()
    {
        int day = seeDay.day;
        if(day == 8)
        {
            SceneManager.LoadScene(2);
        }
    }
}
