using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 0.02f;
    public float runSpeed = 0.03f;

    Animator playerAnim;
    public bool faceRight = true;
    public float maxStamina;
    public float currStamina;
    public float sprintCost;
    public bool isRunning;
    public bool regenDelay = false;
    public float regenRate;
    private PlayerStat readStat;
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = this.GetComponent<Animator>();
        readStat = GameObject.Find("StatHolder").GetComponent<PlayerStat>();
        faceRight = true;
        maxStamina = 50f + 2 * readStat.statEndurance;
        currStamina = maxStamina;
        regenRate = 0.01f;
    }
    void Awake()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        Moving();
        if (isRunning == false && regenDelay == false)
        {
            currStamina = currStamina + regenRate;
        }
        if (currStamina >= maxStamina)
        {
            currStamina = maxStamina;
        }
        else if (currStamina <= 0)
        {
            currStamina = 0f;
            StartCoroutine(startDelay());
        }
        if (readStat.statEndurance >= 20)
        {
            regenRate = 0.02f;
        }
    }
    IEnumerator startDelay()
    {
        regenDelay = true;
        yield return new WaitForSeconds(5f);
        regenDelay = false;
    }
    void Moving()
    {
        Vector3 PlayerTransform = this.transform.position;

        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift) == false && Input.GetKey(KeyCode.LeftControl) == false)
        {
            PlayerTransform.x = PlayerTransform.x + walkSpeed;
            isRunning = false;
            this.transform.position = PlayerTransform;
            playerAnim.SetTrigger("Walk");
            if (faceRight == false)
            {
                Flip();
            }
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift) == false && Input.GetKey(KeyCode.LeftControl) == false)
        {
            PlayerTransform.x = PlayerTransform.x - walkSpeed;
            isRunning = false;
            this.transform.position = PlayerTransform;
            playerAnim.SetTrigger("Walk");
            if (faceRight == true)
            {
                Flip();
            }
        }
        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift) == true && Input.GetKey(KeyCode.LeftControl) == false && currStamina > 0f)
        {
            currStamina = currStamina - 0.03f;
            PlayerTransform.x = PlayerTransform.x + runSpeed;
            isRunning = true;
            this.transform.position = PlayerTransform;
            playerAnim.SetTrigger("Walk");
            if (faceRight == false)
            {
                Flip();
            }
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift) == true && Input.GetKey(KeyCode.LeftControl) == false && currStamina > 0f)
        {
            currStamina = currStamina - 0.03f;
            PlayerTransform.x = PlayerTransform.x - runSpeed;
            isRunning = true;
            this.transform.position = PlayerTransform;
            playerAnim.SetTrigger("Walk");
            if (faceRight == true)
            {
                Flip();
            }
        }
        
        else
        {
            playerAnim.SetTrigger("Idle");
            isRunning = false;
        }

    }
    void Flip()
    {
        faceRight = !faceRight;
        Vector2 playerScale = this.transform.localScale;
        playerScale.x = playerScale.x * -1;
        this.transform.localScale = playerScale;
    }

}
