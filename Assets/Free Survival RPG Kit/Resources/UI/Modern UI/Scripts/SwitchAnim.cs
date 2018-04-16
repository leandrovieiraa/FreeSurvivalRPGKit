using UnityEngine;
using UnityEngine.UI;

public class SwitchAnim : MonoBehaviour {

	[Header("SWITCH")]
	public bool onSwitch;
	public Button switchObject;

	[Header("ANIMATORS")]
	public Animator switchAnimator;
	public Animator onAnimator;
	public Animator offAnimator;

	[Header("ANIM NAMES")]
	public string switchAnim;
	public string onTransition;
	public string offTransition;

	void Start ()
	{
		Button btn = switchObject.GetComponent<Button>();
		switchObject.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		switchAnimator.Play(switchAnim);

		if (onSwitch == true) 
		{
			offAnimator.Play (offTransition);
		} 

		else
		{
			onAnimator.Play(onTransition);
		}
	}
}