using UnityEngine;

public abstract class EngineIncreaseThrust_UI : UIControl {
	public Engine OperatingEngine;
	void OnMouseDown()
	{
		if (Parent.Alive) {
			OperatingEngine.ModUsage (0.001f);
		} else {
			Debug.Log ("Blugh, im dead because my UIConsole is dead");
		}
	}
}