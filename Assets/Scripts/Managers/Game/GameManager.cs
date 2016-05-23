using UnityEngine;
using System.Collections;

public abstract class GameManager : MonoBehaviour 
{
	/// <summary>
	/// InitializeManager で全てのデータが読み込み終わった後に呼ばれる.
	/// </summary>
	public abstract void Initialized();

	int timerPauseCount = 1;

	protected float restTime {get; private set;}

	protected bool timerFlag 
	{
		get{
			return timerPauseCount == 0;
		}
	}

	protected string restTimeToString 
	{
		get{
			return ((int)restTime).ToString();
		}
	}

	protected void StartTimer (float time) 
	{
		restTime = time;

		timerPauseCount = 0;
	}

	protected void PauseTimer () 
	{
		timerPauseCount ++;

		PauseAction ();
	}

	public abstract void PauseAction ();

	protected void ResumeTimer () 
	{
		timerPauseCount --;

		ResumeAction();
	}

	public abstract void ResumeAction ();

	void Update () 
	{
		if (timerPauseCount != 0) return;

		restTime -= Time.deltaTime;

		if (restTime <= 0) 
		{
			restTime = 0f;

			TimeupAction();	

			timerPauseCount ++;
		}
	}

	public abstract void TimeupAction ();
}
