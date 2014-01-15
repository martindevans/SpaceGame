using UnityEngine;

public class BasicNavigationConsole : ShipStation {
	// Use this for initialization
	void Start () {
		// Set default health here. (starts at 300)
		this.healthRef.MaxHealth = 100f;
		this.ModHealth(50);
	}
	// Update is called once per frame
	void Update () {

	}
}

