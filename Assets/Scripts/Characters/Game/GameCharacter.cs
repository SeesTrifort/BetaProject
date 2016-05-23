using UnityEngine;
using System.Collections;

public class GameCharacter : MonoBehaviour 
{
	public Character character ;

	protected void SetCharacter (CharacterType type, int id)
	{
		if (character != null) character.Destroy();

		character = Character.Get(type, id, transform);
	}
}
