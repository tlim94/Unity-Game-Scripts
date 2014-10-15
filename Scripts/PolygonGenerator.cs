using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class PolygonGenerator : MonoBehaviour {

	private List<Vector3> newVertices = new List<Vector3>();
	private List<int> newTriangles = new List<int>();
	private List<Vector2> newUV = new List<Vector2>();
	
	private double tUnitX;
	private double tUnitY;
	//private Vector2 tUnit = new Vector2(1, 1);

	private Vector2 uBlock1 = new Vector2 (0, 0);
	private Vector2 uBlock2 = new Vector2 (1, 0);
	private Vector2 uBlock3 = new Vector2 (2, 0);
	private Vector2 uBlock4 = new Vector2 (3, 0);
	private Vector2 uBlock5 = new Vector2 (4, 0);
	private Vector2 uBlock6 = new Vector2 (5, 0);
	private Vector2 uBlock7 = new Vector2 (6, 0);
	private Vector2 uBlock8 = new Vector2 (7, 0);

	private int squareCount;
	private int skip = 0;
	private byte[,] blocks;

	private List<Vector3> colVertices = new List<Vector3>();
	private List<int> colTriangles = new List<int>();
	private int colCount;
	private int rowCount = 0;
	
	private MeshCollider col;

	private Mesh mesh;

	// Use this for initialization
	void Start () {

		mesh = GetComponent<MeshFilter>().sharedMesh;
		col = GetComponent<MeshCollider>();

		Texture mainText = renderer.sharedMaterial.mainTexture;
		Debug.Log (mainText.height);
		Debug.Log (mainText.width);

		tUnitX = 449.5f / mainText.width;
		tUnitY = 832f / mainText.height;
		Debug.Log ((float)tUnitX);
		Debug.Log ((float)tUnitY);
		//tUnit = new Vector2(1, 1);

		GenTerrain();
		BuildMesh();
		UpdateMesh();


	}

	void GenSquare(int x, int y, Vector2 texture){
	//	0 0

		newVertices.Add(new Vector3 (x, y , 0));
		newVertices.Add(new Vector3 (x + 1, y, 0));
		newVertices.Add(new Vector3 (x + 1, y - 2 , 0));
		newVertices.Add(new Vector3 (x, y - 2, 0));
		
		newTriangles.Add((squareCount * 4));	//0
		newTriangles.Add((squareCount * 4) + 1);//1
		newTriangles.Add((squareCount * 4) + 3);//3
		newTriangles.Add((squareCount * 4) + 1);//1
		newTriangles.Add((squareCount * 4) + 2);//2
		newTriangles.Add((squareCount * 4) + 3);//3

		var unitX = (float)tUnitX;
		var unitY = (float)tUnitY;
		
		newUV.Add(new Vector2(unitX * texture.x, unitY * texture.y + unitY));
		newUV.Add(new Vector2(unitX * texture.x + unitX, unitY * texture.y + unitY));
		newUV.Add(new Vector2(unitX * texture.x + unitX, unitY * texture.y));
		newUV.Add(new Vector2(unitX * texture.x, unitY * texture.y));
			
		squareCount++;
	}

	void GenTerrain(){
		blocks = new byte[300, 200];
		
		for(int px = 0; px < blocks.GetLength(0); px++){

			for(int py = 0; py < blocks.GetLength(1); py++){
				blocks[px, py] = (byte)Random.Range(1, 8);
					//The next three lines remove dirt and rock to make caves in certain places
					if(Noise(px, py * 2, 16, 32, 1) > 10){ //Caves
						blocks[px, py] = 0;
						
					}
			}
		}
	}

	void BuildMesh(){
		for(int px = 0; px < blocks.GetLength(0); px++){

			for(int py = 0; py < blocks.GetLength(1); py++){

				if(py % 2 == 1){
					skip = 1;
				}
				else{
					skip = 0;
				}

				//Debug.Log(skip);

				if(blocks[px, py] != 0){
					GenCollider(px, py * 2);
				}

				if(blocks[px, py] == 1){
					GenSquare(px, py * 2, uBlock1);
				} 
				else if(blocks[px, py] == 2){
					GenSquare(px, py * 2, uBlock2);
				}
				else if(blocks[px, py] == 3){
					GenSquare(px, py * 2, uBlock3);
				}
				else if(blocks[px, py] == 4){
					GenSquare(px, py * 2, uBlock4);
				}
				else if(blocks[px, py] == 5){
					GenSquare(px, py * 2, uBlock5);
				}
				else if(blocks[px, py] == 6){
					GenSquare(px, py * 2, uBlock6);
				}
				else if(blocks[px, py] == 7){
					GenSquare(px, py * 2, uBlock7);
				}
				else if(blocks[px, py] == 8){
					GenSquare(px, py * 2, uBlock8);
				}

			}
			rowCount++;
			Debug.Log(rowCount);
		}
	}

	void GenCollider(int x, int y){
		
		//Top
		if(Block(x, y + 1) == 0){
			colVertices.Add(new Vector3(x, y, 1));
			colVertices.Add(new Vector3(x + 1, y, 1));
			colVertices.Add(new Vector3(x + 1, y, 0));
			colVertices.Add(new Vector3(x, y, 0));
			
			ColliderTriangles();
			
			colCount++;
		}
		
		//bot
		if(Block(x, y-1) == 0){
			colVertices.Add(new Vector3(x, y - 1, 0));
			colVertices.Add(new Vector3(x + 1, y - 1, 0));
			colVertices.Add(new Vector3(x + 1, y - 1, 1));
			colVertices.Add(new Vector3(x, y -1, 1));
			
			ColliderTriangles();
			colCount++;
		}
		
		//left
		if(Block(x - 1, y) == 0){
			colVertices.Add(new Vector3(x, y - 1, 1));
			colVertices.Add(new Vector3(x, y, 1));
			colVertices.Add(new Vector3(x, y, 0 ));
			colVertices.Add(new Vector3(x, y - 1, 0));
			
			ColliderTriangles();
			
			colCount++;
		}
		
		//right
		if(Block(x+1,y)==0){
			colVertices.Add(new Vector3(x + 1, y, 1));
			colVertices.Add(new Vector3(x + 1, y - 1 , 1));
			colVertices.Add(new Vector3(x + 1, y - 1 , 0));
			colVertices.Add(new Vector3(x + 1, y, 0));
			
			ColliderTriangles();
			
			colCount++;
		}
		
	}

	void ColliderTriangles(){
		colTriangles.Add(colCount * 4);
		colTriangles.Add((colCount * 4) + 1);
		colTriangles.Add((colCount * 4) + 3);
		colTriangles.Add((colCount * 4) + 1);
		colTriangles.Add((colCount * 4) + 2);
		colTriangles.Add((colCount * 4) + 3);
	}

	byte Block (int x, int y){
		
		if(x == -1 || x == blocks.GetLength(0) || y == -1 || y == blocks.GetLength(1)){
			return (byte)0;
		}
		
		return blocks[x, y];
	}

	int Noise (int x, int y, float scale, float mag, float exp){
		return (int)(Mathf.Pow((Mathf.PerlinNoise(x / scale, y /scale) * mag),(exp))); 
	}

	void UpdateMesh(){

		Mesh newMesh = new Mesh();
		newMesh.vertices = colVertices.ToArray();
		newMesh.triangles = colTriangles.ToArray();
		col.sharedMesh = newMesh;
		
		colVertices.Clear();
		colTriangles.Clear();
		colCount = 0;

		mesh.Clear();
		mesh.vertices = newVertices.ToArray();
		mesh.triangles = newTriangles.ToArray();
		mesh.uv = newUV.ToArray();
		mesh.Optimize();
		mesh.RecalculateNormals();

		squareCount = 0;
		newVertices.Clear();
		newTriangles.Clear();
		newUV.Clear();
	}

	// Update is called once per frame
	void Update () {

	}
}
