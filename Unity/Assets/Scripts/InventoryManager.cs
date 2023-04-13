using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public float money = 5000;
    public TMP_Text moneyText;

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
