using UnityEngine;

public class AdvancedShipStation : ShipComponent {
	public bool Active { get { return healthRef.Alive && HasPower; } private set{Active = value;}}
	public bool HasPower = false;
	// Use this for initialization
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
		
	}
}