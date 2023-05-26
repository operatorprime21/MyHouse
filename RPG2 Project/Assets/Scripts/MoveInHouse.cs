using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInHouse: MonoBehaviour 
{
    public Transform otherEnd;
    public GameObject Player;
    public bool animPlaying = false;
    public float animTime;
    public Animator screen;
    public GameObject detail;
    // Start is called before the first frame update
    
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && Input.GetKey(KeyCode.E) && animPlaying == false)
        {

            animPlaying = true;
            Player.GetComponent<PlayerController>().enabled = false;
            StartCoroutine(Wait());
            //Start a coroutine to phase out screen and delay the repeated inputs
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            detail.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            detail.SetActive(false);
        }
    }

    IEnumerator Wait()
    {
        //The following animations are for screen fading
        yield return new WaitForSeconds(animTime);
        screen.SetTrigger("FadeOut");
        yield return new WaitForSeconds(2);
        screen.SetTrigger("Black");
        Player.transform.position = otherEnd.position;
        yield return new WaitForSeconds(2);
        screen.SetTrigger("FadeIn");
        yield return new WaitForSeconds(2);
        screen.SetTrigger("Show");
        //Enable Player controls
        Player.GetComponent<PlayerController>().enabled = true;
        animPlaying = false;
    }
}
