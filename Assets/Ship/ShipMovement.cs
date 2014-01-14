using UnityEngine;
using System.Linq;
using System.Collections;

/// <summary>
/// NetWorking class to handle ship movement and sync it
/// </summary>
public class ShipMovement: Photon.MonoBehaviour {
	public Engine[] Engines;
    	public float Thrust;
    	public float TurnMultipler;
    	public GameObject ShipInterior;
	void Start ()
	{

	}
	
	// Update is called once per frame
    void Update ()
	{
		foreach (Engine e in Engines.Where (e => e.Active == true && e.Usage > 0)) {
			this.rigidbody.AddForceAtPosition(e.Direction * e.Power * e.Usage, e.transform.position);
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

            // Just sync Thrust and Turn Multiplier
            stream.SendNext(Thrust);
            stream.SendNext(TurnMultipler);
        }
        else // What should we be recieving from the server?
        {
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();// Get the updated Ship position

            Thrust = (float)stream.ReceiveNext();
            TurnMultipler = (float)stream.ReceiveNext();
        }
    }
}

