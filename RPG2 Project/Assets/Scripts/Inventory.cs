using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int metal;
    public int leather;
    public int wood;
    public int nail;
    public int rope;
    public int gunpowder;
    public int melonSeed;
    public int cornSeed;
    public int carrotSeed;

    public int ammo;
    public int locker;
    public int trap;
    public int barricade;
    public int corn;
    public int carrot;
    public int melon;
    public int bomb;

    public bool hasShotgun;
    public bool hasKnife;
    public bool hasBat;
    public bool hasSpikedBat;

    public Text metalC;
    public Text leatherC;
    public Text woodC;
    public Text nailC;
    public Text ropeC;
    public Text gunpowderC;
    public Text melonSeedC;
    public Text cornSeedC;
    public Text carrotSeedC;

    public Text ammoC;
    public Text lockerC;
    public Text trapC;
    public Text barricadeC;
    public Text cornC;
    public Text carrotC;
    public Text melonC;
    public Text bombC;

    public Text hShotgun;
    public Text hKnife;
    public Text hBat;
    public Text hSpikedBat;

    public float cash;
    public Text cashC;
    // Start is called before the first frame update
    void Start()
    {
        hasShotgun = false;
        hasKnife = false;
        hasBat = false;
        hasSpikedBat = false;
        cash = 20;
    }

    // Update is called once per frame
    void Update()
    {
        cashC.text = "$ " + cash;
        metalC.text = "x " + metal;
        leatherC.text = "x " + leather;
        woodC.text = "x " + wood;
        nailC.text = "x " + nail;
        ropeC.text = "x " + rope;
        gunpowderC.text = "x " + gunpowder;
        melonSeedC.text = "x " + melonSeed;
        cornSeedC.text = "x " + cornSeed;
        carrotSeedC.text = "x " + carrotSeed;
        ammoC.text = "x " + ammo;
        lockerC.text = "x " + locker;
        trapC.text = "x " + trap;
        barricadeC.text = "x " + barricade;
        cornC.text = "x " + corn;
        carrotC.text = "x " + carrot;
        melonC.text = "x " + melon;
        bombC.text = "x " + bomb;

        if (hasShotgun == true)
        {
            hShotgun.text = "Owned";
        }
        else if (hasShotgun == false)
        {
            hShotgun.text = "Not Owned";
        }

        if (hasKnife == true)
        {
            hKnife.text = "Owned";
        }
        else if (hasKnife == false)
        {
            hKnife.text = "Not Owned";
        }

        if (hasBat == true)
        {
            hBat.text = "Owned";
        }
        else if (hasBat == false)
        {
            hBat.text = "Not Owned";
        }

        if (hasSpikedBat == true)
        {
            hSpikedBat.text = "Owned";
        }
        else if (hasSpikedBat == false)
        {
            hSpikedBat.text = "Not Owned";
        }

    }
    public void ChangeNail(int add)
    {
        nail = nail + add;
    }
    public void ChangeMetal(int add)
    {
        metal = metal + add;
    }
    public void ChangeLeather(int add)
    {
        leather = leather + add;
    }
    public void ChangeWood(int add)
    {
        wood = wood + add;
    }
    public void ChangeRope(int add)
    {
        rope = rope + add;
    }
    public void ChangeGunpowder(int add)
    {
        gunpowder = gunpowder + add;
    }
    public void ChangeMelonSeed(int add)
    {
        melonSeed = melonSeed + add;
    }
    public void ChangeCornSeed(int add)
    {
        cornSeed = cornSeed + add;
    }    
    public void ChangeCarrotSeed(int add)
    {
        carrotSeed = carrotSeed + add;
    }
    public void ChangeAmmo(int add)
    {
        ammo = ammo + add;
    }
    public void ChangeLocker(int add)
    {
        locker = locker + add;
    }
    public void ChangeTrap(int add)
    {
        trap = trap + add;
    }
    public void ChangeBarricade(int add)
    {
        barricade = barricade + add;
    }
    public void ChangeCorn(int add)
    {
        corn = corn + add;
    }
    public void ChangeCarrot(int add)
    {
        carrot = carrot + add;
    }
    public void ChangeMelon(int add)
    {
        melon = melon + add;
    }
    public void ChangeBomb(int add)
    {
        bomb = bomb + add;
    }
    public void ChangeCash(int add)
    {
        cash = cash + add;
    }
    public void GetShotgun()
    {
        hasShotgun = true;
    }
    public void GetBat()
    {
        hasBat = true;
    }
    public void GetKnife()
    {
        hasKnife = true;
    }
    public void GetSpikedBat()
    {
        hasSpikedBat = true;
        hasBat = false;
    }
}
