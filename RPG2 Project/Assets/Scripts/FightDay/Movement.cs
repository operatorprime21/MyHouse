using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public List<Transform> wayPoints = new List<Transform>();
    public Transform nextWP;
    public Transform lastWP;
    public float moveSpeed;
    public bool faceRight;
    public int wpRand;
    private bool patrolling;
    public int lowerCap;
    public int upperCap;
    private bool entry;
    public int prevWP;
    Animator movement;
    public Rigidbody2D bossRb;
    public Collider2D floorF1;
    public Collider2D floorF2;
    public Collider2D floorF3;
    public Collider2D stairsF1;
    public Collider2D stairsB1;
    
    private BossAttacks doAttack;

    public bool unlockedProj = false;
    public float attackCD;

    public float distanceX;
    public float distanceY;
    public float distance;
    public float detectRange;

    private bool inCD;
    public string stateAI = "patrolling";
    public Transform player;
    public LayerMask mask;

    public bool stunned;
    private PlayerStat readStat;
    // Start is called before the first frame update
    void Start()
    {   //Scripted sequence where boss enters the house from the front door
        entry = true;
        nextWP = wayPoints[4];
        wpRand = 4;
        movement = this.GetComponent<Animator>();
        faceRight = true;
        doAttack = this.GetComponent<BossAttacks>();
        readStat = GameObject.Find("StatHolder").GetComponent<PlayerStat>();
        detectRange = 17f - 0.3f * readStat.statCleanliness;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - this.transform.position;
        distanceX = this.transform.position.x - player.position.x;
        distanceY = this.transform.position.y - player.position.y;
        distance = Mathf.Sqrt(distanceX * distanceX + distanceY + distanceY);
        if (moveSpeed == 0f)
        {
            stunned = true;
            movement.SetTrigger("idle");
        }
        else
        {
            stunned = false;
        }
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction, Mathf.Infinity, mask);
        if (hit.collider.tag == "Player" && distance <= detectRange)
        {
            Debug.DrawRay(this.transform.position, direction, Color.green);
            stateAI = "attacking";
        }
        else
        {
            Debug.DrawRay(this.transform.position, direction, Color.red);
            stateAI = "patrolling";
        }

        if (entry == true)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, nextWP.position, moveSpeed);
        }
        else if (this.transform.position == nextWP.position && wpRand == 4)
        {
            entry = false;
            //ends the opening sequence
        }
        else if (stateAI == "patrolling")
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, nextWP.position, moveSpeed);
            //All of the patrolling goes here. nextWP is randomized in the codes below
        }

        if ( this.transform.position.x < nextWP.position.x && faceRight == false)
        {
            Flip();
        }
        if (this.transform.position.x > nextWP.position.x && faceRight == true)
        {
            Flip();
        }

        if (stateAI == "attacking")
        {
            nextWP = player;
            if (distance < 5 && distance > -5 && inCD == false && stunned == false) 
            {
                StartCoroutine(Melee());
            }
            else
            {
                movement.SetTrigger("idle");
            }
            if (distance > 5 || distance < -5 && inCD == false && stunned == false & unlockedProj == true)
            {
                StartCoroutine(Shoot());
            }
            else
            {
                movement.SetTrigger("idle");
            }

        }    

    }
    IEnumerator Melee()
    {
        inCD = true;
        movement.SetTrigger("melee");
        moveSpeed = 0f;
        yield return new WaitForSeconds(attackCD);
        inCD = false;
        moveSpeed = 0.01f;
    }
    IEnumerator Shoot()
    {
        inCD = true;
        movement.SetTrigger("range");
        moveSpeed = 0f;
        yield return new WaitForSeconds(attackCD);
        inCD = false;
        moveSpeed = 0.01f;
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "waypoint")
        {
            //Reads if the boss reaches a waypoint, set patrolling to false to stop MoveTowards and begin choosing the next wp
            movement.SetTrigger("idle");
            //Stores the previous wp for other purposes
            prevWP = wpRand;
            if (stateAI == "patrolling")
            {
                StartCoroutine(SearchInPlace());
                Debug.Log("Rolled");
            }
            
        }
    }
    IEnumerator SearchInPlace()
    {
        //Do flips
        //The following strings of codes does a few things.
        //Due to its layered format and complexity, the boss can only pick to move between linear waypoints between the house one by one instead of going straight
        //for a predetermined endpoint or a specific room. The specific design and location of these waypoints are documented in the design document. 
        //First the boss detects which wp it is at.
        //Then set a range of an int respectively to said wp -> Done in SetNextWPCaps 
        if (wpRand == 0)
        {
            SetNextWPCaps(0, 1);
        }
        else if (wpRand == 1)
        {
            SetNextWPCaps(0, 2);
        }
        else if (wpRand == 2)
        {
            SetNextWPCaps(1, 4);
        }
        else if (wpRand == 3)
        {
            SetNextWPCaps(2, 3);
        }
        else if (wpRand == 4)
        {
            SetNextWPCaps(4, 5);
        }
        else if (wpRand == 5 || wpRand == 8)
        {
            SetNextWPCaps(4, 9);
        }
        else if (wpRand == 6)
        {
            SetNextWPCaps(5, 6);
        }
        else if (wpRand == 7)
        {
            SetNextWPCaps(5, 7);
        }
        else if (wpRand == 9)
        {
            SetNextWPCaps(8, 11);
        }
        else if (wpRand == 10)
        {
            SetNextWPCaps(9, 11);
        }
        else if (wpRand == 11)
        {
            SetNextWPCaps(9, 11);
        }
        yield return new WaitForSeconds(3f);
        //wait for a few seconds to search the player in the room. Also ensures things are picked in the correct order.
        //This int will then be picked randomly within assigned range to pick the next WP -> Done in RandomizeLocation.
        RandomizeLocation();
        movement.SetTrigger("walk");
    }
    void RandomizeLocation()
    {
        wpRand = Random.Range(lowerCap, upperCap + 1);
        if (prevWP == wpRand)
        {
            //In case the boss rolls on the same wp as before, reroll
            StartCoroutine(SearchInPlace());
            Debug.Log("Rerolled");
        }
        else if (prevWP == 5 && wpRand == 8)
        {
            StartCoroutine(SearchInPlace());
            Debug.Log("Rerolled");
        }    
        else if (prevWP == 8 && wpRand == 5)
        {
            StartCoroutine(SearchInPlace());
            Debug.Log("Rerolled");
        }    
            //Use int to determine wp from the list
        if (stateAI == "patrolling")
        {
            nextWP = wayPoints[wpRand];
            lastWP = wayPoints[wpRand];
        }    
   //   else if (stateAI == "attacking")
   //   {
   //       nextWP = player;
   //   }   
            
        SetCollision();
    }
    void SetNextWPCaps(int low, int up) 
    {
        lowerCap = low;
        upperCap = up;
    }
    //Collision done for making the boss move reasonably between colliders of the house
    //If possible to implement "Tier 7" mode: Boss ignores all collision and phase through everything including doors and stairs. 
    void SetCollision()
    {
        if (prevWP == 0 && wpRand == 1)
        {
            stairsB1.isTrigger = true;
        }
        else if (prevWP == 1 && wpRand == 0)
        {
            stairsB1.isTrigger = true;
        }
        else if (prevWP == 1 && wpRand == 2)
        {
            stairsB1.isTrigger = false;
            floorF1.isTrigger = true;
            StartCoroutine(WaitToSetCollision(3f));
        }
        else if (prevWP == 2 && wpRand == 1)
        {
            stairsB1.isTrigger = false;
            floorF1.isTrigger = true;
            StartCoroutine(WaitToSetCollision(3f));
        }
        else if (prevWP == 2 && wpRand == 4)
        {
            stairsF1.isTrigger = true;
        }
        else if (prevWP == 4 && wpRand == 2)
        {
            stairsF1.isTrigger = true;
        }
        else if (prevWP == 2 && wpRand == 3)
        {
            floorF1.isTrigger = false;
        }
        else if (prevWP == 3 && wpRand == 2)
        {
            floorF1.isTrigger = false;
        }
        else if (wpRand == 5 && prevWP == 4)
        {
            stairsF1.isTrigger = false;
            floorF2.isTrigger = true;
            Debug.Log("SetCol");
        }
        else if (prevWP == 5 && wpRand == 4)
        {
            stairsF1.isTrigger = false;
            floorF2.isTrigger = true;
            Debug.Log("SetCol");
        }
        else if (wpRand == 5 && prevWP == 6)
        {
            floorF2.isTrigger = false;
            Debug.Log("SetCol");
        }
        else if (prevWP == 5 && wpRand == 6)
        {
            floorF2.isTrigger = false;
            Debug.Log("SetCol");
        }
        else if (wpRand == 5 && prevWP == 9)
        {
            floorF3.isTrigger = true;
            bossRb.gravityScale = 0;
            StartCoroutine(WaitToSetCollision(5f));
            Debug.Log("SetCol");
        }
        else if (prevWP == 5 && wpRand == 9)
        {
            floorF3.isTrigger = true;
            bossRb.gravityScale = 0;
            StartCoroutine(WaitToSetCollision(5f));
            Debug.Log("SetCol");
        }
        else if (wpRand == 8 && prevWP == 4)
        {
            stairsF1.isTrigger = false;
            floorF2.isTrigger = true;
            Debug.Log("SetCol");
        }
        else if (prevWP == 8 && wpRand == 4)
        {
            stairsF1.isTrigger = false;
            floorF2.isTrigger = true;
            Debug.Log("SetCol");
        }
        else if (wpRand == 8 && prevWP == 6)
        {
            floorF2.isTrigger = false;
            Debug.Log("SetCol");
        }
        else if (prevWP == 8 && wpRand == 6)
        {
            floorF2.isTrigger = false;
            Debug.Log("SetCol");
        }
        else if (wpRand == 8 && prevWP == 9)
        {
            floorF3.isTrigger = true;
            bossRb.gravityScale = 0;
            StartCoroutine(WaitToSetCollision(5f));
            Debug.Log("SetCol");
        }
        else if (prevWP == 8 && wpRand == 9)
        {
            floorF3.isTrigger = true;
            bossRb.gravityScale = 0;
            StartCoroutine(WaitToSetCollision(5f));
            Debug.Log("SetCol");
        }
    }
    //Extra piece of code to reenable any floor that the boss phase vertically through. 
    IEnumerator WaitToSetCollision(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        bossRb.gravityScale = 1;
        floorF3.isTrigger = false;
        floorF1.isTrigger = false;
    }

    void Flip()
    {
        faceRight =! faceRight;
        Vector2 bossScale = this.transform.localScale;
        bossScale.x = bossScale.x * -1;
        this.transform.localScale = bossScale;
    }
}
