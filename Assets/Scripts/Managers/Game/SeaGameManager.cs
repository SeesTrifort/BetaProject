using UnityEngine;
using System.Collections;

public class SeaGameManager : GameManager 
{
	[SerializeField]
	Transform[] turtlesTransform;

	SeaGameCharacter[] turtles;

	public override void Initialized ()
	{
		Debug.Log("Initialized");

		turtles = new SeaGameCharacter[turtlesTransform.Length];
		for (int i = 0; i < turtlesTransform.Length; i++) 
		{
			turtles[i] = SeaGameCharacter.Create(Random.Range(1,3),turtlesTransform[i]);
		}
	}
}
