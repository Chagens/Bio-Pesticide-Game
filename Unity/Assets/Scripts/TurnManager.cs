using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
 */

/* Make sure these are in the correct order. */
public enum TurnPhase
{
  Preplant, Planting, Cotyledon, Vegatative, Fertilizer, Reproductive, Harvest
};

public class TurnManager : MonoBehaviour
{
    public TurnPhase current;

    // Start is called before the first frame update
    void Start()
    {
      current = TurnPhase.Preplant;
    }

    void activeTurn()
    {
      /* Maybe add a month display to the UI that changes with the turn phases, a turn is a whole season (for now anyway) */
      switch(current)
      {
        case TurnPhase.Preplant:
          /*
            THINGS FOR THIS PHASE
              Choose whether you want to use a tractor or not.
                If using tractor, do you own it or need to lease
                If leasing, deduct cost

              Decide GMO or not
                Changes what items are available in the shop

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
          Debug.Log(current);
          break;
        case TurnPhase.Planting:
          /*
            THINGS FOR THIS PHASE
              Timing
              I have no idea what this is

              Planter Type
              Allow player to use tiller to till as per the choice made in preplanting
              (I think tilling a tile means a plant was planted there)

              Person Hours
              Not sure here either, higher yield for more man hours (cost)?

              Random Chance
              Random chance for tractor to break down if owned or for "household issues" (not sure what those would be)
          */
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
          Debug.Log(current);
          break;
        case TurnPhase.Vegatative:
          /*
            Plants should show second level of graphic
            THINGS FOR THIS PHASE
              Chance of Disease (Root or foliar), insect (Lepidopteran, Coleoptera, Aphids. Root or foliar), and Weed problems.
          */
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
          Debug.Log(current);
          break;
        case TurnPhase.Harvest:
          /*
            Plants should show fifth level of graphic
            THINGS FOR THIS PHASE
              Timing (still dunno)
              Personnel
                More yield for more cost on workers (?)
              Chance of rain
              Equipment
                Buy from store permanently or lease temporarily

              This is the last phase of the turn, all plants should be gone after this.
          */
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
        current = TurnPhase.Preplant;
      }
      activeTurn();
    }
}
