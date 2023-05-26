using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TierUp : MonoBehaviour  //Script not functional. Nothing goes through. An Attempt was made but I have spent a full week just malding over this one project already Ive had enough
{
    private InternalStats tierUp;
    private Movement doTierUp1;
    private BossAttacks doTierUp2;
    private PlayerEquip doTierUp3;
    // Start is called before the first frame update
    void Start()
    {
        doTierUp1 = GameObject.Find("Boss").GetComponent<Movement>();
        doTierUp2 = GameObject.Find("Boss").GetComponent<BossAttacks>();
        doTierUp3 = GameObject.Find("Player").GetComponent<PlayerEquip>();
        tierUp = GameObject.Find("InternalHolder").GetComponent<InternalStats>();
        StartCoroutine(JustMakingSureThisLoadsLast());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator JustMakingSureThisLoadsLast()//and in order
    {
        yield return new WaitForSeconds(1f);
        tierUp.CheckStat();
        yield return new WaitForSeconds(1f);
        if (tierUp.tierUpFlags >= 1)
        {
            doTierUp2.mHealth = doTierUp2.mHealth + 50f;
        }
        if (tierUp.tierUpFlags >= 2)
        {
            doTierUp1.attackCD = 5f;
        }
        if (tierUp.tierUpFlags >= 3)
        {
            doTierUp1.unlockedProj = true;
        }
        if (tierUp.tierUpFlags >= 4)
        {
            doTierUp3.damageMult = 1.3f;
        }
    }
    //You know what, its already so late and even with all of that delay this thing isn't doing anything so Im just gonna abandon this tier up gimmick Im so done Ill just leave it as Would want to do in the Documents or something
}
