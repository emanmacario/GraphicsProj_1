using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGenerator : MonoBehaviour {

	public int size;
	public Material material;

	// Use this for initialization
	void Start() {

		// Add a MeshFilter component to this entity
		MeshFilter mesh = this.gameObject.AddComponent<MeshFilter>();
		mesh.mesh = this.CreateMesh();

		// Add a MeshRenderer component
		MeshRenderer renderer = this.gameObject.AddComponent<MeshRenderer>();
		renderer.material = material;

	}

	// Creates the mesh
	private Mesh CreateMesh() {
		Mesh mesh = new Mesh();
		mesh.Clear();
		mesh.name = "Water";

		List<Vector3> vertices = new List<Vector3>();
		List<int> triangles = new List<int>();
		List<Color> colors = new List<Color>();

		// Define the vertices and colors array
		for (int x = 0; x < size+1; x++)
		{
			for (int z = 0; z < size+1; z++)
			{
				vertices.Add(new Vector3(x, 0.0f, z));
				colors.Add(Color.black);
			}
		}

		// Define the triangles array, appending
		// two sets of triangles denoting one quad
		// at a time
		for (int x = 0; x < size; x++)
		{
			for (int z = 0; z < size; z++)
			{
				// Calculate quad corner indices
				int topLeft = x * (size+1) + z;
				int topRight = topLeft + 1;
				int bottomLeft = (x + 1) * (size+1) + z;
				int bottomRight = bottomLeft + 1;

				// Add first triangle
				triangles.Add(topLeft);
				triangles.Add(bottomRight);
				triangles.Add(bottomLeft);

				// Add second triangle
				triangles.Add(topLeft);
				triangles.Add(topRight);
				triangles.Add(bottomRight);
			}
		}

		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.colors = colors.ToArray();

		//print(vertices.Count);

		return mesh;
	}
}
