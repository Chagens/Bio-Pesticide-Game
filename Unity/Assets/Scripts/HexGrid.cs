using UnityEngine;
using TMPro;

public class HexGrid : MonoBehaviour
{
	public int width = 6;
	public int height = 6;

  public TextMeshProUGUI cellLabelPrefab;
  public Canvas gridCanvas;
  HexMesh hexMesh;

  public HexCell cellPrefab;
  HexCell[] cells;

  //get references and create the grid
  void Awake ()
  {
    gridCanvas = GetComponentInChildren<Canvas>();
    hexMesh = GetComponentInChildren<HexMesh>();
    cells = new HexCell[height * width];

    for (int z = 0, i = 0; z < height; z++)
    {
      for (int x = 0; x < width; x++)
      {
        CreateCell(x, z, i++);
      }
    }
  }

  //after the grid is made, create the mesh
  void Start ()
  {
		hexMesh.Triangulate(cells);
	}

  //place cells in the grid
  void CreateCell (int x, int z, int i)
  {
    Vector3 position;
		position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f); //offset every other row
		position.y = 0f;
		position.z = z * (HexMetrics.outerRadius * 1.5f);

    HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
    cell.transform.SetParent(transform, false);
    cell.transform.localPosition = position;
		cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);

    /* Set Cell Neighbors
       IF X > 0; We know we're not on the first cell of the row, so the West side of the cell has a neighbor to set
       IF Z > 0; We know we're not on the first cell of the column, so the SE and SW side of the cell has a neighbor to set
          z * 1 tells us whether we're on an even row or not tells us whether we have a SW or SE cell on the edges of the row
		*/
    if (x > 0)
    {
			cell.SetNeighbor(HexDirection.W, cells[i - 1]);
		}
    if (z > 0)
    {
			if ((z & 1) == 0)
      {
				cell.SetNeighbor(HexDirection.SE, cells[i - width]);
        if (x > 0)
        {
					cell.SetNeighbor(HexDirection.SW, cells[i - width - 1]);
				}
			}
      else
      {
				cell.SetNeighbor(HexDirection.SW, cells[i - width]);
				if (x < width - 1)
        {
					cell.SetNeighbor(HexDirection.SE, cells[i - width + 1]);
				}
			}
		}

		TextMeshProUGUI label = Instantiate<TextMeshProUGUI>(cellLabelPrefab);
		label.rectTransform.SetParent(gridCanvas.transform, false);
		label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
		label.text = cell.coordinates.ToStringOnSeparateLines();
  }

	/* Model for methods that interact with the grid, handle input in new script
	   Send it here with the position from a raycast and whatever other data you need
		 Convert the screen position to HexCoordinates
		 Do whatever you need to do
		 If you change the texture, make sure to call hexMesh.Triangulate(cells)
	*/
  public void ColorCell (Vector3 position, int texture)
  {
    position = transform.InverseTransformPoint(position);
    HexCoordinates coordinates = HexCoordinates.FromPosition(position);
    int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
		HexCell cell = cells[index];
		cell.terrainType = texture;
		cell.InstantiateObject(cell.plant, new Vector3(0, 30, 0));
		hexMesh.Triangulate(cells);
    Debug.Log("Touched at " + coordinates.ToString());
  }

	public HexCell GetRandomCell()
	{
		int index = Random.Range(0, cells.Length);
		return cells[index];
	}

	public void resetCellColors()
	{
		foreach(HexCell cell in cells)
		{
			cell.terrainType = 0;
		}
		hexMesh.Triangulate(cells);
	}
}
