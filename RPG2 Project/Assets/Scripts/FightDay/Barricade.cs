using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    public float obsHealth;
    private bool inCD;
    private Inventory addItem;
    // Start is called before the first frame update
    void Start()
    {
        obsHealth = 7;
    }

    // Update is called once per frame
    void Update()
    {
        if (obsHealth <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Boss" && inCD == false)
        {
            StartCoroutine(DrainHealth());
        }
    }
    IEnumerator DrainHealth()
    {
        inCD = true;
        yield return new WaitForSeconds(6f);
        obsHealth--;
        inCD = false;
    }
}
