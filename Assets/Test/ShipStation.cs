using UnityEngine;
using System.Collections;

public class ShipStation : MonoBehaviour {
	public float health;
	public Vector3 Position;
	public bool Active;
	// Use this for initialization
	void Start () {
		
	}

	public void ModHealth (float amount)
	{
		health += amount;
		if (health <= 0) {
			Active = false;
		}
		if (!Active && health >= 200) {
			Active = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
