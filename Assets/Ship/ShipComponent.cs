using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Health))]
public abstract class ShipComponent : MonoBehaviour
{
	public Health health;
	
	// Use this for initialization
	void Start () {
		health = this.GetComponent<Health>();;
	}

	public void ModHealth (float amount)
	{
		health.ModHealth(amount);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!health.Alive) {
			// Add Global state changes here (smoke etc) 
		} else {
			// and for alive.
		}
	}
	
	/// <summary>
        /// Called once per frame, this is an opportunity for this component to use power
        /// </summary>
        /// <param name="availablePower">The total amount of power available to this component</param>
        /// <returns>The amount of power this component consumed</returns>
	public abstract float Operate(float availablePower)
	{

	}
}
