using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public GameObject boss;
    public float speed;
    public float direction;
    // Start is called before the first frame update
    void Start()
    {
       // boss = GameObject.Find("Boss").GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = boss.transform.localScale.x;
        this.transform.localScale = boss.transform.localScale;
        Vector3 projectilePos = this.transform.position;
        projectilePos.x = projectilePos.x + speed * direction;
        this.transform.position = projectilePos;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "house terrain" || collision.tag == "Player" || collision.tag == "Obstacles")
        {
            Destroy(gameObject);
        }
    }
}
