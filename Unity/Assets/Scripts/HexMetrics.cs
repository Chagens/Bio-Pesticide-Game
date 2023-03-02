using UnityEngine;

/* Info used to generate hexes */
public static class HexMetrics
{
	public const float outerRadius = 10f; //radius of circle around outer points of the hex

	public const float innerRadius = outerRadius * 0.866025404f; //radius of the circle around the inner points of the hex (outer radius * sqrt(3)/2)

  public static Vector3[] corners = //corners of the hex
  {
    new Vector3(0f, 0f, outerRadius),
    new Vector3(innerRadius, 0f, 0.5f * outerRadius),
    new Vector3(innerRadius, 0f, -0.5f * outerRadius),
    new Vector3(0f, 0f, -outerRadius),
    new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
    new Vector3(-innerRadius, 0f, 0.5f * outerRadius),
		new Vector3(0f, 0f, outerRadius)
  };
}
