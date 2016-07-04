using UnityEngine;
using System.Collections;

public class NewLevelTurtle : MonoBehaviour 
{
	[SerializeField]
	Transform parentObject;

	public void SetCharacter (int id)
	{
		Character newCharacter = Character.Get(CharacterType.Turtle, id, parentObject);
	}
}
