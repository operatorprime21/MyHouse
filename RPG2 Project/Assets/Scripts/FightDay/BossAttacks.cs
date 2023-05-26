using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossAttacks : MonoBehaviour
{

    public int pick;
    public GameObject meleeHB;
    public GameObject projectile;
    Animator attack;
    public bool animShoot = false;
    public Transform player;
    public float distance;
    private float forceDir;
    public float mHealth;
    public float cHealth;
    public Rigidbody2D bos;
    // Start is called before the first frame update
    void Start()
    {
        attack = this.GetComponent<Animator>();
        mHealth = 200f;
        cHealth = mHealth;
    }

    // Update is called once per frame
    void Update()
    {
        distance = this.transform.position.x - player.position.x;
        if(distance < 0)
        {
            forceDir = -1f;
        }
        if (distance > 0)
        {
            forceDir = 1f;
        }
        if(cHealth <= 0f)
        {
            SceneManager.LoadScene(5);
        }
    }

    public IEnumerator setActiveHitbox()
    {
        meleeHB.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        meleeHB.SetActive(false);
    }

    public void LaunchProjectile()
    {
        GameObject proj = Instantiate(projectile, new Vector2(this.transform.position.x, this.transform.position.y + 0.9f), this.transform.rotation);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "knifeHB")
        {
            cHealth = cHealth - 5f;
            bos.AddRelativeForce(new Vector2(100 * forceDir, 50));
        }
        if (other.tag == "batHB")
        {
            cHealth = cHealth - 15f;
            bos.AddRelativeForce(new Vector2(200 * forceDir, 50));
        }
        if (other.tag == "spikedHB")
        {
            cHealth = cHealth - 20f;
            bos.AddRelativeForce(new Vector2(200 * forceDir, 50));
        }
        if (other.tag == "gunHB")
        {
            cHealth = cHealth - 35f;
            bos.AddRelativeForce(new Vector2(350 * forceDir, 50));
        }
    }

}
