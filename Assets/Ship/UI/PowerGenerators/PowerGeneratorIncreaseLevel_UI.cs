using UnityEngine;

public class PowerGeneratorIncreaseLevel_UI : UIControl {
	public PowerGenerator OperatingGenerator;
	void OnMouseDown()
	{
		if (Parent.Active) {
			OperatingGenerator.ModUsage(0.1f);
		} else {
			// Dont do anything if our console is dead/withoutpower
		}
	}
}
