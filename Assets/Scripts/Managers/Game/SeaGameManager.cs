using UnityEngine;
using System.Collections;

public class SeaGameManager : GameManager 
{
	[SerializeField]
	Transform[] turtlesTransform;

	SeaGameCharacter[] turtles;

	int level = 1;

	public override void Initialized ()
	{
		Debug.Log("Initialized");
		GameStart();
	}

	void GameStart () 
	{
		level = 1;

		turtles = new SeaGameCharacter[turtlesTransform.Length];
		for (int i = 0; i < turtlesTransform.Length; i++) 
		{
			turtles[i] = SeaGameCharacter.Create(Random.Range(1, level+2),turtlesTransform[i]);
		}
	}

#if UNITY_EDITOR
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow)){
			Left();
		}else if (Input.GetKeyDown(KeyCode.RightArrow)){
			Right();
		}
	}
#endif
		
	void Right () 
	{
		if (turtles[0].mixedId % 2 == 0) Correct();
		else Wrong();
	}

	void Left () 
	{
		if (turtles[0].mixedId % 2 == 1) Correct();
		else Wrong();
	}

	void Correct ()
	{
		NextTurtle();
	}

	void Wrong ()
	{
		Debug.Log("Wrong");
	}

	void NextTurtle () 
	{
		for (int i = 0; i < turtles.Length -1; i++) 
		{
			turtles[i].SetCharacter(turtles[i+1].character.id);
		}
		turtles[turtles.Length-1].SetCharacter(Random.Range(1, level+2));
	}
}
