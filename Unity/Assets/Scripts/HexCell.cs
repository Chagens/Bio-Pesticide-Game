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
	public GameObject plant;

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

	/* Instantiates an object at the center of the cell + offset. */
	public void InstantiateObject(GameObject obj, Vector3 offset)
	{
		Instantiate(obj, transform.position + offset, Quaternion.identity, gameObject.transform);
	}
}
