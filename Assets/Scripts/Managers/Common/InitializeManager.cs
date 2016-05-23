using UnityEngine;
using System.Collections;

/// <summary>
/// 全てのシーンに配置する
/// このコンポーネントが配置されているシーンから始まることが可能
/// </summary>
public class InitializeManager : MonoBehaviour 
{
	static InitializeManager instance;

	void Awake ()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		} 
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);

			StartCoroutine(Initialize());
		}
	}

	IEnumerator Initialize () 
	{
		// TODO : Set Master, Transaction Datas //

		yield return new WaitForSeconds(1);

		GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().Initialized();
	}
}
