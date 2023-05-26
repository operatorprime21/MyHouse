using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basement : MonoBehaviour
{
    public bool nearInteract = false;
    public float animTime;
    public GameObject Player;
    private PlayerStat addStat;
    private Time takeTime;
    public Animator screen;
    private Inventory addItem;
    private InternalStats addHidden;
    public Animator seat;

    private bool pick;
    public GameObject optionUI;
    public GameObject lackResource;
    public GameObject craftMenu;
    public GameObject buildMenu;
    public GameObject SpikedBatRecipe;
    public GameObject ShotgunRecipe;
    private bool crafting;
    private bool building;
    public bool hasShotgunRecipe;
    public bool hasSpikedBatRecipe;

    public GameObject detail;
    // Start is called before the first frame update
    void Start()
    {
        pick = false;
        optionUI.SetActive(false);
        crafting = false;
        craftMenu.SetActive(false);
        building = false;
        buildMenu.SetActive(false);
        lackResource.SetActive(false);
        hasSpikedBatRecipe = false;
        hasShotgunRecipe = false;
        detail.SetActive(false);
        SpikedBatRecipe.SetActive(false);
        ShotgunRecipe.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
        //Access every data possible
        addStat = GameObject.Find("StatHolder").GetComponent<PlayerStat>();
        takeTime = GameObject.Find("TimeHolder").GetComponent<Time>();
        addItem = GameObject.Find("InventoryHolder").GetComponent<Inventory>();
        addHidden = GameObject.Find("OthersHolder").GetComponent<InternalStats>();
        int metal = addItem.metal;
        int leather = addItem.leather;
        int wood = addItem.wood;
        int nail = addItem.nail;
        int rope = addItem.rope;
        int gunpowder = addItem.gunpowder;
        //Read input
        if (Input.GetKeyDown(KeyCode.E) && nearInteract == true && pick == false)
        {
            Debug.Log("Pressed E");
            pick = true;
            optionUI.SetActive(true);
            detail.SetActive(false);
            //Bool to prevent repeated inputs
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && pick == true)
        {
            crafting = true;
            craftMenu.SetActive(true);
            pick = false;
            optionUI.SetActive(false);
            if (hasSpikedBatRecipe == true)
            {
                SpikedBatRecipe.SetActive(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && crafting == true)
        {
            crafting = false;
            craftMenu.SetActive(false);
            pick = true;
            optionUI.SetActive(true);
            SpikedBatRecipe.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && crafting == true)
        {
            if (metal > 1 && gunpowder > 0)
            {
                Craft();
                addItem.ChangeMetal(-2);
                addItem.ChangeGunpowder(-1);
                addItem.ChangeAmmo(3);
            }
            else 
            {
                StartCoroutine(NotEnoughResource());
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && crafting == true)
        {
            if (nail > 0 && wood > 0)
            {
                Craft();
                addItem.ChangeNail(-1);
                addItem.ChangeWood(-1);
                addItem.ChangeLocker(1);
            }
            else
            {
                StartCoroutine(NotEnoughResource());
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && crafting == true)
        {
            if (nail > 1 && rope > 1 && metal > 0)
            {
                Craft();
                addItem.ChangeNail(-2);
                addItem.ChangeRope(-2);
                addItem.ChangeMetal(-1);
                addItem.ChangeTrap(1);
            }
            else
            {
                StartCoroutine(NotEnoughResource());
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && crafting == true && hasSpikedBatRecipe == true)
        {
            if (nail > 4)
            {
                Craft();
                addItem.ChangeNail(-4);
                addItem.GetSpikedBat();
            }
            else
            {
                StartCoroutine(NotEnoughResource());
            }
        }


        else if (Input.GetKeyDown(KeyCode.Alpha2) && pick == true)
        {
            building = true;
            buildMenu.SetActive(true);
            pick = false;
            optionUI.SetActive(false);
            if (hasShotgunRecipe == true)
            {
                ShotgunRecipe.SetActive(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && building == true)
        {
            building = false;
            buildMenu.SetActive(false);
            pick = true;
            optionUI.SetActive(true);
            ShotgunRecipe.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && building == true)
        {
            if (wood > 3 && rope > 1 && leather > 1)
            {
                Build();
                addItem.ChangeWood(-4);
                addItem.ChangeRope(-2);
                addItem.ChangeLeather(-2);
                addItem.ChangeBarricade(1);
            }
            else
            {
                StartCoroutine(NotEnoughResource());
            }    
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && building == true)
        {
            if (gunpowder > 2 && rope > 0)
            {
                Build();
                addItem.ChangeGunpowder(-3);
                addItem.ChangeRope(-1);
                addItem.ChangeBomb(1);
            }
            else
            {
                StartCoroutine(NotEnoughResource());
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && building == true && hasShotgunRecipe == true)
        {
            if (gunpowder > 0 && metal > 1 && wood > 1)
            {
                Build();
                addItem.ChangeGunpowder(-1);
                addItem.ChangeMetal(-2);
                addItem.ChangeWood(-2);
                addItem.GetShotgun();
            }
            else
            {
                StartCoroutine(NotEnoughResource());
            }
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
        Player.GetComponent<SpriteRenderer>().enabled = false;
        seat.SetTrigger("Sitting");
        yield return new WaitForSeconds(1);
        seat.SetTrigger("Seated");
        //Activity.SetTrigger("Animation of this activity");
        //The following animations are for screen fading
        yield return new WaitForSeconds(animTime);
        screen.SetTrigger("FadeOut");
        yield return new WaitForSeconds(2);
        screen.SetTrigger("Black");
        yield return new WaitForSeconds(2);
        screen.SetTrigger("FadeIn");
        seat.SetTrigger("Nothing");
        Player.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(2);
        screen.SetTrigger("Show");
        //Enable Player controls
        Player.GetComponent<PlayerController>().enabled = true;

        nearInteract = true;
    }

    void Craft()
    {
        nearInteract = false;
        //Do whatever we do with the associated 
        addStat.AddCleanliness(-1);
        addStat.AddFlexibility(2);
        takeTime.ActivityTimeDeduce(4);
        addHidden.addCraft(1);
        //Play Animations
        StartCoroutine(ActivityAnimPlaying());
    }

    void Build()
    {
        nearInteract = false;
        //Do whatever we do with the associated 
        addStat.AddCleanliness(-2);
        addStat.AddFlexibility(2);
        addStat.AddEndurance(1);
        takeTime.ActivityTimeDeduce(6);
        addHidden.addBuild(1);
        //Play Animations
        StartCoroutine(ActivityAnimPlaying());
    }

    IEnumerator NotEnoughResource()
    {
        lackResource.SetActive(true);
        yield return new WaitForSeconds(2);
        lackResource.SetActive(false);
    }

    public void UnlockSpikedBat()
    {
        hasSpikedBatRecipe = true;
    }

    public void UnlockShotgun()
    {
        hasShotgunRecipe = true;
    }

}
