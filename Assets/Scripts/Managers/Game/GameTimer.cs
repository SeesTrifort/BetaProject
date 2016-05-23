using UnityEngine;
using System.Collections;
using System;

public class GameTimer : MonoBehaviour 
{
	static GameTimer _instance;
	public static GameTimer instance {
		get{
			if (_instance == null) {
				_instance = new GameObject().AddComponent<GameTimer>();
				_instance.name = "GameTimer";
			}
			return _instance;
		}
	}

	int timerPauseCount = 1;

	public float restTime {get; private set;}

	public bool timerFlag 
	{
		get{
			return timerPauseCount == 0;
		}
	}

	public string restTimeToString 
	{
		get{
			return ((int)restTime).ToString();
		}
	}

	Action timeupAction;

	public void StartTimer (float time, Action _timeupAction) 
	{
		restTime = time;

		timeupAction = _timeupAction;

		timerPauseCount = 0;
	}

	public void PauseTimer () 
	{
		timerPauseCount ++;
	}
		
	public void ResumeTimer () 
	{
		timerPauseCount --;
	}

	void Update () 
	{
		if (timerPauseCount != 0) return;

		restTime -= Time.deltaTime;

		if (restTime <= 0) 
		{
			restTime = 0f;

			timerPauseCount ++;

			timeupAction();
		}
	}
}
