using UnityEngine;
using System.Collections;

public class SeaGameCharacter : GameCharacter 
{
	public static SeaGameCharacter Create (int id, Transform parent = null) 
	{
		return Create(parent).SetCharacter(id);
	}

	public static SeaGameCharacter Create (Transform parent = null) 
	{
		SeaGameCharacter gameCharacter = new GameObject().AddComponent<SeaGameCharacter>();
		gameCharacter.name = "Turtle";
		gameCharacter.transform.SetParent(parent);
		gameCharacter.transform.localPosition = Vector3.zero;
		return gameCharacter;
	}

	public int mixedId;

	public SeaGameCharacter SetCharacter (int id)
	{
		SetCharacter(CharacterType.Turtle, id);
		mixedId = id;
		return this;
	}
}
