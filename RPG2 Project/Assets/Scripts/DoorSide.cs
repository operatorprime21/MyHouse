using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSide : MonoBehaviour
{
    Animator door;
    public bool isOpen = false;
    private bool inputListening = false;
    public Collider2D doorStop;
    public GameObject detail;
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
            detail.SetActive(true);
        }
    }

    void OnTriggerExit2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            inputListening = false;
            detail.SetActive(false);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E) && isOpen == false && inputListening == true)
        {
            StartCoroutine(DoorOpen());
        }

        else if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E) && isOpen == true && inputListening == true)
        {
            StartCoroutine(DoorClose());
        }
    }
    IEnumerator DoorOpen()
    {
        inputListening = false;
        door.SetTrigger("ToFront");
        yield return new WaitForSeconds(1f);
        doorStop.isTrigger = true;
        door.SetTrigger("FrontState");
        yield return new WaitForSeconds(1.0f);
        isOpen = true;
        inputListening = true;
    }
    IEnumerator DoorClose()
    {
        inputListening = false;
        door.SetTrigger("ToSide");
        yield return new WaitForSeconds(1f);
        doorStop.isTrigger = false;
        door.SetTrigger("SideState");
        yield return new WaitForSeconds(1.0f);
        isOpen = false;
        inputListening = true;
    }
}
