using UnityEngine;

public class CharacterPool : MonoBehaviour 
{

	static CharacterPool _instance;
	public static CharacterPool instance {
		get{
			if (_instance == null) {
				_instance = new GameObject().AddComponent<CharacterPool>();
				_instance.gameObject.SetActive(false);
				_instance.name = "CharacterPool";
			}
			return _instance;
		}
	}
}
