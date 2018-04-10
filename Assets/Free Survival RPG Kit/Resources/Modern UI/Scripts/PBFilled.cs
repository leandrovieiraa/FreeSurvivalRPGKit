using System.Collections;
using UnityEngine;

public class PBFilled : MonoBehaviour {

	public ProgressBar proggresBar;

	[Header("SETTINGS")]
	public Animator barAnimatior;
	public string animText;
	[Range(0, 100)] public int transitionAfter = 50;

	void Update ()
	{
		if (proggresBar.currentPercent >= transitionAfter) 
		{
			barAnimatior.Play (animText);
		}
	}
}