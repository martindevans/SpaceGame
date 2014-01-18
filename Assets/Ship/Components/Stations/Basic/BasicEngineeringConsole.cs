using UnityEngine;

public class BasicEngineeringConsole : BasicShipStation {
	// Use this for initialization
	void Start () {
		// Set default health here. (starts at 300)
		this.health.MaxHealth = 100f;
		this.ModHealth(50);
	}
	// Update is called once per frame
	void Update () {
		Debug.Log ("Basic Engineering Console is .." + this.Active);
	}
	
	public override float Operate(float availablePower)
	{
		return 0;
	}
}

