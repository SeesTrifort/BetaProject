using UnityEngine;
using System.Collections;

public abstract class GameManager : MonoBehaviour 
{
	/// <summary>
	/// InitializeManager で全てのデータが読み込み終わった後に呼ばれる.
	/// </summary>
	public abstract void Initialized();
}
