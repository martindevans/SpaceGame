using UnityEngine;
using System.Collections;

public class Engine : ShipComponent {
	public float Power = 1000f;
	public float Usage = 0f;
	public Vector3 Direction { get { return -this.transform.forward; } }
	// Use this for initialization 
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
