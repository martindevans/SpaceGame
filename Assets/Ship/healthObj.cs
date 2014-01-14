using UnityEngine;

public class healthObj : MonoBehaviour
{
	public float Health { get { return _health; } set { _health = value; } }
	public bool Alive { get { return _alive; } set { _alive = value; } }
	float _health = 0;
	bool _alive = false;

	void Start(){

	}

	public void ModHealth (float amount){
		_health += amount;
		if (_health <= 0) {
			_alive = false;
		}
		if (!_alive && _health >= 200) {
			_alive = true;
		}
	}

	void Update()
	{
	}
}

