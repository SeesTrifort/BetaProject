using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonStateManager : StateManager {

	[SerializeField, HideInInspector]
	Button button; 

	void Reset () 
	{
		animator = GetComponent<Animator>();
		button = GetComponent<Button>();
		button.transition = Selectable.Transition.Animation;
		states = new string[]{button.animationTriggers.normalTrigger, button.animationTriggers.highlightedTrigger, button.animationTriggers.pressedTrigger, button.animationTriggers.disabledTrigger};
	}
}