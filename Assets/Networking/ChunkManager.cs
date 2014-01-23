using UnityEngine;
using System.Collections;

public class ChunkManager : Photon.MonoBehaviour
{
	public float Seed;
	public GameObject Ship;

	Chunk ActiveChunk {get {return _activeChunk;}
		set{ 
			if (_activeChunk.position != value.position) {
				GenerateChunkBlock(value);
				_activeChunk = value;
			}
			else {
				// Do nothing.
			}
		}}
	
	// Just an underlying private property, not actually used.
	Chunk _activeChunk;
	
	
	Dictionary<Vector3, Chunk> Chunks = new Dictionary<Vector3, Chunk>();
	float chunksize = 1000f;
	// Use this for initialization
	void Start ()
	{
		// Generate the starting chunk
		startingPosition = Ship.transform.position;
		
		startingPosition = new Vector3((int)(Ship.transform.position.x / chunksize), (int)(Ship.transform.position.y / chunksize),(int)(Ship.transform.position.z / chunksize));
		GenerateChunk (startingPosition);
		GenerateChunkBlock(Chunks[startingPosition]);
		
		// Set the active chunk to the starting chunk.
		ActiveChunk = Chunks [startingPosition];
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
		
		// Moving in the positive X direction
		if (Ship.transform.position.x > ActiveChunk.boundriesX.y) {
			ActiveChunk = Chunks[new Vector3(ActiveChunk.x + 1, ActiveChunk.y, ActiveChunk.z)]
			//GenerateChunk((ActiveChunk.x + 2), ActiveChunk.y, ActiveChunk.z);
		}
		// Moving in the positive Y direction
		if (Ship.transform.position.y > ActiveChunk.boundriesY.y) {
			ActiveChunk = Chunks[new Vector3(ActiveChunk.x, ActiveChunk.y + 1, ActiveChunk.z)]
			//GenerateChunk(ActiveChunk.x, (ActiveChunk.y + 1), ActiveChunk.z);
		}
		// Moving in the positive Z direction
		if (Ship.transform.position.z > ActiveChunk.boundriesZ.y) {
			ActiveChunk = Chunks[new Vector3(ActiveChunk.x, ActiveChunk.y, ActiveChunk.z + 1)]
			//GenerateChunk(ActiveChunk.x, ActiveChunk.y, (ActiveChunk.z + 1));
		}
		
		
		
		// Moving in the negative X direction
		if (Ship.transform.position.x < ActiveChunk.boundriesX.x) {
			ActiveChunk = Chunks[new Vector3(ActiveChunk.x - 1, ActiveChunk.y, ActiveChunk.z)]
			//GenerateChunk((ActiveChunk.x + 2), ActiveChunk.y, ActiveChunk.z);
		}
		// Moving in the negative Y direction
		if (Ship.transform.position.y < ActiveChunk.boundriesY.x) {
			ActiveChunk = Chunks[new Vector3(ActiveChunk.x, ActiveChunk.y - 1, ActiveChunk.z)]
			//GenerateChunk(ActiveChunk.x, (ActiveChunk.y + 1), ActiveChunk.z);
		}
		// Moving in the negative Z direction
		if (Ship.transform.position.z < ActiveChunk.boundriesZ.x) {
			ActiveChunk = Chunks[new Vector3(ActiveChunk.x, ActiveChunk.y, ActiveChunk.z - 1)]
			//GenerateChunk(ActiveChunk.x, ActiveChunk.y, (ActiveChunk.z + 1));
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
		chunk.GO = chunkIdentifier;
		chunkIdentifier.transform.parent = this.transform;
	}
	
	void GenerateChunkBlock(Chunk centreChunk)
	{
		Vector3 centrePosition = centreChunk.position;
		Vector3[] surroundingPositions = {
			// +X Plane
			new Vector3(centrePosition.x + 1, centrePosition.y, centrePosition.z),
			new Vector3(centrePosition.x + 1, centrePosition.y + 1, centrePosition.z),
			new Vector3(centrePosition.x + 1, centrePosition.y + 1, centrePosition.z + 1),
			new Vector3(centrePosition.x + 1, centrePosition.y, centrePosition.z + 1),
			new Vector3(centrePosition.x + 1, centrePosition.y + 1, centrePosition.z - 1),
			new Vector3(centrePosition.x + 1, centrePosition.y, centrePosition.z - 1),
			new Vector3(centrePosition.x + 1, centrePosition.y - 1, centrePosition.z + 1),
			new Vector3(centrePosition.x + 1, centrePosition.y - 1, centrePosition.z),
			new Vector3(centrePosition.x + 1, centrePosition.y - 1, centrePosition.z - 1),
			// +Y Plane
			new Vector3(centrePosition.x, centrePosition.y + 1, centrePosition.z),
			new Vector3(centrePosition.x, centrePosition.y + 1, centrePosition.z + 1),
			new Vector3(centrePosition.x - 1 , centrePosition.y + 1, centrePosition.z + 1),
			new Vector3(centrePosition.x - 1, centrePosition.y + 1, centrePosition.z),
			new Vector3(centrePosition.x - 1, centrePosition.y + 1, centrePosition.z - 1),
			new Vector3(centrePosition.x, centrePosition.y + 1, centrePosition.z - 1),
			// +Z Plane
			new Vector3(centrePosition.x - 1, centrePosition.y - 1, centrePosition.z + 1),
			new Vector3(centrePosition.x - 1, centrePosition.y, centrePosition.z + 1),
			new Vector3(centrePosition.x, centrePosition.y - 1, centrePosition.z + 1),
			new Vector3(centrePosition.x, centrePosition.y, centrePosition.z + 1),
			// -X Plane
			new Vector3(centrePosition.x - 1, centrePosition.y - 1, centrePosition.z),
			new Vector3(centrePosition.x - 1, centrePosition.y, centrePosition.z - 1),
			new Vector3(centrePosition.x - 1, centrePosition.y - 1, centrePosition.z - 1),
			new Vector3(centrePosition.x - 1, centrePosition.y, centrePosition.z),
			// -Y Plane
			new Vector3(centrePosition.x, centrePosition.y - 1, centrePosition.z - 1),
			new Vector3(centrePosition.x, centrePosition.y - 1, centrePosition.z),
			// -Z Plane
			new Vector3(centrePosition.x, centrePosition.y, centrePosition.z - 1)
		}
		
		foreach (Vector3 v in surroundingPositions)
		{
			GenerateChunk(v);
		}
		
		Chunk[] ChunksToDelete;
		// Delete unnessecery chunks here, using some weird linq statement i.e ChunksToDelete = Chunks.Select(c => 
		// c.key.position.x < centrePosition.x - 1 || 
		// c.key.position.x > centrePosition.x + 1 ||
		// c.key.position.y < centrePosition.y - 1 ||
		// c.key.position.y > centrePosition.y + 1 ||
		// c.key.position.z < centrePosition.z - 1 ||
		// c.key.position.z > centrePosition.z + 1)
		
		foreach (Chunk c in ChunksToDelete)
		{
			RemoveChunk(c);
		}
	}
	/// <summary>
	/// Generates an individual chunk
	/// </summary>
	/// <param name="position"></param>
	void GenerateChunk(Vector3 position)
	{
		// Generate a new chunk only if the existing one doesnt exist.
		Chunk outchunk;
		bool chunkTest = Chunks.TryGetValue(position, out outchunk);
		if (chunkTest == false)
		{
			Chunks[position] = Chunk.GenerateChunk(position, ChunkSize, 1);
		}
		
		
		// Instantiate the chunk.
		BuildChunk (Chunks[position]);
	}
	/// <summary>
	/// Removes the given chunk, but only if it's been instantiated.
	/// </summary>
	/// <param name="chunk"></param>
	void RemoveChunk(Chunk chunk)
	{
		PhotonNetwork.Destroy(chunk.GO);
	}
}
