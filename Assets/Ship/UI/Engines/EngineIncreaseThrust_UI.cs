using UnityEngine;

public class EngineIncreaseThrust_UI : UIControl {
	public Engine OperatingEngine;
	void OnMouseDown()
	{
		if (Parent.Active) {
			OperatingEngine.ModUsage (0.001f);
		} else {
			// Dont do anything if our console is dead/withoutpower
		}
	}
}