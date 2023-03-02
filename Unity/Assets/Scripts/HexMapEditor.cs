using UnityEngine;
using UnityEngine.EventSystems;

/* Currently allows us to interact with the map, will be replaced by actual gameplay */
public class HexMapEditor : MonoBehaviour
{

	public int[] textures;

	public HexGrid hexGrid;

	private int activeTexture;

	void Awake ()
  {
		SelectColor(0);
	}

	void Update () {
		if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
    {
			HandleInput();
		}
	}

	/* Model for how to handle clicking on the hexes
		 Raycast to get click coordinates
		 Add a method to HexGrid to do whatever the click needs to do
		 e.g. ColorCell */
	void HandleInput ()
  {
		Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(inputRay, out hit))
    {
			hexGrid.ColorCell(hit.point, activeTexture);
		}
	}

	public void SelectColor (int index)
  {
		activeTexture = textures[index];
	}
}
