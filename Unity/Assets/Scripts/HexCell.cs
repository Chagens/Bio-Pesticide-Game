using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* HexCell contains all data that needs to be known by the Cells themselves
	 If you need to add data to the Cells, add a private variable and get and set methods as done for neighbors[]
*/
public class HexCell : MonoBehaviour
{
	public HexCoordinates coordinates;

  public int terrainType = 0;
	// public GameObject thingToInstantiate;

  [SerializeField]
  HexCell[] neighbors;

  public HexCell GetNeighbor(HexDirection direction)
  {
    return neighbors[(int)direction];
  }

  public void SetNeighbor (HexDirection direction, HexCell cell)
  {
		neighbors[(int)direction] = cell;
    cell.neighbors[(int)direction.Opposite()] = this;
	}

	/* Instantiates an object at the center of the cell + whatever value you give the new Vector3. Currently it's just 2 units above the cell.
	void InstantiateObject()
	{
		Instantiate(thingToInstantiate, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
	}
	*/
}
