﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniRx;

public class SeaGameManager : GameManager 
{
	[SerializeField]
	StateManager uiState;

	enum SeaGameState 
	{
		Init,
		Start, 
		Game,
		Pause,
		Result
	}

	SeaGameState gameState 
	{
		set{
			uiState.ChangeState(value.ToString());
		}
	}

	[SerializeField]
	SeaGameUI ui;


	/// <summary>
	/// Game
	/// </summary>
	[SerializeField]
	Transform[] turtlesTransform;

	SeaGameCharacter[] turtles;

	int level = 1;

	int totalCorrect = 0;

	int totalWrong = 0;

	int combo = 0;

	int maxCombo = 0;

	float lastCorrectTime = 0;

	ReactiveProperty<int> score = new ReactiveProperty<int>(0);

	ReactiveProperty<int> bestScore = new ReactiveProperty<int>(0);

	void Awake () 
	{
		gameState = SeaGameState.Init;

		score.AddTo(this);
		score.Subscribe(s => ui.SetScore(s)).AddTo(this);

		bestScore.AddTo(this);
		bestScore.Subscribe(bs => ui.SetBestScore(bs)).AddTo(this);
	}

	public override void Initialized ()
	{
		gameState = SeaGameState.Start;

		InputManager.instance.SetGameManager(this);
	}

	/// <summary>
	/// スタートボタン押した時
	/// </summary>
	public void GameStart () 
	{
		gameState = SeaGameState.Game;

		level = 1;

		totalCorrect = 0;

		totalWrong = 0;

		combo = 0;

		maxCombo = 0;

		lastCorrectTime = 60f;

		score.Value = 0;

		bestScore.Value = ScoreManager.GetBestScore(GameType.SeaGame);

		turtles = new SeaGameCharacter[turtlesTransform.Length];
		for (int i = 0; i < turtlesTransform.Length; i++) 
		{
			turtles[i] = SeaGameCharacter.Create(Random.Range(1, level+2),turtlesTransform[i]);
		}

		GameTimer.instance.StartTimer(60f, () => TimeUp());
	}

	/// <summary>
	/// ポーズボタン押した時.
	/// </summary>
	public void Pause () 
	{
		GameTimer.instance.PauseTimer();	

		gameState = SeaGameState.Pause;
	}

	/// <summary>
	/// 再開ボタン押した時.
	/// </summary>
	public void Resume ()
	{
		GameTimer.instance.ResumeTimer();

		gameState = SeaGameState.Game;
	}

	void TimeUp () 
	{
		gameState = SeaGameState.Result;

	//	ScoreManager.SetBestScore(bestScore.Value);
	}

	/// <summary>
	/// 動画見終わって、コンティニュー押した時.
	/// </summary>
	public void Continue() 
	{
		GameTimer.instance.PlusTime(10);

		gameState = SeaGameState.Game;
	}

	/// <summary>
	/// リスタート押した時.
	/// </summary>
	public void ReStart() 
	{
		GameStart();
	}

	/// <summary>
	/// 右ボタン押した時. エディターだと右キーボード
	/// </summary>
	public void Right () 
	{
		if (!GameTimer.instance.timerFlag) return;

		if (turtles[0].mixedId % 2 == 0) Correct();
		else Wrong();
	}

	/// <summary>
	/// 左ボタン押した時. エディターだと左キーボード
	/// </summary>
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
		else 
		{
			combo = 0;
		}

		lastCorrectTime = GameTimer.instance.restTime;

		NextTurtle();

		level = Mathf.Min(totalCorrect/30 +1, 9);

		CalculateScore();
	}

	void Wrong ()
	{
		totalWrong ++;

		combo = 0;
	}

	void CalculateScore () 
	{
		score.Value += (totalCorrect * (combo + maxCombo + 1));

		bestScore.Value = Mathf.Max(bestScore.Value, score.Value);
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
