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

		GameTimer.instance.StartTimer(lastCorrectTime, () => TimeUp());
	}

	void Pause () 
	{
		GameTimer.instance.PauseTimer();	
	}

	void Resume ()
	{
		GameTimer.instance.ResumeTimer();
	}

	void TimeUp () 
	{
		Debug.Log("TimeUp");
	}
		
	public void Right () 
	{
		if (!GameTimer.instance.timerFlag) return;

		if (turtles[0].mixedId % 2 == 0) Correct();
		else Wrong();
	}

	public void Left () 
	{
		if (!GameTimer.instance.timerFlag) return;

		if (turtles[0].mixedId % 2 == 1) Correct();
		else Wrong();
	}

	void Correct ()
	{
		totalCorrect ++;

		if (lastCorrectTime - GameTimer.instance.restTime < 1f)
		{
			combo ++;

			maxCombo = Mathf.Max(maxCombo, combo);
		}

		lastCorrectTime = GameTimer.instance.restTime;

		NextTurtle();

		level = totalCorrect/30 +1;
	}

	void Wrong ()
	{
		totalWrong ++;
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
