using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gardening : MonoBehaviour
{
    public bool nearInteract = false;
    public float animTime;
    public GameObject Player;
    private PlayerStat addStat;
    private Time takeTime;
    public Animator screen;
    private Inventory addItem;
    private InternalStats addHidden;
    public GameObject detail;
    private bool pick;
    public GameObject optionUI;

    public Text seed1;
    public Text seed2;
    public Text seed3;
    public GameObject outOfSeed;
    // Start is called before the first frame update
    void Start()
    {
        pick = false;
        optionUI.SetActive(false);
        outOfSeed.SetActive(false);
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

        float carrotSeed = addItem.carrotSeed;
        float cornSeed = addItem.cornSeed;
        float melonSeed = addItem.melonSeed;

        seed1.text = carrotSeed + " Carrot seeds left";
        seed2.text = cornSeed + " Corn seeds left";
        seed3.text = melonSeed + " Melon seeds left";
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
            if (carrotSeed > 0)
            {
                addItem.ChangeCarrotSeed(-1);
                addItem.ChangeCarrot(3);
                Garden();
            }
            else
            {
                StartCoroutine(NoResource());
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && pick == true)
        {
            if (cornSeed > 0)
            {
                addItem.ChangeCornSeed(-1);
                addItem.ChangeCorn(2);
                Garden();
            }
            else
            {
                StartCoroutine(NoResource());
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && pick == true)
        {
            if (melonSeed > 0)
            {
                addItem.ChangeMelonSeed(-1);
                addItem.ChangeMelon(1);
                Garden();
            }
            else
            {
                StartCoroutine(NoResource());
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pick == true)
        {
            pick = false;
            optionUI.SetActive(false);

        }
    }
    void Garden()
    {
        nearInteract = false;
        //Do whatever we do with the associated 
        addStat.AddFlexibility(4);
        addStat.AddCleanliness(-2);
        addItem.ChangeCash(15);
        takeTime.ActivityTimeDeduce(10);
        //Play Animations
        StartCoroutine(ActivityAnimPlaying());
        pick = false;
        optionUI.SetActive(false);
        detail.SetActive(false);
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

    IEnumerator NoResource()
    {
        outOfSeed.SetActive(true);
        yield return new WaitForSeconds(2);
        outOfSeed.SetActive(false);
    }
}
