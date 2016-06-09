using UnityEngine;

public static class ScoreManager
{
	public static int GetBestScore (GameType type) 
	{
		return PlayerPrefs.GetInt(type.ToString(), 0);
	}

	public static void SetBestScore (GameType type, int score)
	{
		PlayerPrefs.SetInt(type.ToString(), score);
		PlayerPrefs.Save();
	}
}
