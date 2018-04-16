using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBar : MonoBehaviour {

	[Header("OBJECTS")]
	public Transform loadingBar;
	public Transform textPercent;

	[Header("VARIABLES (IN-GAME)")]
	public bool isOn;
	[Range(0, 100)] public float currentPercent;
	[Range(0, 100)] public int speed;

	void Update ()
	{
		if (currentPercent < 100 && isOn == true) 
		{
			currentPercent += speed * Time.deltaTime;
		}
		loadingBar.GetComponent<Image> ().fillAmount = currentPercent / 100;
		textPercent.GetComponent<Text> ().text = ((int)currentPercent).ToString ("F0") + "%";
	}
}