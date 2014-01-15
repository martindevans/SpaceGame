using UnityEngine;

public class EngineIncreaseThrust_UI : UIControl {
	public Engine OperatingEngine;
	void OnMouseDown()
	{
		if (Parent.Active) {
			OperatingEngine.ModUsage (0.001f);
		} else {
			Debug.Log ("Blugh, im dead because my UIConsole is dead," +  Parent.healthRef.CurrentHealth);
		}
	}
}