using UnityEngine;

public class BasicNavigationConsole : BasicShipStation {
	// Use this for initialization
	void Start () {
		// Set default health here. (starts at 300)
		this.health.MaxHealth = 100f;
		this.ModHealth(50);
	}
	// Update is called once per frame
	void Update () {

	}
	
	override float Operate(float availablePower)
	{
		return 0;
	}
}

