using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFront : MonoBehaviour
{
    Animator door;
    private bool inputListening = false;
    public Transform otherEnd;
    public GameObject Player;
    public float animTime;
    public Animator screen;
    // Start is called before the first frame update
    void Start()
    {
        door = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inputListening = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inputListening = false;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E) && inputListening == true )
        {
            Player.GetComponent<PlayerController>().enabled = false;
            StartCoroutine(DoorOpen());
            StartCoroutine(Wait());
        }

    }
    IEnumerator DoorOpen()
    {
        inputListening = false;
        door.SetTrigger("ToSide");
        yield return new WaitForSeconds(0.5f);
        door.SetTrigger("SideState");
        yield return new WaitForSeconds(3.0f);

        door.SetTrigger("ToFront");
        yield return new WaitForSeconds(0.5f);

        door.SetTrigger("FrontState");
        
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
        inputListening = true;
    }
}

