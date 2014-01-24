using UnityEngine;
using System.Collections;

public class ChunkManager : Photon.MonoBehaviour
{
	public float Seed;
	public GameObject Ship;

	Chunk _activeChunk;
	Chunk ActiveChunk
    {
        get { return _activeChunk; }
		set
        {
			if (_activeChunk.position != value.position)
            {
				GenerateChunkBlock(value);
				_activeChunk = value;
			}
		}
    }
	
	Dictionary<Vector3, Chunk> Chunks = new Dictionary<Vector3, Chunk>();
    
	const float CHUNK_SIZE = 1000f;
    
	// Use this for initialization
	void Start ()
	{
		// Generate the starting chunk
		startingPosition = Ship.transform.position;
		
		startingPosition = new Vector3((int)(Ship.transform.position.x / CHUNK_SIZE), (int)(Ship.transform.position.y / CHUNK_SIZE),(int)(Ship.transform.position.z / CHUNK_SIZE));
		GenerateChunk (startingPosition);
		GenerateChunkBlock(Chunks[startingPosition]);
		
		// Set the active chunk to the starting chunk.
		ActiveChunk = Chunks [startingPosition];
	}
	
	// Sync the map seed.
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
        // So I don't really know how photon works, but you *really* need to make sure the seed is synced before generating a single thing.
        // I'd have a boolean field like:
        //
        // bool isSeedAvailable = NETWORKING.IsThisTheServer;
        //
        // And then in this method (when a non server receives the seed) it sets that to true. Obviously then in all your generate methods you check of the flag is set.
        // And if it isn't refuse to generate a thing
    
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
		
        // Maybe I'm missing something here... but as far as I can see Chunk doesn't have an x, y or z property? :S
        // I'm *guessing* you mean chunk.position.x ???
        
		// Moving in the positive X direction
		if (Ship.transform.position.x > ActiveChunk.bounds.max.x) {
			ActiveChunk = Chunks[new Vector3(ActiveChunk.x + 1, ActiveChunk.y, ActiveChunk.z)]
			//GenerateChunk((ActiveChunk.x + 2), ActiveChunk.y, ActiveChunk.z);
		}
		// Moving in the positive Y direction
		if (Ship.transform.position.y > ActiveChunk.bounds.max.y) {
			ActiveChunk = Chunks[new Vector3(ActiveChunk.x, ActiveChunk.y + 1, ActiveChunk.z)]
			//GenerateChunk(ActiveChunk.x, (ActiveChunk.y + 1), ActiveChunk.z);
		}
		// Moving in the positive Z direction
		if (Ship.transform.position.z > ActiveChunk.bounds.max.z) {
			ActiveChunk = Chunks[new Vector3(ActiveChunk.x, ActiveChunk.y, ActiveChunk.z + 1)]
			//GenerateChunk(ActiveChunk.x, ActiveChunk.y, (ActiveChunk.z + 1));
		}
		
		
		
		// Moving in the negative X direction
		if (Ship.transform.position.x < ActiveChunk.bounds.min.x) {
			ActiveChunk = Chunks[new Vector3(ActiveChunk.x - 1, ActiveChunk.y, ActiveChunk.z)]
			//GenerateChunk((ActiveChunk.x + 2), ActiveChunk.y, ActiveChunk.z);
		}
		// Moving in the negative Y direction
		if (Ship.transform.position.y < ActiveChunk.bounds.min.y) {
			ActiveChunk = Chunks[new Vector3(ActiveChunk.x, ActiveChunk.y - 1, ActiveChunk.z)]
			//GenerateChunk(ActiveChunk.x, (ActiveChunk.y + 1), ActiveChunk.z);
		}
		// Moving in the negative Z direction
		if (Ship.transform.position.z < ActiveChunk.bounds.min.z) {
			ActiveChunk = Chunks[new Vector3(ActiveChunk.x, ActiveChunk.y, ActiveChunk.z - 1)]
			//GenerateChunk(ActiveChunk.x, ActiveChunk.y, (ActiveChunk.z + 1));
		}
	}
	
	void BuildChunk(Chunk chunk)
	{
		GameObject chunkIdentifier = new GameObject(string.Format("{0} {1} {2}", chunk.position.x, chunk.position.y, chunk.position.z));
		chunkIdentifier.transform.position = new Vector3(chunk.position.x * ChunkSize, chunk.position.y * ChunkSize, chunk.position.z * ChunkSize);
		
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
    
        // I haven't got a clue what the fuck this does?
    
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
		Chunk chunk;
		if (!Chunks.TryGetValue(position, out chunk))
        {
			chunk = Chunk.GenerateChunk(position, ChunkSize, 1);
            Chunks[position] = chunk;
        }
		
		// Instantiate the chunk.
		BuildChunk(chunk);              //Saves an extra dictionary lookup by saving the value from above
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
