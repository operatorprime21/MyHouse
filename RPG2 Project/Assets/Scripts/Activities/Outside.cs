using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outside : MonoBehaviour
{
    public bool nearInteract = false;
    public float animTime;
    public GameObject Player;
    private PlayerStat addStat;
    private Time takeTime;
    public Animator screen;
    private Inventory addItem;
    private InternalStats addHidden;
    public GameObject ShoppingList;
    private bool pick;
    private bool shopping;
    public GameObject optionUI;
    public GameObject notEnoughMoney;
    public GameObject detail;
    // Start is called before the first frame update
    void Start()
    {
        pick = false;
        optionUI.SetActive(false);
        ShoppingList.SetActive(false);
        shopping = false;
        notEnoughMoney.SetActive(false);
        detail.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Access every data possible
        addStat = GameObject.Find("StatHolder").GetComponent<PlayerStat>();
        takeTime = GameObject.Find("TimeHolder").GetComponent<Time>();
        addItem = GameObject.Find("InventoryHolder").GetComponent<Inventory>();
        addHidden = GameObject.Find("OthersHolder").GetComponent<InternalStats>();
        //Read input
        float cash = addItem.cash;
        if (Input.GetKeyDown(KeyCode.E) && nearInteract == true && pick == false)
        {
            detail.SetActive(false);
            Debug.Log("Pressed E");
            pick = true;
            optionUI.SetActive(true);
            //Bool to prevent repeated inputs
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && pick == true)
        {
            nearInteract = false;
            //Do whatever we do with the associated 
            addStat.AddCharisma(2);
            addStat.AddEndurance(1);
            takeTime.ActivityTimeDeduce(3);
            addHidden.addWalk(1);
            //Play Animations
            StartCoroutine(ActivityAnimPlaying());
            pick = false;
            optionUI.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && pick == true)
        {
            nearInteract = false;
            ShoppingList.SetActive(true);
            //Give player 5 options
            shopping = true;
            pick = false;
            optionUI.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && shopping == true && cash > 11)
        {
            Shop();
            addItem.ChangeWood(2);
            addItem.ChangeMelonSeed(1);
            addItem.ChangeCash(-12);
            if (cash < 12)
            {
                StartCoroutine(lackMoney());
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && shopping == true && cash < 12)
        {
            StartCoroutine(lackMoney());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && shopping == true && cash > 13)
        {
            Shop();
            addItem.ChangeMetal(1);
            addItem.ChangeCarrotSeed(1);
            addItem.ChangeCash(-14);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && shopping == true && cash < 14)
        {
            StartCoroutine(lackMoney());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && shopping == true && cash > 15)
        {
            Shop();
            addItem.ChangeLeather(2);
            addItem.ChangeNail(2);
            addItem.ChangeCash(-16);
            if (cash < 16)
            {
                StartCoroutine(lackMoney());
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && shopping == true && cash < 16)
        {
            StartCoroutine(lackMoney());
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4) && shopping == true && cash > 29)
        {
            Shop();
            addItem.GetBat();
            addItem.ChangeNail(2);
            addItem.ChangeCash(-30);
            if (cash < 30)
            {
                StartCoroutine(lackMoney());
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && shopping == true && cash < 30)
        {
            StartCoroutine(lackMoney());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && shopping == true && cash > 12)
        {
            Shop();
            addItem.ChangeGunpowder(2);
            addItem.ChangeRope(1);
            addItem.ChangeCash(-13);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && shopping == true && cash < 13)
        {
            StartCoroutine(lackMoney());
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && shopping == true)
        {
            shopping = false;
            ShoppingList.SetActive(false);
            pick = true;
            optionUI.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pick == true)
        {
            pick = false;
            optionUI.SetActive(false);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Enable text and activity prompt
        if (other.tag == "Player" && nearInteract == false)
        {
            nearInteract = true;
            detail.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        //Disable text and prompt
        if (other.tag == "Player" && nearInteract == true)
        {
            nearInteract = false;
            detail.SetActive(false);
        }
    }
    IEnumerator ActivityAnimPlaying()
    {
        //Play animation here
        //Disable Player controls
        Player.GetComponent<PlayerController>().enabled = false;
        detail.SetActive(false);

        yield return new WaitForSeconds(1);

        //Activity.SetTrigger("Animation of this activity");
        //The following animations are for screen fading
        yield return new WaitForSeconds(animTime);
        screen.SetTrigger("FadeOut");
        yield return new WaitForSeconds(2);
        screen.SetTrigger("Black");
        yield return new WaitForSeconds(2);
        screen.SetTrigger("FadeIn");
  
   
        yield return new WaitForSeconds(2);
        screen.SetTrigger("Show");
        //Enable Player controls
        Player.GetComponent<PlayerController>().enabled = true;

        nearInteract = true;
    }

    void Shop()
    {
        addStat.AddCharisma(1);
        addStat.AddEndurance(1);
        addStat.AddKnowledge(1);
        takeTime.ActivityTimeDeduce(6);
        StartCoroutine(ActivityAnimPlaying());
        pick = false;
        optionUI.SetActive(false);
        shopping = false;
        ShoppingList.SetActive(false);
    }
    IEnumerator lackMoney()
    {
        notEnoughMoney.SetActive(true);
        yield return new WaitForSeconds(2);
        notEnoughMoney.SetActive(false);

    }
}
