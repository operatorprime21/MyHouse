using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatKeys : MonoBehaviour
{
    public GameObject controls;
    private bool UIon = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && UIon == false)
        {
            controls.SetActive(true);
            UIon = true;
        }
       else if (Input.GetKeyDown(KeyCode.Escape) && UIon == true)
        {
            controls.SetActive(false);
            UIon = false;
        }
    }
}
