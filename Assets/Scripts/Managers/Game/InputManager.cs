using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour 
{
	static InputManager _instance;
	public static InputManager instance {
		get{
			if (_instance == null) {
				_instance = new GameObject().AddComponent<InputManager>();
				_instance.name = "InputManager";
			}
			return _instance;
		}
	}

	SeaGameManager gameManager;

	public void SetGameManager (SeaGameManager _gameManager)
	{
		gameManager = _gameManager;
	}

#if UNITY_EDITOR
	void Update()
	{
		if (gameManager == null) return;

		if (Input.GetKeyDown(KeyCode.LeftArrow)){
			gameManager.Left();
		}else if (Input.GetKeyDown(KeyCode.RightArrow)){
			gameManager.Right();
		}
	}
#endif

}
