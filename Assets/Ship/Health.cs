using UnityEngine;
using AssemblyCSharp;


public class Health : MonoBehaviour
{
	/// <summary>
        /// Indicates the current health of the component or ship
        /// </summary>
	public float CurrentHealth { get; private set; }
	
	/// <summary>
        /// Indicates if this entity is currently alive
        /// </summary>
	public bool Alive { get { return CurrentHealth > 0; } private set{ Alive = value; } }
	
	/// <summary>
        /// The maximum amount of hitpoints the entity may have
        /// </summary>
	public float MaxHealth { get; set; }
	void Awake(){
		// Default values when spawned. Do this before initalizing anything else.
		CurrentHealth = 0;
		MaxHealth = 1;
	}
	void Start() {

	}

	public bool ModHealth (float amount)
	{
		// Update health to new value (capped by max)
		CurrentHealth = Math.Max(MaxHealth, CurrentHealth + amount);
		return Alive;
	}

	/// <summary>
	/// Revive this entity (Bring it back to maximum health)
	/// </summary>
	public void Revive()
	{
		if (Alive) {
			Debug.Log ("Ship cannot be revived when it is already alive");
		} else {
			CurrentHealth = MaxHealth;
		}
	}

	void Update()
	{
	}
}
