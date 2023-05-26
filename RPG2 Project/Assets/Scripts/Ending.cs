using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BackToMenu());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(0);
    }
}
