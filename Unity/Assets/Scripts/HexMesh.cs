using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{
	Mesh hexMesh;
	MeshCollider meshCollider;
	List<Vector3> vertices, terrainTypes;
	List<int> triangles;
  List<Texture> textures;

  //setup
	void Awake()
  {
		GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
    meshCollider = gameObject.AddComponent<MeshCollider>();
		hexMesh.name = "Hex Mesh";
		vertices = new List<Vector3>();
    terrainTypes = new List<Vector3>();
    textures = new List<Texture>();
		triangles = new List<int>();
	}

  //create the hexes in the mesh one triangle at a time
  public void Triangulate(HexCell[] cells)
  {
    //clear everything to ensure that triangulation is correct even if called while some triangles already exist.
    hexMesh.Clear();
    vertices.Clear();
    terrainTypes.Clear();
    triangles.Clear();
    for (int i = 0; i < cells.Length; i++) {
      Triangulate(cells[i]);
    }
    hexMesh.vertices = vertices.ToArray();
    hexMesh.SetUVs(2, terrainTypes);
    hexMesh.triangles = triangles.ToArray();
    hexMesh.RecalculateNormals();
    meshCollider.sharedMesh = hexMesh;
  }

  void Triangulate(HexCell cell)
  {
    Vector3 center = cell.transform.localPosition;
    for (int i = 0; i < 6; i++)
    {
      /*Each triangle will have a corner in the center, and it's other two corners will be corners of the hex.
        By iterating through the corners, we make an entire hex out of triangles.*/
      AddTriangle(center, center + HexMetrics.corners[i], center + HexMetrics.corners[i + 1]);
      AddTriangleTerrainTypes(cell.terrainType);
		}
  }

  void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
  {
    int vertexIndex = vertices.Count;
    vertices.Add(v1);
    vertices.Add(v2);
    vertices.Add(v3);
    triangles.Add(vertexIndex);
    triangles.Add(vertexIndex + 1);
    triangles.Add(vertexIndex + 2);
  }

  public void AddTriangleTerrainTypes (float type)
  {
    Vector3 types;
    types.x = types.y = types.z = type;
		terrainTypes.Add(types);
		terrainTypes.Add(types);
		terrainTypes.Add(types);
	}
}
