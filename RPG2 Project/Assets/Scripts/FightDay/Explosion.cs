using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private bool inRadius;
    private BossAttacks injure;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        StartCoroutine(Explode());
    }
    // Update is called once per frame
    void Update()
    {
        injure = GameObject.Find("Boss").GetComponent<BossAttacks>();
    }
    IEnumerator Explode()
    {
        yield return new WaitForSeconds(3f);
        if(inRadius == true)
        {
            injure.cHealth = injure.cHealth - 50f;
        }
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boss")
        {
            inRadius = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Boss")
        {
            inRadius = false;
        }
    }
}
