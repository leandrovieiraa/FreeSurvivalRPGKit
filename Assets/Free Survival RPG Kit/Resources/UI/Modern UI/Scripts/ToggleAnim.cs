using UnityEngine;
using UnityEngine.UI;

public class ToggleAnim : MonoBehaviour {


	[Header("TOGGLE")]
	public Toggle toggleObject;

	[Header("ANIMATORS")]
	public Animator toggleAnimator;

	[Header("ANIM NAMES")]
	public string toggleOn;
	public string toggleOff;

	void Start ()
	{
		Toggle tgl = toggleObject.GetComponent<Toggle>();
		toggleObject.onValueChanged.AddListener(TaskOnClick);
	}

	void TaskOnClick(bool value)
	{
		if (toggleObject.isOn) 
		{
			toggleAnimator.Play(toggleOn);
		} 

		else 
		{
			toggleAnimator.Play(toggleOff);
		}
	}
}