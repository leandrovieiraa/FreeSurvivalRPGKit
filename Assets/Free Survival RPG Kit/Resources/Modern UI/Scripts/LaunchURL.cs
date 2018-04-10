using UnityEngine;
using System.Collections;

public class LaunchURL : MonoBehaviour {

	public string URL; 

	public void urlLinkOrWeb() 
	{
		Application.OpenURL(URL);
	}
}
