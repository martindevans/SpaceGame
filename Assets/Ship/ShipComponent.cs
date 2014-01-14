using UnityEngine;
using System.Collections;

[RequireComponent(typeof(healthObj))]
public abstract class ShipComponent : MonoBehaviour {
	healthObj healthRef;
	// Use this for initialization
	void Start () {
		healthRef = this.GetComponent<healthObj>();
	}

	public void ModHealth (float amount)
	{
		healthRef.ModHealth(amount);
	}
	// Update is called once per frame
	void Update () {
		UpdateState();
	}

	public virtual void UpdateState ()
	{
		if (!healthRef.Alive) {
			// Add Global state changes here (smoke etc) 
		} else {
			// and for alive.
		}
	}
}
