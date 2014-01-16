using UnityEngine;

public class AdvancedShipStation : ShipComponent {
	public bool Active { get { return health.Alive && HasPower; } private set{Active = value;}}
	public bool HasPower = false;
	// Use this for initialization
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
		
	}
	
	override float Operate(float availablePower)
	{
		float projectedPowerUsage = 300f; // Uses 3x more power than a standard station
		if ((AvaliablePower - projectedPowerUsage) > 0){
			s.HasPower = true;
			return projectedPowerUsage;
		}
		else {
			s.HasPower = false;
			return 0;
		}
	}
}
