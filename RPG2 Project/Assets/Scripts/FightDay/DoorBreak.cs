using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBreak : MonoBehaviour
{
    public float obsHealth;
    private DoorSide doorState;
    private bool inCD;
    private Inventory addItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        doorState = this.GetComponent<DoorSide>();
        addItem = GameObject.Find("InventoryHolder").GetComponent<Inventory>();
        if (obsHealth <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Boss" && doorState.isOpen == false && inCD == false)
        {
            StartCoroutine(DrainHealth());
        }
        if(collision.tag == "Player" && Input.GetKeyDown(KeyCode.L) && addItem.locker > 0)
        {
            obsHealth = obsHealth + 4;
            addItem.ChangeLocker(-1);
        }
    }
    IEnumerator DrainHealth()
    {
        inCD = true;
        yield return new WaitForSeconds(4f);
        obsHealth--;
        inCD = false;
    }
    
}
