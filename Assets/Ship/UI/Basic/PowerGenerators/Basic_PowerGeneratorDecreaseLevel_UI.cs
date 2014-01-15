using UnityEngine;

public class Basic_PowerGeneratorDecreaseLevel_UI : BasicUIControl {
	public PowerGenerator OperatingGenerator;
	void OnMouseDown()
	{
		Debug.Log ("Pressing buttons over here");
		if (Parent.Active) {
			OperatingGenerator.ModUsage(-0.1f);
		} else {
			// Dont do anything if our console is dead/withoutpower
		}
	}
}
