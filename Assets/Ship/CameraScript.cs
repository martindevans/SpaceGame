using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	// Use this for initialization
    public GameObject playerToMimic;
    public GameObject playerPhysicalShip;

    void Start () {
        this.transform.parent = playerPhysicalShip.transform;
        this.transform.position = Vector3.zero;
        //this.transform.position = playerPhysicalShip.transform.position + playerToMimic.transform.position;
        //this.transform.rotation = playerPhysicalShip.transform.localRotation;
	}

    void Update()
    {
		/*Matrix4x4 test = playerPhysicalShip.transform.localToWorldMatrix * playerToMimic.transform.localToWorldMatrix;
		
		this.transform.position = test.GetColumn(3);
		this.transform.rotation = Quaternion.LookRotation(
				test.GetColumn(2),
				test.GetColumn(1)
			);*/
		
		//this.transform.position = playerPhysicalShip.transform.TransformPoint(playerToMimic.transform.position);
		//this.transform.position += playerToMimic.transform.localPosition;
		//this.transform.rotation *= playerToMimic.transform.localRotation;
		 
		this.transform.localPosition = playerToMimic.transform.localPosition;
		this.transform.localRotation = playerToMimic.transform.localRotation;
		//Debug.Log(playerPhysicalShip.transform.rotation.ToString());
        //this.transform.rotation = playerPhysicalShip.transform.rotation * playerToMimic.transform.localRotation;
        //this.transform.position = playerPhysicalShip.transform.position + playerToMimic.transform.localPosition;
       // transform.LookAt(target);
    }

}


