using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour {

    public GameObject PlayerShipPhysical;
    public GameObject PlayerShipVirtual;
	// Use this for initialization
	void Start () {
        	Physics.IgnoreLayerCollision(8, 9);
	}
	
	// Update is called once per frame
	void Update () {
        PlayerShipPhysical = GameObject.FindGameObjectWithTag("PlayerShip");
        PlayerShipVirtual = GameObject.FindGameObjectWithTag("PlayerShipVirtual");
        if (PlayerShipPhysical != null && PlayerShipVirtual != null)
        {
            Debug.Log("Loading Complete");
            InitializeGame();
            Debug.Log("Destroying LoadingScreen");
            Destroy(gameObject);
        }
    }

    void InitializeGame()
    	{
		Vector3 localPositionMod = new Vector3(0,2,0);

		// Initialize the player at a relative position on the ship.
	        GameObject physPlayer = PhotonNetwork.Instantiate("Player_Phys", PlayerShipVirtual.transform.position + localPositionMod, Quaternion.identity, 0);
		GameObject modelPlayer = PhotonNetwork.Instantiate("Player", PlayerShipPhysical.transform.position + localPositionMod, Quaternion.identity, 0);
	        physPlayer.transform.parent = PlayerShipVirtual.transform;
	        physPlayer.GetComponent<CharacterController>().enabled = true;

	        Camera.main.gameObject.AddComponent<CameraScript>();
	        Camera.main.gameObject.GetComponent<CameraScript>().playerToMimic = physPlayer;
	        Camera.main.gameObject.GetComponent<CameraScript>().playerPhysicalShip = PlayerShipPhysical;

		modelPlayer.transform.position = Camera.main.gameObject.transform.position;
		modelPlayer.transform.localPosition = Camera.main.gameObject.transform.localPosition;
		modelPlayer.transform.parent = Camera.main.gameObject.transform;
	       // Camera.main.GetComponent<CameraScript>().target = newPlayer.transform;
	 }

    

}
