using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charisma : MonoBehaviour
{
    public bool inCD = false;
    public bool enoughCharisma = false;
    private PlayerStat readStat;
    private Movement stun;
    public float storeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        readStat = GameObject.Find("StatHolder").GetComponent<PlayerStat>();
        
        if (readStat.statCharisma >= 5)
        {
            enoughCharisma = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        stun = GameObject.Find("Boss").GetComponent<Movement>();
        if (enoughCharisma == true && inCD == false)
        {
            storeSpeed = stun.moveSpeed;
            StartCoroutine(DogStun());
        }
        
    }
    IEnumerator DogStun()
    {
        Debug.Log("Woof");
        inCD = true;
        stun.moveSpeed = 0f;
        yield return new WaitForSeconds(10f);
        stun.moveSpeed = storeSpeed;
        yield return new WaitForSeconds(60f - readStat.statCharisma);
        inCD = false;
    }
}
