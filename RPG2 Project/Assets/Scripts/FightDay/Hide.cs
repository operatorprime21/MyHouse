using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerStat checkStat;
    public GameObject player;
    private bool hiding;
    private bool inCD;
    void Start()
    {
        checkStat = GameObject.Find("StatHolder").GetComponent<PlayerStat>();
        hiding = false;
        inCD = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hiding == true)
        {
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            player.GetComponent<Rigidbody2D>().gravityScale = 0f;
            //Debug.Log("true");
        }
        if (hiding == false)
        {
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            player.GetComponent<Rigidbody2D>().gravityScale = 1f;
            //Debug.Log("false");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && checkStat.statFlexibility >= 20)
        {
            if (Input.GetKeyDown(KeyCode.E) && hiding == false && inCD == false)
            {
                Debug.Log("EEEE");
                player.GetComponent<SpriteRenderer>().enabled = false;
                player.GetComponent<PlayerController>().enabled = false;
                player.GetComponent<PlayerEquip>().enabled = false;
                player.layer = 8;
                hiding = true;
                player.GetComponent<BoxCollider2D>().isTrigger = true;
            }
            else if (Input.GetKeyDown(KeyCode.E) && hiding == true && inCD == false)
            {
                Debug.Log("A");
                player.GetComponent<SpriteRenderer>().enabled = true;
                player.GetComponent<PlayerController>().enabled = true;
                player.GetComponent<PlayerEquip>().enabled = true;
                player.layer = 6;
                hiding = false;
                player.GetComponent<BoxCollider2D>().isTrigger = false;
            }
        }
    }
    IEnumerator Cooldown()
    {
        inCD = true;
        yield return new WaitForSeconds(1f);
        inCD = false;
    }
}
