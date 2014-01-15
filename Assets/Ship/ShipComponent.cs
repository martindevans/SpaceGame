using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Health))]
public abstract class ShipComponent : MonoBehaviour {
	public Health healthRef;
	// Use this for initialization
	void Start () {
		healthRef = this.GetComponent<Health>();;
	}

	public void ModHealth (float amount)
	{
		healthRef.ModHealth(amount);
	}
	// Update is called once per frame
	void Update () {
		if (!healthRef.Alive) {
			// Add Global state changes here (smoke etc) 
		} else {
			// and for alive.
		}
	}
}
