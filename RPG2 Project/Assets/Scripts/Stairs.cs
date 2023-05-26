using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    public Collider2D stairs;
    public Collider2D secondFloor;
    public GameObject detail;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            Debug.Log("step"); 
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S) == false)
            {
                stairs.isTrigger = true;
                secondFloor.isTrigger = false; 
            }
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) == false)
            {
                stairs.isTrigger = true;
                secondFloor.isTrigger = false;
            }
            else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S) == true)
            {
                stairs.isTrigger = false;
                secondFloor.isTrigger = true;
            }
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) == true)
            {
                stairs.isTrigger = false;
                secondFloor.isTrigger = true;
            }
        }
    }
}
