using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Character : MonoBehaviour 
{
	/// <summary>
	/// Static Methods.
	/// </summary>
	static List<Character> pool = new List<Character>();

	public static Character Get (CharacterType type, int id, Transform parent = null) 
	{
		Character[] characters = pool.Where(character => character.characterType == type && character.id == id).ToArray();

		Character selected ;

		if (characters.Length == 0)
		{
			selected = GetFromResource(type, id);
		}
		else 
		{
			selected = characters[0];
			pool.Remove(selected);
		}

		selected.transform.SetParent(parent);
		selected.transform.localPosition = Vector3.zero;

		return selected;
	}

	static Character GetFromResource (CharacterType type, int id) 
	{
		return Instantiate(Resources.Load(string.Format("Prefabs/{0}_{1}", type, id)) as GameObject).GetComponent<Character>();
	}

	static void SetInPool (Character character) 
	{
		pool.Add(character);
		character.transform.SetParent(CharacterPool.instance.transform);
	}

	/// <summary>
	/// Character Common.
	/// </summary>

	public CharacterType characterType;

	public int id;

	public void Destroy()
	{
		SetInPool(this);	
	}
}

public enum CharacterType 
{
	Turtle = 1,
}
