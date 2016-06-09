using UnityEngine;
using System.Collections;
using System.Linq;

[RequireComponent(typeof(Animator))]
public class StateManager : MonoBehaviour {

	[SerializeField, HideInInspector]
	public Animator animator;

	public RuntimeAnimatorController stateAnimator{
		get{
			return animator.runtimeAnimatorController;
		}
	}

	[SerializeField]
	public string [] states;

	[HideInInspector]
	public string presentState;

	void Reset () 
	{
		animator = GetComponent<Animator>();
	}

	void OnEnable () 
	{
		if (presentState != "")
		{
			ChangeState(presentState);	
		}
	}

	/// <summary>
	/// DO NOT call this method in script.
	/// This will be Called From Animation Clip.
	/// Instead, Use 'ChangeState' method.
	/// </summary>
	public void SetState (string stateName) 
	{
		presentState = stateName;
	}

	public void ChangeState (string stateName) 
	{
		animator.SetTrigger(stateName);
	}
}