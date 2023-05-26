using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    
    public Transform CamNewPos;
    public Camera Cam;
    public float newCamSize = 5f;
    public float speed = 1f;
    public float zoom;
    public bool camMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = CamNewPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        Cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        if(camMoving==true)
        {
            Cam.transform.position = Vector3.MoveTowards(Cam.transform.position, CamNewPos.position, 0.5f);
        }
        if(Cam.transform.position == CamNewPos.position)
        {
            camMoving = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            camMoving = true;
         // Cam.orthographicSize = newCamSize;
        }
    }
}
