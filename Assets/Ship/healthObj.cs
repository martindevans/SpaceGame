using UnityEngine;

// .SendMessage('ModHealth')(amount)
public class Health : MonoBehaviour
{
	/// <summary>
        /// Indicates the current health of the ships
        /// </summary>
	public float Health { get; private set; }
	
	/// <summary>
        /// Indicates if this ship is currently alive
        /// </summary>
	public bool Alive { get { return Health > 0; } }
	
	/// <summary>
        /// The maximum amount of hitpoints the ship may have
        /// </summary>
	public float MaxHealth { get; set; }

	void Start() {
		// Default values when spawned.
		Health = 0;
		MaxHealth = 1;
	}

	public bool ModHealth (float amount)
	{
		// Update health to new value (capped by max)
		Health = Math.Max(MaxHealth, Health + amount);

		return Alive;
	}
	
	public void Revive()
	{
		if (Alive)
			throw new InvalidOperationException("Ship cannot be revived when it is already alive");
			
		Health = MaxHealth;
	}

	void Update()
	{
	}
}

