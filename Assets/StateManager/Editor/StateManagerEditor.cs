using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(StateManager))]
public class StateManagerEditor : Editor {

	public override void OnInspectorGUI ()
	{
		StateManager stateManager = (StateManager) target;

		// 再生中じゃない
		if (!Application.isPlaying){
			base.OnInspectorGUI();

			// アニメーターがない場合、アニメーター作成ボタンを出す
			if (stateManager.stateAnimator == null) 
			{
				if (stateManager.states.Where(state => !string.IsNullOrEmpty(state)).Count() > 0){
					if (GUILayout.Button("Create State Machine"))
					{
						CreateStateMachine(stateManager);
					}
				}
			}
			// アニメーターがある場合、修正ボタンと読み込みボタンを出す
			else 
			{
				if (GUILayout.Button("Modify State Machine"))
				{
					ModifyStateMachine(stateManager);
				}

				if (GUILayout.Button("Reset State"))
				{
					stateManager.states = stateManager.animator.parameters.Select(p => p.name).ToArray();
				}

			}
		
		}
		// 再生中
		else 
		{
			foreach (string state in stateManager.states) 
			{
				if (state == stateManager.presentState) GUI.backgroundColor = Color.red;
				else GUI.backgroundColor = Color.white;

				if (GUILayout.Button(state))
				{
					stateManager.ChangeState(state);
				}
			}

			GUI.backgroundColor = Color.white;
		}
	}

	// ステートマシン作成、子供のステート追加
	void CreateStateMachine (StateManager stateManager) 
	{
		string path = EditorUtility.SaveFilePanel("State Machine", "Assets/StateManager/Animators", stateManager.gameObject.name, "controller");

		if (!string.IsNullOrEmpty(path))
		{
			path = "Assets" + path.Replace(Application.dataPath, "");

			UnityEditor.Animations.AnimatorController animatorController = UnityEditor.Animations.AnimatorController.CreateAnimatorControllerAtPath(path);

			foreach (string state in stateManager.states) 
			{
				AddState(animatorController, state);
			}

			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			stateManager.GetComponent<Animator>().runtimeAnimatorController = animatorController;
		}
	}

	// 新しいステートがあったら追加
	void ModifyStateMachine (StateManager stateManager) {

		UnityEditor.Animations.AnimatorController animatorController = stateManager.stateAnimator as UnityEditor.Animations.AnimatorController;

		string[] animationClipNames = animatorController.animationClips.Select(clip => clip.name).ToArray();

		foreach (string state in stateManager.states) 
		{
			if (!animationClipNames.Contains(state))
			{
				AddState(animatorController, state);
			}
		}

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}

	// 新しいステートを作成
	void AddState (UnityEditor.Animations.AnimatorController animatorController, string stateName)
	{
		if (string.IsNullOrEmpty(stateName)) return;

		if (animatorController.parameters.Select(p => p.name).ToArray().Contains(stateName)) return;

		//Trigger追加
		animatorController.AddParameter(stateName, AnimatorControllerParameterType.Trigger);

		//ステート生成
		UnityEditor.Animations.AnimatorStateMachine fsm = animatorController.layers[0].stateMachine;
		UnityEditor.Animations.AnimatorState state = fsm.AddState(stateName);

		//AnyからのTransition生成
		UnityEditor.Animations.AnimatorStateTransition anyToNormal = fsm.AddAnyStateTransition(state);
		anyToNormal.hasFixedDuration = true;
		anyToNormal.hasExitTime = false;
		anyToNormal.exitTime = 1f;
		anyToNormal.canTransitionToSelf = true;
		anyToNormal.duration = 0f;
		anyToNormal.offset = 0f;
		anyToNormal.AddCondition(UnityEditor.Animations.AnimatorConditionMode.If, 0f, stateName);

		//各ステートにAnimation追加
		AnimationClip clip = new AnimationClip();
		clip.name = stateName;
		state.motion = clip;

		//各Animationにイベント追加
		AnimationEvent animationEvent = new AnimationEvent();
		animationEvent.functionName = "SetState";
		animationEvent.stringParameter = stateName;
		AnimationUtility.SetAnimationEvents(clip, new AnimationEvent[] {animationEvent});

		AssetDatabase.AddObjectToAsset(clip, animatorController);
		AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(clip));
	}
}