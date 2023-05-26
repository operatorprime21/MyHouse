using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCol : MonoBehaviour
{
    public GameObject player;
    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), boss.GetComponent<Collider2D>());
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
