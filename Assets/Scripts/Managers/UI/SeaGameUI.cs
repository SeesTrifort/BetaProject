using UnityEngine;
using UnityEngine.UI;

public class SeaGameUI : MonoBehaviour 
{
	public Text[] bestScoreLabels;

	public Text[] scoreLabels;

	public Text timerLabel;

	public Image timerImage;

	void Awake () 
	{
		SetTimer();
	}

	void SetTimer ()
	{
		GameTimer.instance.SetText(timerLabel, timerImage);
	}
		
	public void SetBestScore (int score) 
	{
		if (bestScoreLabels.Length > 0)
			for (int i = 0; i < bestScoreLabels.Length; i++) {
				bestScoreLabels[i].text = Utils.IntToString(score);
			}
	}

	public void SetScore (int score) 
	{
		if (scoreLabels.Length > 0)
			for (int i = 0; i < scoreLabels.Length; i++) {
				scoreLabels[i].text = Utils.IntToString(score);
			}
	}
}
