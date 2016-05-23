using UnityEngine;
using System.Collections;

public class SeaGameManager : GameManager 
{
	[SerializeField]
	Transform[] turtlesTransform;

	SeaGameCharacter[] turtles;

	int level = 1;

	int totalCorrect = 0;

	int totalWrong = 0;

	int combo = 0;

	int maxCombo = 0;

	float lastCorrectTime = 0;

	public override void Initialized ()
	{
		GameStart();

		InputManager.instance.SetGameManager(this);
	}

	void GameStart () 
	{
		level = 1;

		totalCorrect = 0;

		totalWrong = 0;

		combo = 0;

		maxCombo = 0;

		lastCorrectTime = 60f;

		turtles = new SeaGameCharacter[turtlesTransform.Length];
		for (int i = 0; i < turtlesTransform.Length; i++) 
		{
			turtles[i] = SeaGameCharacter.Create(Random.Range(1, level+2),turtlesTransform[i]);
		}

		StartTimer(lastCorrectTime);
	}

	void Pause () 
	{
		PauseTimer();	
	}

	public override void PauseAction ()
	{
		Debug.Log("Pause");
	}

	void Resume ()
	{
		ResumeTimer();
	}

	public override void ResumeAction ()
	{
		Debug.Log("Resume");
	}

	public override void TimeupAction ()
	{
		Debug.Log("Timeup");
	}
		
	public void Right () 
	{
		if (!timerFlag) return;

		if (turtles[0].mixedId % 2 == 0) Correct();
		else Wrong();
	}

	public void Left () 
	{
		if (!timerFlag) return;

		if (turtles[0].mixedId % 2 == 1) Correct();
		else Wrong();
	}

	void Correct ()
	{
		totalCorrect ++;

		if (lastCorrectTime - restTime < 1f)
		{
			combo ++;

			maxCombo = Mathf.Max(maxCombo, combo);
		}

		lastCorrectTime = restTime;

		NextTurtle();

		level = totalCorrect/30 +1;

		Debug.Log("Correct " + combo +" , "+ restTime);

	}

	void Wrong ()
	{
		totalWrong ++;

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
