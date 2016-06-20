using UnityEngine;
using System.Collections;

public class ButtonClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void HyperbolicTessellationButtonClick()
	{
		Camera [] c = Camera.allCameras;
		print ("c[0] " + c [0]);
		c [0].transform.position = new Vector3 (0, 0, 1.46f);
		c [0].rect = new Rect (0, 0, 1f, 1f);
		c [1].rect = new Rect (0, 0, 0, 0);
	}
		
}
