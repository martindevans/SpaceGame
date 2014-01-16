using UnityEngine;

public class ShipStation : ShipComponent {
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
		float projectedPowerUsage = 100f;
		if ((availablePower - projectedPowerUsage) > 0){
			HasPower = true;
			return projectedPowerUsage;
		}
		else {
			HasPower = false;
			return 0;
		}
	}
}
