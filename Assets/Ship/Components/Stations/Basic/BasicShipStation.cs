using UnityEngine;

public class BasicShipStation : ShipComponent {
	public bool Active { get { return health.Alive && HasPower; } private set{Active = value;}}
	public bool HasPower = true; // Basic Stations always have power.
	// Use this for initialization
	void Start () {

	}
	// Update is called once per frame
	void Update () {

	}
	
	public override float Operate(float availablePower)
	{
		return 0;
	}
}

