using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public float money = 5000;
    public int rhizobium = 0;
    public int pesticides = 0;
    public int fert = 0;

    public TMP_Text moneyText;
    public TMP_Text rText;
    public TMP_Text pText;
    public TMP_Text fText;

    public bool ownTractor = false;
    public bool brokenTractor = false;
    // Start is called before the first frame update
    void Start()
    {
      moneyText.text = "$" + money;
    }

    // Update is called once per frame
    void Update()
    {
      rText.text = "Rhizobium: " + rhizobium;
      pText.text = "BioPesticides: " + pesticides;
      fText.text = "Fertilizer: " + fert;
    }

    /* Adds to money. Pass in negative amount to subtract */
    public bool changeMoney(float amt)
    {
      if(amt >= 0 || money >= Math.Abs(amt))
      {
        money += amt;
        moneyText.text = "$" + money;
        return true;
      }
      return false;
    }
}
