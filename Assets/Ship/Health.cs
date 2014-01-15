using UnityEngine;

// .SendMessage('ModHealth')(amount)
public class Health : MonoBehaviour
{
	/// <summary>
        /// Indicates the current health of the component or ship
        /// </summary>
	public float CurrentHealth { get; private set; }
	
	/// <summary>
        /// Indicates if this entity is currently alive
        /// </summary>
	public bool Alive { get { return Health > 0; } }
	
	/// <summary>
        /// The maximum amount of hitpoints the entity may have
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
		CurrentHealth = Math.Max(MaxHealth, CurrentHealth + amount);

		return Alive;
	}

	/// <summary>
	/// Revive this entity (Bring it back to maximum health)
	/// </summary>
	public void Revive()
	{
		if (Alive)
			throw new InvalidOperationException("Ship cannot be revived when it is already alive");
			
		CurrentHealth = MaxHealth;
	}

	void Update()
	{
	}
}
