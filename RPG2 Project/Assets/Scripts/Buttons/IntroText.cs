using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroText : MonoBehaviour
{
    public GameObject part1;
    public GameObject part2;

    void Start()
    {
        StartCoroutine(IntroSequence());
        part2.SetActive(false);
    }

    IEnumerator IntroSequence()
    {
        part1.SetActive(true);
        yield return new WaitForSeconds(5f);
        part1.SetActive(false);
        part2.SetActive(true);
        yield return new WaitForSeconds(5f);
        //Load level
    }
}
