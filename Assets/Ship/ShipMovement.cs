using UnityEngine;
using System.Linq;
using System.Collections;

/// <summary>
/// NetWorking class to handle ship movement and sync it
/// </summary>
public class ShipMovement: Photon.MonoBehaviour {
	public PowerGenerator[] PowerGenerators;
	public ShipStation[] ShipStations;
	public AdvancedShipStation[] AdvancedShipStations;
	public Engine[] Engines;
    	public GameObject ShipInterior;

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
		foreach (PowerGenerator p in PowerGenerators.Where (p => p.healthRef.Alive == true)) {
			AvaliablePower += p.Output;
		}

		// Engine Handling
		foreach (Engine e in Engines.Where (e => e.healthRef.Alive == true)) {
			float projectedPowerUsage = (e.Strength * e.Usage) * 100f;
			if ((AvaliablePower - projectedPowerUsage) > 0){
				this.rigidbody.AddForceAtPosition (e.Direction * e.Strength * e.Usage, e.transform.position);
				AvaliablePower -= projectedPowerUsage;
			}
			else {
				// Just dont function if we havent got the power, possibly add some sort of inefficent movement here.
			}
		}

		// Console Handling
		// Medium Ship Stations (Dont need to bother with basic as they dont require any power)
		foreach (ShipStation s in ShipStations.Where (e => e.healthRef.Alive == true)) {
			float projectedPowerUsage = 100f;
			if ((AvaliablePower - projectedPowerUsage) > 0){
				AvaliablePower -= projectedPowerUsage;
				s.HasPower = true;
			}
			else {
				s.HasPower = false;
			}
		}
		// Advanced Ship Stations
		foreach (AdvancedShipStation s in AdvancedShipStations.Where (e => e.healthRef.Alive == true)) {
			float projectedPowerUsage = 300f; // Uses 3x more power than a standard station
			if ((AvaliablePower - projectedPowerUsage) > 0){
				AvaliablePower -= projectedPowerUsage;
				s.HasPower = true;
			}
			else {
				s.HasPower = false;
			}
		}
		
		ShipInterior.transform.position = this.transform.position;
		ShipInterior.transform.rotation = this.transform.rotation;
    	}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting) // What are we sending to the server?
        {
            // Dont send anything, just recieve the ship position here.
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else // What should we be recieving from the server?
        {
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();// Get the updated Ship position
        }
    }
}

