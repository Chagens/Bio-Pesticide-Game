using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Contains info needed to interact with cell neighbors */
public enum HexDirection
{
	NE, E, SE, SW, W, NW
}

public static class HexDirectionExtensions
{
	public static HexDirection Opposite (this HexDirection direction)
  {
		return (int)direction < 3 ? (direction + 3) : (direction - 3);
	}
}
