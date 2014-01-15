using UnityEngine;

public class ShipStation : ShipComponent {
	public bool Active { get { return healthRef.Alive && HasPower; } private set{Active = value;}}
	public bool HasPower = false;
	// Use this for initialization
	void Start () {
		// Set default health here. (starts at 300)
		this.healthRef.MaxHealth = 100f;
		this.ModHealth(50);
	}
	// Update is called once per frame
	void Update () {

	}
}

