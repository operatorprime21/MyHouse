using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEquip : MonoBehaviour
{
    public List<GameObject> Equipped = new List<GameObject>();

    public int equipState;
    public GameObject batHitbox;
    public GameObject spikedHitbox;
    public GameObject gunHitbox;
    public GameObject knifeHitbox;
    Animator attack;
    private Inventory addItem;
    private int ammo;
    public Rigidbody2D bomb;
    public GameObject trap;
    public GameObject barri;
    public Transform boss;
    private float distance;
    private float forceDirection;
    public float maxHealth;
    public float currHealth;
    public Rigidbody2D rbPlayer;
    private bool regenCD;
    private PlayerStat readStat;
    public float damageMult = 1f;
    // Start is called before the first frame update
    void Start()
    {
        attack = this.GetComponent<Animator>();
        readStat = GameObject.Find("StatHolder").GetComponent<PlayerStat>();
        maxHealth = 100f + readStat.statKnowledge * 5f;
        currHealth = maxHealth;
    }
    IEnumerator Regen()
    {
        regenCD = true;
        currHealth = currHealth + 2f;
        yield return new WaitForSeconds(1f);
        regenCD = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "whipAttack")
        {
            Debug.Log("hit");
            currHealth = currHealth - 10f*damageMult;
            rbPlayer.AddRelativeForce(new Vector2(200 * forceDirection,50));
        }
        if (other.tag == "projAttack")
        {
            currHealth = currHealth - 15f*damageMult;
            rbPlayer.AddRelativeForce(new Vector2(250 * forceDirection, 50));
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (readStat.statKnowledge >= 20 && regenCD == false)
        {
            StartCoroutine(Regen());
        }
        if (currHealth >= maxHealth)
        {
            currHealth = maxHealth;
        }
        if(Input.GetKeyDown(KeyCode.Alpha5) && addItem.carrot > 0)
        {
            currHealth = currHealth + 20f;
            addItem.ChangeCarrot(-1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) && addItem.corn > 0)
        {
            currHealth = currHealth + 30f; 
            addItem.ChangeCorn(-1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) && addItem.carrot > 0)
        {
            currHealth = currHealth + 50f;
            addItem.ChangeMelon(-1);
        }
        if (Input.GetKeyDown(KeyCode.B) && addItem.barricade > 0)
        {
            Instantiate(barri, new Vector2 (this.transform.position.x + 2f*this.transform.localScale.x, this.transform.position.y + 1.5f), this.transform.rotation);
            addItem.ChangeBarricade(-1);
        }
        if(Input.GetKeyDown(KeyCode.F) && addItem.bomb > 0)
        {
            addItem.ChangeBomb(-1);
            Equipped[4].SetActive(false);
            Rigidbody2D newBomb = Instantiate(bomb, new Vector2(this.transform.position.x + 1f * this.transform.localScale.x, this.transform.position.y), this.transform.rotation);
            newBomb.AddRelativeForce(new Vector2(1000 * this.transform.localScale.x, 50));
        }
        if(Input.GetKeyDown(KeyCode.T) && addItem.trap > 0)
        {
            Instantiate(trap, new Vector2(this.transform.position.x + 2f * this.transform.localScale.x, this.transform.position.y), this.transform.rotation);
            addItem.ChangeTrap(-1);
        }
        distance = this.transform.position.x - boss.position.x;
        if(distance > 0)
        {
            forceDirection = 1f;
        }
        else if (distance < 0)
        {
            forceDirection = -1f;
        }
        addItem = GameObject.Find("InventoryHolder").GetComponent<Inventory>();
        ammo = addItem.ammo;
        if (Input.GetKeyDown(KeyCode.Alpha1) && addItem.hasKnife == true)
        {
            ChangeEquippment(0);
            equipState = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && addItem.hasBat == true)
        {
            ChangeEquippment(1);
            equipState = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && addItem.hasSpikedBat == true)
        {
            ChangeEquippment(2);
            equipState = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && addItem.hasShotgun == true)
        {
            ChangeEquippment(3);
            equipState = 3;
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (equipState == 0)
            {
                Equipped[0].SetActive(false);
                attack.SetTrigger("Knife");
                this.GetComponent<PlayerController>().enabled = false;
            }
            else if (equipState == 1)
            {
                Equipped[1].SetActive(false);
                attack.SetTrigger("Bat");
                this.GetComponent<PlayerController>().enabled = false;
            }
            else if (equipState == 2)
            {
                Equipped[2].SetActive(false);
                attack.SetTrigger("SpikedBat");
                this.GetComponent<PlayerController>().enabled = false;
            }
            else if (equipState == 3 && ammo > 0)
            {
                Equipped[3].SetActive(false);
                attack.SetTrigger("Gun");
                this.GetComponent<PlayerController>().enabled = false;
            }
        }
        if(currHealth <= 0)
        {
            SceneManager.LoadScene(4);
        }

    }
    void BackToIdle()
    { 
        this.GetComponent<PlayerController>().enabled = true;
        Equipped[equipState].SetActive(true);
        Debug.Log("on");
        attack.SetTrigger("Idle");
    }
    void ChangeEquippment(int curEquip)
    {
        for (int i = 0; i < 4; i++)
        {
            Equipped[i].SetActive(false);
            if (i == curEquip)
            {
                Equipped[curEquip].SetActive(true);
            }
        }
    }
    IEnumerator SetActiveHitbox()
    {
        if (equipState == 0)
        {
            knifeHitbox.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            knifeHitbox.SetActive(false);
        }
        if (equipState == 1)
        {
            batHitbox.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            batHitbox.SetActive(false);
        }
        if (equipState == 2)
        {
            spikedHitbox.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            spikedHitbox.SetActive(false);
        }
        if (equipState == 3 && ammo > 0)
        {
            gunHitbox.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            gunHitbox.SetActive(false);
            addItem.ChangeAmmo(-1);
        }
    }
}
