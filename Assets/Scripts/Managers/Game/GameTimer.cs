using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

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

	Text label;

	Image image;

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

	public void PlusTime (float time)
	{
		restTime += time;
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

		if (label != null) label.text = ((int)restTime).ToString();

		if (image != null) image.fillAmount = (restTime/60f);
	}

	public void SetText (Text _label, Image _image) 
	{
		label = _label;

		image = _image;
	}
}
