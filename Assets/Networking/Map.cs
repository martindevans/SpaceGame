using UnityEngine;
using System.Collections;

public class Map : Photon.MonoBehaviour
{
	public float Seed;
	public GameObject Ship;

	WorldChunk ActiveChunk;

	int ChunkSize = 250;
	Vector3 startingPosition;
	WorldChunk[,,] Chunks;

	// Use this for initialization
	void Start ()
	{
		// Generate the starting chunk
		Chunks = new WorldChunk[20, 20, 20];
		startingPosition = Ship.transform.position;
		GenerateChunk (0,0,0);
		ActiveChunk = Chunks [0, 0, 0];
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

		/*
		if (Ship.transform.position.x <= ActiveChunk.x * ChunkSize + ChunkSize/2) {
			GenerateChunk((ActiveChunk.x - 1), ActiveChunk.y, ActiveChunk.z);
		}
		if (Ship.transform.position.y <= ActiveChunk.y * ChunkSize + ChunkSize/2) {
			GenerateChunk(ActiveChunk.x, (ActiveChunk.y - 1), ActiveChunk.z);
		}
		if (Ship.transform.position.z <= ActiveChunk.z * ChunkSize + ChunkSize/2) {
			GenerateChunk(ActiveChunk.x, ActiveChunk.y, (ActiveChunk.z - 1));
		}*/
	}
	void BuildChunk(WorldChunk chunk)
	{
		GameObject newChunk = new GameObject (chunk.x.ToString () + "" + chunk.y.ToString () + "" + chunk.z.ToString ());
		newChunk.transform.position = new Vector3 (chunk.x * ChunkSize, chunk.y * ChunkSize, chunk.z * ChunkSize);
		for (int i = 0; i < chunk.ChunkObjects.Length; i++) {
			// Just build asteroids for now, alter WorldChunkObject to hold object name.
			GameObject newAsteroid = PhotonNetwork.InstantiateSceneObject("Asteroid", chunk.ChunkObjects[i].Position, chunk.ChunkObjects[i].Rotation,0,null);
			newAsteroid.transform.parent = newChunk.transform;
		}
		newChunk.transform.parent = this.transform;
	}
	void GenerateChunk(int x, int y, int z)
	{
		// Generate a new chunk, then set it to the active one.
		Chunks[x,y,z] = WorldChunk.GenerateChunk (x, y, z, ChunkSize, 1);

		ActiveChunk = Chunks [x, y, z];
		BuildChunk (ActiveChunk);
	}
}
public class WorldChunk
{
	public int x { get; private set; }
	public int y {get; private set; }
	public int z { get; private set; }

	public bool isBuilt {get;set;}

	public WorldChunkObject[] ChunkObjects;
	int size;

	private WorldChunk(int x, int y, int z, int size)
	{
		this.isBuilt = false;
		this.x = x;
		this.y = y;
		this.z = z;
		this.size = size;
	}

	public static WorldChunk GenerateChunk(int x, int y, int z, int size, int density)
	{
		WorldChunk retChunk = new WorldChunk (x, y, z, size);

		// Set the bounds of the chunk.
		Vector2 xBounds = new Vector2 ((x * size) - size, (x * size) + size);
		Vector2 yBounds = new Vector2 ((y * size) - size, (y * size) + size);
		Vector2 zBounds = new Vector2 ((z * size) - size, (z * size) + size);

		int numberOfObjects = UnityEngine.Random.Range (density * 1, density * 100);

		retChunk.ChunkObjects = new WorldChunkObject[numberOfObjects];

		for (int i = 0; i < numberOfObjects; i++)
		{
			// Get the position of the new object
			float xLocation = UnityEngine.Random.Range (xBounds.x, xBounds.y);
			float yLocation = UnityEngine.Random.Range (yBounds.x, yBounds.y);
			float zLocation = UnityEngine.Random.Range (zBounds.x, zBounds.y);

			WorldChunkObject newWCO = new WorldChunkObject(new Vector3(xLocation, yLocation,zLocation), Vector3.one, Quaternion.identity);
			retChunk.ChunkObjects[i] = newWCO;
		}

		return retChunk;
	}
}

public class WorldChunkObject
{
	public Vector3 Position { get; set; }
	public Quaternion Rotation{ get; set; }
	public Vector3 Scale { get; set; }

	public WorldChunkObject(Vector3 position, Vector3 scale, Quaternion rotation)
	{
		this.Position = position;
		this.Scale = scale;
		this.Rotation = rotation;
	}
}

