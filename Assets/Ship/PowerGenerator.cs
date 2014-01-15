using UnityEngine;
using System.Collections;

public class PowerGenerator : ShipComponent {
	public float Capability = 10000f;
	public float Modifier = 1f;
	public float Output = 0f;
	// Use this for initialization
	void Start () {
		// Set default health here. (starts at 300)
		this.healthRef.MaxHealth = 1000f;
		this.ModHealth(500);
	}

	// Update is called once per frame
	void Update () {
		if (healthRef.Alive) {
			Debug.Log("Power Gen is Alive");
			Modifier = this.healthRef.CurrentHealth / this.healthRef.MaxHealth;
			Output = Capability * Modifier;
		} else {
			Debug.Log("Ohnoes");
			Output = 0f;
		}
		Debug.Log (Output);
	}
}
