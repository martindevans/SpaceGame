using UnityEngine;

public class ShipStation : ShipComponent {
	public bool Alive { get { return healthRef.Alive && HasPower; } private set; }
	public bool HasPower = false;
	// Use this for initialization
	void Start () {

	}
	// Update is called once per frame
	void Update () {

	}
}

