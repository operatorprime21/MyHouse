using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginPrep : MonoBehaviour
{
    public GameObject instructions;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Instruct());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Instruct()
    {
        instructions.SetActive(true);
        yield return new WaitForSeconds(7f);
        instructions.SetActive(false);
    }
}
