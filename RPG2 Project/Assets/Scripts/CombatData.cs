using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatData : MonoBehaviour
{
    public float maxHealth;
    public float maxStamina;
    public float currentHealth;
    public float currentStamina;
    public float staminaRegen;
    public float sprintDrain;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 50f;
        maxStamina = 40f;
        sprintDrain = 5f;
        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeMaxHealth(float add)
    {
        maxHealth = maxHealth + add;
    }
    public void ChangeMaxStamina(float add)
    {
        maxStamina = maxStamina + add;
    }
   
}
