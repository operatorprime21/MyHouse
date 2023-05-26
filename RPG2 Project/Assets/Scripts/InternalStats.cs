using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternalStats : MonoBehaviour
{
    public float sleepCount;
    public float trainCount;
    public float walkCount;
    public float eatCount;
    public float houseCleanCount;
    public float craftCount;
    public float buildCount;
    public float electronicsCount;

    public float tierUpFlags;

    private bool day8;

    private Inventory addItem;
    private Basement unlockRecipe;

    // Start is called before the first frame update
    void Start()
    {
        addItem = GameObject.Find("InventoryHolder").GetComponent<Inventory>();
        unlockRecipe = GameObject.Find("BasementWork").GetComponent<Basement>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void addSleep(float add)
    {
        sleepCount = sleepCount + add;
    }
    public void addTrain(float add)
    {
        trainCount = trainCount + add;
    }
    public void addWalk(float add)
    {
        walkCount = walkCount + add;
    }
    public void addEat(float add)
    {
        eatCount = eatCount + add;
    }
    public void addHouseClean(float add)
    {
        houseCleanCount = houseCleanCount + add;
        if (houseCleanCount == 2)
        {
            addItem.GetKnife();
        }
    }
    public void addCraft(float add)
    {
        craftCount = craftCount + add;
        if (craftCount == 3 && addItem.hasBat == true)
        {
            unlockRecipe.UnlockSpikedBat();
        }
    }
    public void addBuild(float add)
    {
        buildCount = buildCount + add;
        if (buildCount == 3)
        {
            unlockRecipe.UnlockShotgun();
        }
    }
    public void addElectronics(float add)
    {
        electronicsCount = electronicsCount + add;
    }

    public void CheckStat()
    {
        if (sleepCount <= 3 && sleepCount >= 8)
        {
            tierUpFlags++;
        }
        else if (trainCount <= 3)
        {
            tierUpFlags++;
        }
        else if (walkCount <= 3)
        {
            tierUpFlags++;
        }
        if (eatCount >= 8)
        {
            tierUpFlags++;
        }
        if (electronicsCount >= 8)
        {
            tierUpFlags++;
        }
        
    }
}
