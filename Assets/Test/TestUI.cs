using UnityEngine;
using System.Collections;

public class TestUI : MonoBehaviour {
	public ShipMovement ShipMovementScript;
	public ShipStation Parent;
	// Use this for initialization
	void Start () {

	}

	void OnMouseOver()
	{

	}
	void OnMouseDown ()
	{
		if (!Parent.Active) {
			Debug.Log ("Clicked!");
			ShipMovementScript.Thrust += 1000f;
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
