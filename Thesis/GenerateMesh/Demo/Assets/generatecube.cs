using UnityEngine;
using System.Collections;

public class generatecube : MonoBehaviour {


	//public float height=100;
	//public float width=100;
	
	// Use this for initialization
	//void Start () 
	//{
//		MeshFilter mf = GetComponent<MeshFilter>();
//		Mesh mesh = new Mesh ();
//		mf.mesh = mesh;
//		
//		// vertices
//		Vector3[] vertices = new Vector3[4]
//		{
//			new Vector3 (0, 0, 0), new Vector3 (width, 0, 0), new Vector3 (0, height, 0), new Vector3 (width, height, 0)
//		};
//		
//		// triangles
//		int[] tri = new int[6];
//		tri[0]=0;
//		tri[1]=2;
//		tri[2]=1;
//		
//		tri[3]=2;
//		tri[4]=3;
//		tri[5]=1;
//		
//		// normals
//		Vector3[] normals = new Vector3[4];
//		normals [0] = -Vector3.forward;
//		normals [1] = -Vector3.forward;
//		normals [2] = -Vector3.forward;
//		normals [3] = -Vector3.forward;
//		
//		//uvs (How textures are displayed)
//		Vector2[] uv = new Vector2[4];
//		
//		uv [0] = new Vector2 (0,0);
//		uv [1] = new Vector2 (1, 0);
//		uv [2] = new Vector2 (0, 1);
//		uv [3] = new Vector2 (1, 1);
//		
//		// assign arrays
//		mesh.vertices = vertices;
//		mesh.triangles = tri;
//		mesh.normals = normals;
//		mesh.uv = uv;
//		
//	}

	public float height=1f;
	public float width=1f;
	public float depth=1f;

	// Use this for initialization
	void Start () {
	
		MeshFilter mf = GetComponent<MeshFilter> ();
		Mesh mesh = new Mesh ();
		mf.mesh = mesh;

		// Vertices

		Vector3[] vertices = new Vector3[]{
		new Vector3(0,0,0), new Vector3(0,height,0), new Vector3(width,height,0), new Vector3(width,0,0),
		new Vector3(0,0,depth), new Vector3(0,height,depth), new Vector3(width,height,depth), new Vector3(width,0,depth)
		};

		// triangles
		int[] tri = new int[36];
		tri [0] = 0;
		tri [1] = 1;
		tri [2] = 2;

		tri [3] = 0;
		tri [4] = 2;
		tri [5] = 3;

		tri [6] = 5;
		tri [7] = 6;
		tri [8] = 1;

		tri [9] = 5;
		tri [10] = 1;
		tri [11] = 0;

		tri [12] = 7;
		tri [13] = 6;
		tri [14] = 5;

		tri [15] = 7;
		tri [16] = 5;
		tri [17] = 4;

		tri [18] = 4;
		tri [19] = 3;
		tri [20] = 7;

		tri [21] = 4;
		tri [22] = 7;
		tri [23] = 8;

		tri [24] = 1;
		tri [25] = 6;
		tri [26] = 3;

		tri [27] = 2;
		tri [28] = 6;
		tri [29] = 7;

		tri [30] = 4;
		tri [31] = 0;
		tri [32] = 7;

		tri [33] = 7;
		tri [34] = 0;
		tri [35] = 1;

		// Normals
		Vector3[] normals = new Vector3[8];

		// assign arrays
		mesh.vertices = vertices;
		mesh.triangles = tri;



	}

}
