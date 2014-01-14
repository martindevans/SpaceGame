using UnityEngine;
using System.Collections;

public class Engine : ShipComponent {

	public float Strength = 1000f;
	public float Usage = 0f;
	public Vector3 Direction { get { return -this.transform.forward; } }
	// Use this for initialization 
	void Start () {
		// Set default engine health here. (starts at 300)
		this.healthRef.MaxHealth = 1000f;
		this.ModHealth(500);


		// Testing
		this.Usage = 0f;

	}

	public void ModUsage (float amount)
	{
		if ((Usage + amount) > 1)
			Usage = 1;
		else {
			Usage += amount;
		}
		if ((Usage < 0)) {
			Usage = 0;
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
