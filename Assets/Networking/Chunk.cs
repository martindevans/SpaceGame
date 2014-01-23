using System;
using UnityEngine;

namespace Assets.Networking
{
	/// <summary>
	/// Chunk
	/// </summary>
	public class Chunk
	{
		public Vector2 boundriesX;
		public Vector2 boundriesY;
		public Vector2 boundriesZ;
		
		public GameObject GO;
		public Vector3 position;
		
		public ChunkObject[] ChunkObjects;
		public Chunk(Vector3 position, float size)
		{
			boundriesX = new Vector2((position.x * size) - 0.5*size,  (position.x * size) + 0.5*size);
			boundriesY = new Vector2((position.y * size) - 0.5*size,  (position.y * size) + 0.5*size);
			boundriesZ = new Vector2((position.z * size) - 0.5*size,  (position.z * size) + 0.5*size);
			
			this.position = position;
		}		
		public bool Inside(Vector3 position)
		{
			if (position.x < boundriesX.y && position.x > boundriesX.x)
				if (position.y < boundriesY.y && position.y > boundriesY.x)
					if (position.z < boundriesZ && position.z > boundriesZ.x)
			{
				return true;
			}
			
			return false;
		}
		public static Chunk GenerateChunk(Vector3 position, int size, int density)
		{
			Chunk retChunk = new Chunk (position, size);
			
			int numberOfObjects = UnityEngine.Random.Range (density * 1, density * 100);
			
			retChunk.ChunkObjects = new ChunkObject[numberOfObjects];
	
			for (int i = 0; i < numberOfObjects; i++)
			{
				// Get the position of the new object
				float xLocation = UnityEngine.Random.Range (retChunk.boundriesX.x, retChunk.boundriesX.y);
				float yLocation = UnityEngine.Random.Range (retChunk.boundriesY.x, retChunk.boundriesY.y);
				float zLocation = UnityEngine.Random.Range (retChunk.boundriesZ.x, retChunk.boundriesZ.y);
	
				ChunkObject newCO = new ChunkObject(new Vector3(xLocation, yLocation,zLocation), Vector3.one, Quaternion.identity);
				retChunk.ChunkObjects[i] = newCO;
			}
	
			return retChunk;
		}
		
		private class ChunkObject
		{
			public Vector3 Position { get; set; }
			public Quaternion Rotation{ get; set; }
			public Vector3 Scale { get; set; }
		
			public ChunkObject(Vector3 position, Vector3 scale, Quaternion rotation)
			{
				this.Position = position;
				this.Scale = scale;
				this.Rotation = rotation;
			}
		}
	}
}