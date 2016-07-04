using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SeaGameCharacter : GameCharacter 
{
	public static SeaGameCharacter Create (int id, Transform parent = null) 
	{
		return Create(parent).SetCharacter(id);
	}

	public static SeaGameCharacter Create (Transform parent = null) 
	{
		SeaGameCharacter gameCharacter = Instantiate(Resources.Load("Prefabs/SeaGameCharacter") as GameObject).AddComponent<SeaGameCharacter>();
		gameCharacter.name = "Turtle";
		gameCharacter.transform.SetParent(parent);
		RectTransform uiTransform = gameCharacter.transform as RectTransform;
		uiTransform.offsetMax = Vector3.zero;
		uiTransform.offsetMin = Vector3.zero;
		gameCharacter.transform.localPosition = Vector3.zero;
		gameCharacter.transform.localScale = Vector3.one;
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
