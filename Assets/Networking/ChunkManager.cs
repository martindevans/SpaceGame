using UnityEngine;
using System.Collections;

public class ChunkManager : Photon.MonoBehaviour
{
	public float Seed;
	public GameObject Ship;

	WorldChunk ActiveChunk;
	WorldChunk[,,] Chunks;

	float chunksize = 1000f;
	// Use this for initialization
	void Start ()
	{
		// Generate the starting chunk
		Chunks = new Chunk[20, 20, 20];
		startingPosition = Ship.transform.position;
		
		startingPosition = new Vector3((int)(Ship.transform.position.x / chunksize), (int)(Ship.transform.position.y / chunksize),(int)(Ship.transform.position.z / chunksize));
		GenerateChunk (startingPosition);
		//ActiveChunk = Chunks [startingPosition.x, startingPosition.y, startingPosition.z];
	}
	// Sync the map seed.
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting) // What are we sending to the server?
		{
			stream.SendNext(Seed);
		}
		else // What should we be recieving from the server?
		{
			Seed = (float)stream.ReceiveNext();
			UnityEngine.Random.seed = Seed;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		// When the ship moves more than chunksize/2 , generate the next chunk in advance.
		if (Ship.transform.position.x > (ActiveChunk.x * ChunkSize) - ChunkSize/2) {
			GenerateChunk((ActiveChunk.x + 1), ActiveChunk.y, ActiveChunk.z);
		}
		if (Ship.transform.position.y > (ActiveChunk.y * ChunkSize) - ChunkSize/2) {
			GenerateChunk(ActiveChunk.x, (ActiveChunk.y + 1), ActiveChunk.z);
		}
		if (Ship.transform.position.z > (ActiveChunk.z * ChunkSize) - ChunkSize/2) {
			GenerateChunk(ActiveChunk.x, ActiveChunk.y, (ActiveChunk.z + 1));
		}
	}
	
	
	void BuildChunk(Chunk chunk)
	{
		GameObject chunkIdentifier = new GameObject (chunk.position.x.ToString () + "" + chunk.position.y.ToString () + "" + chunk.position.z.ToString ());
		chunkIdentifier.transform.position = new Vector3 (chunk.position.x * ChunkSize, chunk.position.y * ChunkSize, chunk.position.z * ChunkSize);
		for (int i = 0; i < chunk.ChunkObjects.Length; i++) {
			// Just build asteroids for now, alter WorldChunkObject to hold object name.
			GameObject newAsteroid = PhotonNetwork.InstantiateSceneObject("Asteroid", chunk.ChunkObjects[i].Position, chunk.ChunkObjects[i].Rotation,0,null);
			newAsteroid.transform.parent = chunkIdentifier.transform;
		}
		chunkIdentifier.transform.parent = this.transform;
	}
	void GenerateChunk(Vector3 position)
	{
		// Generate a new chunk, then set it to the active one.
		Chunks[position.x,position.y,position.z] = Chunk.GenerateChunk(position, ChunkSize, 1);
		BuildChunk (ActiveChunk);
	}
}
