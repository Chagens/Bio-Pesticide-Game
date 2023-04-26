using UnityEngine;
using UnityEngine.EventSystems;

/* Currently allows us to interact with the map, will be replaced by actual gameplay */
public class HexMapEditor : MonoBehaviour
{

    public int[] textures;

    public HexGrid hexGrid;
    public TurnManager turnManager;

    private int activeTexture;

    void Awake()
    {
        SelectColor(0);
    }

    Vector3 lastHitPoint; // Keep track of the last hex that was colored
    bool isDragging = false; // Keep track of whether the mouse is currently being dragged

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = false;
            }
            else if (Input.GetMouseButton(0))
            {
                if (!isDragging)
                {
                    // If the mouse has just started dragging, color the initial hex
                    isDragging = true;
                    RaycastHit hit;
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                    {
                        lastHitPoint = hit.point;
                        if(hexGrid.CheckColorCell(lastHitPoint, activeTexture)){
                        	HandleInput();
						}
                    }
                }
                else
                {
                    // If the mouse is still being dragged, color any new hexes that it hits
                    RaycastHit hit;
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                    {
                        if (hit.point != lastHitPoint)
                        {
                            if (hexGrid.CheckColorCell(hit.point, activeTexture))
                            {
                                lastHitPoint = hit.point;
                                HandleInput();
                            }
                        }
                    }
                }
            }
            else
            {
                isDragging = false;
            }
        }
    }



    /* Model for how to handle clicking on the hexes
		 Raycast to get click coordinates
		 Add a method to HexGrid to do whatever the click needs to do
		 e.g. ColorCell */
    void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            if (turnManager.tilling())
            {
                hexGrid.ColorCell(hit.point, activeTexture);
            }
        }
    }

    public void SelectColor(int index)
    {
        activeTexture = textures[index];
    }
}
