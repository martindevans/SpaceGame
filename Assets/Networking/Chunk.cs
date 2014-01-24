using System;
using UnityEngine;

namespace Assets.Networking
{
	/// <summary>
	/// Chunk
	/// </summary>
	public class Chunk
	{
        //Unity Axis Aligned Bounding Box type - http://docs.unity3d.com/Documentation/ScriptReference/Bounds.html
        public Bounds bounds;
		
		public GameObject GO;
		public Vector3 position;
		
		public ChunkObject[] ChunkObjects;
        
		public Chunk(Vector3 position, float size)
		{
            bounds = new Bounds(position, new Vector3(size, size, size));

			this.position = position;
		}
        
		public bool Inside(Vector3 position)
		{
            return bounds.Contains(position);
		}
        
		public static Chunk GenerateChunk(Vector3 position, int size, float density)
		{
            if (density < 0 || density > 1)
                throw new ArgumentOutOfRangeException("density");
        
			Chunk retChunk = new Chunk (position, size);
			
			int numberOfObjects = UnityEngine.Random.Range(0, density * 100);
			
			retChunk.ChunkObjects = new ChunkObject[numberOfObjects];
	
			for (int i = 0; i < numberOfObjects; i++)
			{
				// Get the position of the new object
				float xLocation = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
				float yLocation = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);
				float zLocation = UnityEngine.Random.Range(bounds.min.z, bounds.max.z);
	
				ChunkObject newCO = new ChunkObject(new Vector3(xLocation, yLocation,zLocation), Vector3.one, Quaternion.identity);
                
                // I'd insert some logic here to check if this CO overlaps with any existing COs and if so skip it,
                // that means you can handle more or less infinite density without things getting impossibly overcrowded
                
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