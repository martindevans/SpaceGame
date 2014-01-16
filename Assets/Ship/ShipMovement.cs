using UnityEngine;
using System;
using System.Linq;
using System.Collections;

/// <summary>
/// class to handle ship movement
/// </summary>
public class ShipMovement: MonoBehaviour {
	public PowerGenerator[] PowerGenerators;
	public ShipStation[] ShipStations;
	public AdvancedShipStation[] AdvancedShipStations;
	public Engine[] Engines;
    	public GameObject ShipInterior;
	public GameObject ShipNetwork;

	// 1 Unit of thrust = 0.5 Power Units
	public float AvaliablePower = 0f;

	void Start ()
	{
		// Just a git test
	}
	
	// Update is called once per frame
    void Update ()
	{

		// Power Generation Handling
		AvaliablePower = 0f; // Ineffiecent - find a better way to do this.
		foreach (PowerGenerator p in PowerGenerators) {
			AvaliablePower += p.Operate(AvailablePower);
		}

		// Engine Handling
		foreach (Engine e in Engines.Where(e => e.healthRef.Alive == true)) {
			AvailablePower -= e.Operate(AvailablePower)
		}

		// Console Handling

		// Medium Ship Stations (Dont need to bother with basic as they dont require any power)
		foreach (ShipStation s in ShipStations.Where(e => e.healthRef.Alive == true)) {
			AvailablePower -= s.Operate(AvailablePower);
		}
		// Advanced Ship Stations
		foreach (AdvancedShipStation s in AdvancedShipStations.Where(e => e.healthRef.Alive == true)) {
			AvailablePower -= s.Operate(AvailablePower);
		}

		/*
		// When implementing this you need to be careful to handle power generators properly.
		// I suggest returning a negative value from operate on generators.
		foreach (ShipComponent c in Components)
		{
			if (!c.healthRef.Alive)
				continue;
				
			AvailablePower -= s.Operate(AvailablePower)
		}
		*/
		

		//ShipInterior.transform.position = this.transform.position;
		//ShipInterior.transform.rotation = this.transform.rotation;

		ShipNetwork.transform.position = this.transform.position;
		ShipNetwork.transform.rotation = this.transform.localRotation;
	}

	void OnGUI()
	{	int stuff = (int)Math.Round(AvaliablePower); // Round to friendly number
		GUILayout.Label (stuff.ToString());
	}


}

