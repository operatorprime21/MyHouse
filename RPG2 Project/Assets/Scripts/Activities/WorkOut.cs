using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkOut : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.E) && nearInteract == true)
        {
            Debug.Log("Pressed E");
            //Bool to prevent repeated inputs
            nearInteract = false;
            //Do whatever we do with the associated 
            addStat.AddEndurance(3);
            addStat.AddCleanliness(-1);
            takeTime.ActivityTimeDeduce(5);
            addHidden.addTrain(1);
            //Play Animations
            StartCoroutine(ActivityAnimPlaying());
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
        detail.SetActive(false);
        //Play animation here
        //Disable Player controls
        Player.GetComponent<PlayerController>().enabled = false;
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
}