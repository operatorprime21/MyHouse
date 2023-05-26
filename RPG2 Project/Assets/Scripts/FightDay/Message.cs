using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message: MonoBehaviour
{
    public GameObject message;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Disable());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Disable()
    {
        yield return new WaitForSeconds(5f);
        message.SetActive(false);
    }
}
