using UnityEngine;

// .SendMessage('ModHealth')(amount)
public class healthObj : MonoBehaviour
{
	public float Health { get { return _health; } set { _health = value; } }
	public bool Alive { get { return _alive; } set { _alive = value; } }
	public float MaxHealth { get { return _maxhealth; } set { _maxhealth = value; } }
	// Default values when spawned.
	float _health = 0;
	float _maxhealth =1f;
	bool _alive = false;

	void Start(){
	}

	public bool ModHealth (float amount)
	{
		_health += amount;
		if (_health >= _maxhealth) {
			_health = _maxhealth;
		}
		if (_health <= 0) {
			_alive = false;
		}
		if (!_alive && _health >= 200) {
			_alive = true;
		}
		return _alive;
	}

	void Update()
	{
	}
}
