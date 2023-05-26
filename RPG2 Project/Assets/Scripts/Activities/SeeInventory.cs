using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeInventory : MonoBehaviour
{
    public bool nearInteract = false;
    public GameObject Player;
    public GameObject InventoryUI;
    private bool uiOn;
    public GameObject detail;
    // Start is called before the first frame update
    void Start()
    {
        InventoryUI.SetActive(false);
        uiOn = false;
        detail.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Access every data possible
        
        //Read input
        if (Input.GetKeyDown(KeyCode.E) && nearInteract == true)
        {
            uiOn = true;
            nearInteract = false;
            InventoryUI.SetActive(true);
            detail.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && uiOn == true)
        {
            InventoryUI.SetActive(false);
            uiOn = false;
            nearInteract = true;
            detail.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Enable text and activity prompt
        if (other.tag == "Player" && nearInteract == false)
        {
            nearInteract = true;
            detail.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        //Disable text and prompt
        if (other.tag == "Player" && nearInteract == true)
        {
            nearInteract = false;
            detail.SetActive(false);
        }
    }
}
