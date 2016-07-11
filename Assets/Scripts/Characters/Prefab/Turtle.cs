using UnityEngine;

public class Turtle : Character 
{
	protected Animator animator;

	void Awake () 
	{
		animator = GetComponent<Animator>();
	}

	public void MoveDown ()
	{
		animator.SetTrigger("Move");
	}
}
