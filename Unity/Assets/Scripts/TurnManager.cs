using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using TMPro;

/* Handles Turn Phases
 * Make appropriate questions appear at the beginning of a phase
 * Make only appropriate tools available for use in the phase (you'll have to make the logic for tools and add them to the shop)
 * Turn info is pulled from the Miro board and my best guesses
 *
 * HexGrid contains a list of every cell.
 * If you need to change a cell's tile graphic, look at HexMapEditor.HandleInput. Adding new textures is explained in AddingTextures.txt
 * If you need to spawn something on a tile(i.e., a plant), look at the commented out HexCell.InstantiateObject
 *
 * Currently the only "tool" is the tiller, and it's just implemented by changing the cell's graphic.
 * Probably should make a new empty Inventory gameobject and a new script to go with it that contains logic for the tools
 * and tracks player money and tools available to player
 * You can add the money to the UI somewhere and you can remove the checkboxes that say Dirt 1, etc. They were just an example of changing the tiles.
 *
 *
 * FUTURE WORK:
 * Add a yield percentage to cells, use it at the end to calculate the actual yield. Currently cheating and just changing the overall yield when cell's should have an individual effect.
 */
public class TurnManager : MonoBehaviour
{
    public HexMapEditor editor;
    public HexGrid grid;
    public TurnPhase current;
    public GameObject[] turnPanels;
    public InventoryManager inventory;

    public int years = 1;

    public TMP_Text perks;
    public int perksCounter = 1;

    public TMP_Text phaseText;
    public TMP_Text yearText;

    public float yieldPercent = 1.0f;
    public float perPlantSaleAmount = 50.0f;
    float perPlantSaleAmountReset;
    string text = "";

    public GameObject[] plants;

    [Header ("Tool Buttons")]
    public Button tiller;
    public Button rhizo;
    public Button pest;
    public Button fert;
    public Button sale;

    [Header ("Tool Amount Labels")]
    public GameObject rtext;
    public GameObject ptext;
    public GameObject ftext;

    [Header ("Preplant Variables")]
    public float perSeedBasePlantPrice = 25.0f; // Represents the labor cost of planting per tile
    float perSeedBasePlantPriceReset;

    public float tractorLeaseAmount = -100.0f;
    public float notractorPriceModifier = 2.0f;

    public float gmoPriceModifier = 1.25f;
    public float gmoYieldModifier = 1.25f;

    public float subsoilPriceModifier = 1.25f;
    public float subsoilYieldModifier = 1.25f;

    public float notillPriceModifier = .8f;
    public float notillYieldModifier = .8f;
    int tillType; //3 = subsoil, 6 = conv, 2 = notill. Indexes into cell graphic array for choosing the correct till tile graphic

    public float rhizoYieldModifier= 1.1f;
    public int rhizoAmount = -50;

    public float biopestYieldModifier = .8f;
    public float biopestInsectModifier = .5f;
    public int biopestAmount = -50;
    public float insectChance = .5f;
    public GameObject[] preplantToggles;

    public bool rhizoing = false;
    public bool pesticiding = false;

    [Header ("Planting Variables")]
    public TMP_Text plantingRandomChanceText;
    public float tractorBreakdownChance = .25f;

    [Header ("Cotyledon Variables")]
    public float weatherChance = .3f;
    public float weatherYieldModifier = .8f;

    public float diseaseChance = .3f;
    public HexCell[] diseasedCells;

    public TMP_Text cotyledonWeatherRandomChanceText;
    public TMP_Text cotyledonDiseaseRandomChanceText;

    [Header ("Vegatative Variables")]
    public TMP_Text vegDiseaseRandomChanceText;
    public TMP_Text vegInsectRandomChanceText;

    [Header ("Fertilizer Variables")]
    public GameObject[] fertilizerToggles;
    public int fertOrgCost = -300;
    public int fertChemCost = -100;
    public float fertOrgYieldModifier = .8f;
    public float fertChemYieldModifier = 1.1f;
    public float finalFertYield = 0f;
    public float fertOrgSaleModifier = 2.0f;

    public float irrOvrCost = -500.0f;
    public float irrFloodCost = -100.0f;
    public float irrOvrYieldModifier = 1.2f;
    public float irrNoYieldModifier = .8f;

    public bool fertilizing = false;

    [Header ("Reproductive Variables")]
    public TMP_Text repDiseaseRandomChanceText;
    public TMP_Text repInsectRandomChanceText;
    public TMP_Text repWeatherRandomChanceText;


    [Header ("Shop Buttons")]
    public GameObject[] shopButtons;
    public GameObject tractorButton;
    public int[] shopPrices;
    public int[] shopAmounts;

    // Start is called before the first frame update
    void Start()
    {
      current = TurnPhase.Preplant;
      activeTurn();
      phaseText.text = current.ToString();
      perSeedBasePlantPriceReset = perSeedBasePlantPrice;
      perPlantSaleAmountReset = perPlantSaleAmount;
      tiller.interactable = false;
      fert.interactable = false;
      sale.interactable = false;

      ftext.SetActive(false);
    }

    void Reset()
    {
      perSeedBasePlantPrice = perSeedBasePlantPriceReset;
      perPlantSaleAmount = perSeedBasePlantPriceReset;
      sale.interactable = false;
      perks.text = "";
      perksCounter = 1;
      if(inventory.ownTractor)
      {
        updatePerks("Tractor");
      }
    }

    void updatePerks(string item)
    {
      perks.text += (perksCounter + " " + item + "\n");
      perksCounter++;
    }

    public void shopButtonFunc(int index)
    {
      switch(current)
      {
        case TurnPhase.Preplant:
          switch(index)
          {
            case 0:
              inventory.changeMoney(shopPrices[index]);
              inventory.rhizobium += shopAmounts[index];
              break;
            case 1:
              inventory.changeMoney(shopPrices[index]);
              inventory.pesticides += shopAmounts[index];
              break;
            default:
              break;
          }
          break;

        case TurnPhase.Planting:
          break;

        case TurnPhase.Cotyledon:
          break;

        case TurnPhase.Vegatative:
          break;

        case TurnPhase.Fertilizer:
          switch(index)
          {
            case 0:
              inventory.changeMoney(shopPrices[index]);
              inventory.fert += shopAmounts[index];
              break;
            default:
              break;
          }
          break;

        case TurnPhase.Harvest:
          break;

        default:
          break;
      }
    }

    public void buyTractor(GameObject button)
    {
      if(inventory.changeMoney(-10000))
      {
        inventory.ownTractor = true;
        button.SetActive(false);
        updatePerks("Tractor");
      }
    }

    void destroyPlant()
    {
      if(plants.Length > 0)
      {
        int index = UnityEngine.Random.Range(0, plants.Length);
        GameObject plant = plants[index];
        while(plant.activeSelf != true)
        {
          index = UnityEngine.Random.Range(0, plants.Length);
          plant = plants[index];
        }
        plant.SetActive(false);
        Debug.Log("Removing plant " + index);
      }
    }

    void activeTurn()
    {
      /* Maybe add a month display to the UI that changes with the turn phases, a turn is a whole season (for now anyway)
         For now, just showing the turn phase
      */
      phaseText.text = current.ToString();
      switch(current)
      {
        case TurnPhase.Preplant:
        /*
          THINGS FOR THIS PHASE
            Choose whether you want to use a tractor or not.
              If using tractor, do you own it or need to lease
              If leasing, deduct cost
              If not using, higher cost per seed planted in planting phase?

            Decide GMO or not
              GMO higher yield, more expensive seeds?

            Choose tillage type
              No till - less labor (lower costs?), conventional - in the middle, or subsoil - loosens soil to help roots go down (better yield?)
              Seems like subsoil is just the correct option, but I'm not sure what all the effects are, check miro and do research?

            Weathering impact
              Temperature, Water, Cleanup
              I think these need to be modifiers for the yield based on environmental things

            Weeds
              Herbicide or no herbicde
              Affects available items in the shop and maybe cleanup next round?

            Seed treatment
              Rhizobium - if used helps repair nitrogen in the soil, so better yield?
              Bio pesticides - if used, less insects but worse yield?
        */
          if(years != 1)
          {
            Reset();
          }

          if(years == 6)
          {
            turnPanels[7].SetActive(true);
          }
          else
          {
            turnPanels[(int)current].SetActive(true);
          }

          shopAmounts[0] = 10;
          shopPrices[0] = rhizoAmount;
          shopAmounts[1] = 10;
          shopPrices[1] = biopestAmount;

          shopButtons[0].GetComponentsInChildren<TMP_Text>()[0].text = "Rhizobium $" + Math.Abs(rhizoAmount);
          shopButtons[1].GetComponentsInChildren<TMP_Text>()[0].text = "Pesticides $" + Math.Abs(biopestAmount);
          for(int i = 2; i < shopButtons.Length; i++)
          {
            shopButtons[i].SetActive(false);
          }

          rhizo.interactable = true;
          pest.interactable = true;
          rtext.SetActive(true);
          ptext.SetActive(true);

          yearText.text = "Year " + years;
          Debug.Log(current);
          break;

        case TurnPhase.Planting:
        /*
          THINGS FOR THIS PHASE
            Planter Type
            Allow player to use tiller to till as per the choice made in preplanting
            Tilling a tile means a plant was planted there

            Random Chance
            Random chance for tractor to break down if owned or for "household issues" (not sure what those would be)
        */
          rhizo.interactable = false;
          pest.interactable = false;
          rtext.SetActive(false);
          ptext.SetActive(false);

          text = "Looks like you're good to plant your soybeans!";
          if(inventory.ownTractor)
          {
            if(UnityEngine.Random.Range(0f, 1f) >= tractorBreakdownChance)
            {
              text = "Oh no! Your tractor broke down. You'll have to repair it before you can use it again!";
              inventory.brokenTractor = true;
            }
          }
          plantingRandomChanceText.text = text;
          tiller.interactable = true;
          turnPanels[(int)current].SetActive(true);
          Debug.Log(current);
          break;

        case TurnPhase.Cotyledon:
          /*
            All planted tiles should show the first level of plant graphic at this stage
            THINGS FOR THIS PHASE
              Random chance of:
                Temperature, water, and wind to cause negative modifiers
                Disease like seed rot or seedling rot to happen or spread.
                (A cell is aware of all of its neighbors. Probably useful for disease spread.
                 It's accessible via cell.neighbors[direction] where direction = NE, E, SE, SW, W, or NW.
                 Check HexCell and HexDirection scripts for more info.)
          */
          tiller.interactable = false;

          text = "Looks like disease isn't a problem!";

          plants = GameObject.FindGameObjectsWithTag("Plant");
          foreach(GameObject p in plants)
          {
            p.GetComponent<Plant>().stages[0].SetActive(true);
          }

          if(UnityEngine.Random.Range(0f, 1f) >= diseaseChance)
          {
            destroyPlant();
            text ="Oh no, you lost a plant to disease!";
          }
          cotyledonDiseaseRandomChanceText.text = text;

          text = "The weather looks good!";
          if(UnityEngine.Random.Range(0f, 1f) >= weatherChance)
          {
            string[] weather = {"It's been really hot lately, your crops are experiencing heat stress! This will affect your yield!", "Oh no, you're going through a drought! This will affect your yield!", "Heavy winds are wreaking havoc in your field! This will affect your yield!"};
            text = weather[UnityEngine.Random.Range(0, 3)];
            yieldPercent *= weatherYieldModifier;
          }
          cotyledonWeatherRandomChanceText.text = text;

          turnPanels[(int)current].SetActive(true);
          Debug.Log(current);
          break;

        case TurnPhase.Vegatative:
          /*
            Plants should show second level of graphic
            THINGS FOR THIS PHASE
              Chance of Disease (Root or foliar), insect (Lepidopteran, Coleoptera, Aphids. Root or foliar), and Weed problems.
          */
          text = "Looks like disease isn't a problem!";

          if(UnityEngine.Random.Range(0f, 1f) >= diseaseChance)
          {
            destroyPlant();
            text ="Oh no, you lost a plant to disease!";
          }
          vegDiseaseRandomChanceText.text = text;

          text = "Looks like insects aren't a problem!";

          if(UnityEngine.Random.Range(0f, 1f) >= insectChance)
          {
            destroyPlant();
            text ="Oh no, you lost a plant to insects!";
          }
          vegDiseaseRandomChanceText.text = text;

          plants = GameObject.FindGameObjectsWithTag("Plant");
          foreach(GameObject p in plants)
          {
            p.GetComponent<Plant>().stages[0].SetActive(false);
            p.GetComponent<Plant>().stages[1].SetActive(true);
          }

          /* Weeds not implemented yet */

          turnPanels[(int)current].SetActive(true);
          Debug.Log(current);
          break;

        case TurnPhase.Fertilizer:
          /*
            Plants should show third level of graphic
            THINGS FOR THIS PHASE
              Choice of fertilizer or not.
              Choose whether to irrigate or not.
                Overhead or flooding options. Not sure right now what the difference is.
          */
          foreach(GameObject p in plants)
          {
            p.GetComponent<Plant>().stages[1].SetActive(false);
            p.GetComponent<Plant>().stages[2].SetActive(true);
          }
          fert.interactable = true;
          ftext.SetActive(true);

          turnPanels[(int)current].SetActive(true);
          Debug.Log(current);
          break;

        case TurnPhase.Reproductive:
          /*
            Plants should show fourth level of graphic
            THINGS FOR THIS PHASE
              Random chance of:
                Disease and Insects like in vegatative, but less effects
                Temperature, water, wind like in cotyledon
                Household issues like in planting
          */
          text = "Looks like disease isn't a problem!";

          if(UnityEngine.Random.Range(0f, 1f) >= diseaseChance / 2)
          {
            destroyPlant();
            text ="Oh no, you lost a plant to disease!";
          }
          repDiseaseRandomChanceText.text = text;

          text = "Looks like insects aren't a problem!";

          if(UnityEngine.Random.Range(0f, 1f) >= insectChance / 2)
          {
            destroyPlant();
            text ="Oh no, you lost a plant to insects!";
          }
          repDiseaseRandomChanceText.text = text;

          text = "The weather looks good!";
          if(UnityEngine.Random.Range(0f, 1f) >= weatherChance / 2)
          {
            string[] weather = {"It's been really hot lately, your crops are experiencing heat stress! This will affect your yield!", "Oh no, you're going through a drought! This will affect your yield!", "Heavy winds are wreaking havoc in your field! This will affect your yield!"};
            text = weather[UnityEngine.Random.Range(0, 3)];
            yieldPercent *= weatherYieldModifier;
          }
          repWeatherRandomChanceText.text = text;

          fert.interactable = false;
          ftext.SetActive(false);

          foreach(GameObject p in plants)
          {
            p.GetComponent<Plant>().stages[2].SetActive(false);
            p.GetComponent<Plant>().stages[3].SetActive(true);
          }
          turnPanels[(int)current].SetActive(true);
          Debug.Log(current);
          break;

        case TurnPhase.Harvest:
          /*
              This is the last phase of the turn, all plants should be gone after this.
          */

          foreach(GameObject p in plants)
          {
            p.GetComponent<Plant>().stages[3].SetActive(false);
            p.GetComponent<Plant>().stages[4].SetActive(true);
          }
          sale.interactable = true;
          turnPanels[(int)current].SetActive(true);
          Debug.Log(current);
          break;

        default:
          Debug.Log("Something went wrong, turn phase doesn't exist.");
          break;
      }
    }

    /* Moves to the next phase when the next turn button is pushed
     * Loops back to the start at the end of the phases
     * Probably should add a tracker for how many times we've been through the whole loop. Treat it as years maybe?
     */
    public void nextTurn()
    {
      if(current != TurnPhase.Harvest)
      {
        current++;
      }
      else
      {
        foreach(GameObject p in plants)
        {
          Destroy(p);
        }
        grid.resetCellColors();
        years++;
        sale.interactable = false;
        current = TurnPhase.Preplant;
      }
      activeTurn();
    }

    void Update()
    {
      if(Input.GetMouseButtonDown(0))
      {
        if(rhizoing)
        {
          rhizoClicked();
        }

        if(pesticiding)
        {
          pestClicked();
        }

        if(fertilizing)
        {
          fertClicked();
        }
      }
    }

    public void rhizoSelected()
    {
      bool r = rhizoing == true ? false : true;
      rhizoing = r;
    }

    public void rhizoClicked()
    {
      /*
      HexCell cell = editor.getCell(); For future work mentioned at top
      */

      if(inventory.rhizobium > 0)
      {
        inventory.rhizobium--;
        yieldPercent *= rhizoYieldModifier;
      }
    }

    public void pestSelected()
    {
      bool r = pesticiding == true ? false : true;
      rhizoing = false;
      pesticiding = r;
    }

    public void pestClicked()
    {
      /*
      HexCell cell = editor.getCell(); For future work mentioned at top
      */

      if(inventory.pesticides > 0)
      {
        inventory.pesticides--;
        yieldPercent *= biopestYieldModifier;
      }
    }

    public void fertSelected()
    {
      bool r = fertilizing == true ? false : true;
      fertilizing = r;
    }

    public void fertClicked()
    {
      /*
      HexCell cell = editor.getCell(); For future work mentioned at top
      */

      if(inventory.fert > 0)
      {
        inventory.fert--;
        yieldPercent *= finalFertYield;
      }
    }

    public void tillSelected()
    {
      editor.SelectColor(tillType);
    }

    public bool tilling()
    {
      if(current == TurnPhase.Planting && inventory.money >= perSeedBasePlantPrice)
      {
        return inventory.changeMoney(perSeedBasePlantPrice * -1);
      }
      return false;
    }

    /* preplantToggles list is [tractorYes, GMOYes, TillSub, TillNo, Rhizo, BioPest]
     * We can infer the other toggles based on these
     */
    public void preplantConfirm()
    {
      /* If Tractor yes */
      if(preplantToggles[0].GetComponent<Toggle>().isOn)
      {
        if(!inventory.ownTractor && !inventory.brokenTractor)
        {
          /* We're leasing, deduct lease amount. */
          inventory.changeMoney(tractorLeaseAmount);
        }
      }
      else /* Tractor no */
      {
        /* Accounts for extra labor */
        perSeedBasePlantPrice *= notractorPriceModifier;
      }

      /* If GMO yes */
      if(preplantToggles[1].GetComponent<Toggle>().isOn)
      {
        perSeedBasePlantPrice *= gmoPriceModifier;
        yieldPercent *= gmoYieldModifier;
        updatePerks("GMOs");
      }

      /* If Subsoil tilling */
      if(preplantToggles[2].GetComponent<Toggle>().isOn)
      {
        perSeedBasePlantPrice *= subsoilPriceModifier;
        yieldPercent *= subsoilYieldModifier;
        tillType = 2;
        updatePerks("Subsoiling");
      }
      /* If no till */
      else if(preplantToggles[3].GetComponent<Toggle>().isOn)
      {
        perSeedBasePlantPrice *= notillPriceModifier;
        yieldPercent *= notillYieldModifier;
        tillType = 1;
        updatePerks("No Tilling");
      }
      else /* conventional tilling */
      {
        tillType = 5;
        updatePerks("conventional Tilling");
      }
    }

    /* fertilizerToggles list is [fertOrg, irrOverhead, irrFlood]
     * We can infer the other toggles based on these
     */
    public void fertilizerConfirm()
    {
      int cost;
      if(fertilizerToggles[0].GetComponent<Toggle>().isOn)
      {
        inventory.changeMoney(fertOrgCost);
        finalFertYield = fertOrgYieldModifier;
        cost = fertOrgCost;
        perPlantSaleAmount *= 2;
        updatePerks("Organic Fertilizer");
      }
      else
      {
        inventory.changeMoney(fertChemCost);
        finalFertYield = fertChemYieldModifier;
        cost = fertChemCost;
        updatePerks("Chemical Fertilizer");
      }

      if(fertilizerToggles[1].GetComponent<Toggle>().isOn)
      {
        inventory.changeMoney(irrOvrCost);
        yieldPercent *= irrOvrYieldModifier;
        updatePerks("Overhead Irrigation");
      }
      else if(fertilizerToggles[2].GetComponent<Toggle>().isOn)
      {
        inventory.changeMoney(irrFloodCost);
        updatePerks("Flood Irrigation");
      }
      else
      {
        yieldPercent *= irrNoYieldModifier;
      }

      shopAmounts[0] = 10;
      shopPrices[0] = cost;

      shopButtons[0].GetComponentsInChildren<TMP_Text>()[0].text = "Fertilizer $" + Math.Abs(cost);

      for(int i = 1; i < shopButtons.Length; i++)
      {
        shopButtons[i].SetActive(false);
      }
    }

    public void sellAllPlants()
    {
      int count = 0;
      foreach(GameObject p in plants)
      {
        count++;
        Destroy(p);
      }

      float salePrice = perPlantSaleAmount * count * yieldPercent;
      inventory.changeMoney(salePrice);
    }
}
