using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    Animator trapAnim;
    private BossAttacks injure;
    private Movement stopSpeed;
    // Start is called before the first frame update
    void Start()
    {
        trapAnim = this.GetComponent<Animator>();
        injure = GameObject.Find("Boss").GetComponent<BossAttacks>();
        stopSpeed = GameObject.Find("Boss").GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boss")
        {
            trapAnim.SetTrigger("trapped");
            injure.cHealth = injure.cHealth - 30f;
            StartCoroutine(Trapped());
        }
    }

    IEnumerator Trapped()
    {
        stopSpeed.moveSpeed = 0f;
        yield return new WaitForSeconds(10f);
        stopSpeed.moveSpeed = 0.01f;
    }

    void Closed()
    {
        trapAnim.SetTrigger("closed");
    }
}
