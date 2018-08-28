using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainGen : MonoBehaviour
{
	/* creates a 1x1 tile of terrain with a total height of 1 from top to 
	bottom, with the 0 point at the waterline (so the terrain is contained
	in a 1x1x1 cube at 0,-waterline,0) */
	
	public int iterations = 7;
	public float roughness = 0.5f;
	
	// heights where waterline = 0 and maxheight (0.5) = 1 
	public float snowline = 0.85f;
	public float treeline = 0.72f;
	public float highlands = 0.35f;
	public float beach = 0.05f;
	
	public float waterline = 0f;
	
	 
	float snowline_h;
	float treeline_h;
	float highlands_h;
	float beach_h;
	
	//colours nicked from wind waker;
	public Color ice = new Color(0.8f, 0.91f, 0.91f);
	public Color rock = new Color(0.34f, 0.36f, 0.4f);
	public Color forest = new Color(0.19f, 0.46f, 0.15f);
	public Color grass = new Color(0.42f, 0.55f, 0.22f);
	public Color sand = new Color(0.77f, 0.77f, 0.62f);
	
	
	// Use this for initialization
	void Start()
	{
		// Add a MeshFilter component to this entity. This essentially comprises of a
		// mesh definition, which in this example is a collection of vertices, colours 
		// and triangles (groups of three vertices). 
		MeshFilter Mesh = this.gameObject.AddComponent<MeshFilter>();
		Mesh.mesh = this.CreateTerrain();
		Mesh.mesh.RecalculateNormals();

		// Add a MeshRenderer component. This component actually renders the mesh that
		// is defined by the MeshFilter component.
		MeshRenderer renderer = this.gameObject.AddComponent<MeshRenderer>();
		renderer.material.shader = Shader.Find("Unlit/TerrainShader");
	}
	
	Mesh CreateTerrain() {
		Mesh m = new Mesh();
		m.name = "Terrain";
		
		float[,] heights = this.DiamondSquare(iterations);
		
		List<Vector3> vertices = new List<Vector3> ();
		List<Color> colors = new List<Color> ();
		
		
		//might as well do this here
		snowline_h = Mathf.Lerp(waterline+0.5f, 1.0f, snowline);
		treeline_h = Mathf.Lerp(waterline+0.5f, 1.0f, treeline);
		highlands_h = Mathf.Lerp(waterline+0.5f, 1.0f, highlands);
		beach_h = Mathf.Lerp(waterline+0.5f, 1.0f, beach);
		
		//add the vertices one by one
		for (int i=0; i<heights.GetLength(0); i++){
			for (int j=0; j<heights.GetLength(1); j++){
				float x = Mathf.InverseLerp(0, heights.GetLength(0)-4,i) - 0.5f; //no clue why -4 works
				float y = Mathf.InverseLerp(0,heights.GetLength(1)-1,j) - 0.5f; 
				 vertices.Add(new Vector3(x, heights[i,j]- 0.5f- waterline, y));
				colors.Add(getColor((float)heights[i,j]));
			}
		}
		
		//make the triangles
		List<int> triangles = new List<int> ();
		for (int i=0; i<heights.GetLength(0)-1; i++){
			for (int j=0; j<heights.GetLength(1)-1; j++){
				int length = heights.GetLength(1);
				triangles.Add(i*length + j);
				triangles.Add(i*length + j + 1);
				triangles.Add((i+1)*length + j);
				
				triangles.Add((i+1)*length + j);
				triangles.Add(i*length + j + 1);
				triangles.Add((i+1)*length + j + 1);
			}
		}
		m.vertices = vertices.ToArray();
		m.colors = colors.ToArray();
		m.triangles = triangles.ToArray();

		return m;
	}
	
	Color getColor(float height) {		
		if (height > snowline_h) {
			return ice;
		}
		if (height > treeline_h) {
			return rock;
		}
		if (height > highlands_h) {
			return forest;
		}
		if (height > beach_h) {
			return grass;
		}
		return sand;
	}
	
	float[,] DiamondSquare(int depth) {
		int size = 1+(int)Mathf.Round(Mathf.Pow(2,depth));
		double[,] output = new double[size,size];
		float scale = 7;
		//intitialise the corners
		output[0,0] = this.random()*scale;
		output[0,size-1] = this.random()*scale;
		output[size-1,size-1] = this.random()*scale;
		output[size-1,0] = this.random()*scale;
		
		//output[0,0] = output[0,size-1] = output[size-1,size-1] = output[size-1,0] = 0.5;
		
		int iters = 0;
		
		while(depth > iters) {
			int divisor = (int)Mathf.Pow(2,iters);
			iters++;			
			scale *= roughness;	
			
			
			//diamond
			for (int i=0; i<divisor; i++){
				for(int j=0; j<divisor; j++){
					int x1,x2,x3,x4,x5,y1,y2,y3,y4,y5;
					
					// 1   2
					//   5
					// 3   4
					
					x1 = x3 = i * (size-1)/divisor;
					x2 = x4 = (i+1) * (size-1)/divisor;
					y1 = y2 = j * (size-1)/divisor;
					y3 = y4 = (j+1) * (size-1)/divisor;
					x5 = (x1+x4)/2;
					y5 = (y1+y4)/2;
					output[x5,y5] = (output[x1,y1] + output[x2,y2] + 
					   output[x3,y3] + output[x4,y4])/4 + this.random()*scale;				
					
				}
			}
			
			//square
			//calculates most points twice but whatevs
			for (int i=0; i<divisor; i++){
				for (int j=0; j<divisor; j++){
					int x1,x2,x3,x4,y1,y2,y3,y4;
					
					//  1
					//4   2  
					//  3
					
					x1 = x3 = ((1+i*2) * (size-1)/divisor)/2;
					x2 = (i+1) * (size-1)/divisor;
					x4 = i * (size-1)/divisor;
					y1 = j * (size-1)/divisor;
					y2 = y4 = ((1+j*2) * (size-1)/divisor)/2;
					y3 = (j+1) * (size-1)/divisor;
					
					int[,] mids = {{x1,y1},{x2,y2},{x3,y3},{x4,y4}};
					
					int diff = (size-1)/(divisor*2);
					for (int k=0; k<4; k++){
						int x = mids[k,0];
						int y = mids[k,1];
						
						int[,] neighs = {{x+diff,y},{x,y+diff},
										 {x-diff,y},{x,y-diff}};
						double sum = 0;
						int count = 0;
						for (int n=0; n<4; n++){
							int xx = neighs[n,0];
							int yy = neighs[n,1];
							if (!(xx < 0 || xx >= size || yy < 0 || yy >= size)){
								sum += output[xx,yy];
								count++;
							}
						}
						output[x,y] = sum/count + this.random()*scale;
						
					}
					
				}
			}
		}
		
		
		return norm2dArray(output);
	}
	
	float[,] norm2dArray(double[,] input) {
		float[,] output = new float[input.GetLength(0), input.GetLength(1)];
		double max = -5;
		double min = 5;
		for (int i=0; i<output.GetLength(0); i++){
			for (int j=0; j<output.GetLength(1); j++){
				if (input[i,j] > max){
					max = input[i,j];
				} else if (input[i,j] < min) {
					min = input[i,j];
				}
			}
		}
		
		for (int i=0; i<output.GetLength(0); i++){
			for (int j=0; j<output.GetLength(1); j++){
				output[i,j] = Mathf.InverseLerp((float)min, (float)max, (float)input[i,j]);
			}
		}
		
		return output;
	}
	
	float random(){
		return Random.value-0.5f;
	}
}
