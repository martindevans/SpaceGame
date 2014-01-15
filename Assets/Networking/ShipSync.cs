using UnityEngine;
using System.Collections;

public class ShipSync : Photon.MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
	
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
	// Update is called once per frame
	void Update ()
	{
	
	}
}

