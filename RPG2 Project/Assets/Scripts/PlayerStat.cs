using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStat : MonoBehaviour
{
    public float statKnowledge;
    public float statEndurance;
    public float statCharisma;
    public float statFlexibility;
    public float statCleanliness; 
    public GameObject statScreen;
    public Text stat1;
    public Text stat2;
    public Text stat3;
    public Text stat4;
    public Text stat5;

    // Start is called before the first frame update
    void Start()
    {
        statScreen = GameObject.Find("StatScreen").GetComponent<GameObject>();
        stat1 = GameObject.Find("text1").GetComponent<Text>();
        stat2 = GameObject.Find("text2").GetComponent<Text>();
        stat3 = GameObject.Find("text3").GetComponent<Text>();
        stat4 = GameObject.Find("text4").GetComponent<Text>();
        stat5 = GameObject.Find("text5").GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        stat1.text = "Knowledge: " + statKnowledge;
        stat2.text = "Cleanliness: "+ statCleanliness;
        stat3.text = "Charisma: " + statCharisma;
        stat4.text = "Flexibility: " + statFlexibility;
        stat5.text = "Endurance: " + statEndurance;
       if (Input.GetKey(KeyCode.Tab))
        {
            statScreen.SetActive(true);
        }
       else
        {
            statScreen.SetActive(false);
        }
    }
    public void AddKnowledge(float addKnowledge)
    {
        statKnowledge = statKnowledge + addKnowledge;
    }
    public void AddCleanliness(float addCleanliness)
    {
        statCleanliness = statCleanliness + addCleanliness;
    }
    public void AddEndurance(float addEndurance)
    {
        statEndurance = statEndurance + addEndurance;
    }
    public void AddCharisma(float addCharisma)
    {
        statCharisma = statCharisma + addCharisma;
    }
    public void AddFlexibility(float addFlexibility)
    {
        statFlexibility = statFlexibility + addFlexibility;
    }
}
