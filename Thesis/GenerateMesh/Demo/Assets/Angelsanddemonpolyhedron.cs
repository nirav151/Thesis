using UnityEngine;
using System.Collections;

public class Angelsanddemonpolyhedron : MonoBehaviour {
	
	public GameObject plane1,plane2,plane3,plane4,plane5,plane6,plane7,plane8,plane9,plane10,plane11,plane12,plane13,plane14,plane15,plane16;
	public GameObject plane17,plane18,plane19,plane20,plane21,plane22,plane23,plane24;
	public Material material1, blankmaterial;
	public Vector3 originalposition;
	public int SizeinX, SizeinZ,SizeinY;
	// Use this for initialization
	void Start () 
	{
		BlockinX ();

		BlockinZ ();

		BlockinY ();
		for (int a=0; a<SizeinY; a++) {
			for (int i=0; i<SizeinX; i++) {
				for (int j=0; j< SizeinZ-1; j++) {
					repeateblockX (2 * i * Vector3.right + 2 * j * Vector3.forward + 2 * a * Vector3.up);
				}
			}

			for (int i=0; i<SizeinZ; i++) {
				for (int j=0; j< SizeinX-1; j++) {
					repeateblockZ (2 * i * Vector3.forward + 2 * j * Vector3.right + 2 * a * Vector3.up);
				}
			}
			for (int i=0;i<SizeinX-1; i++)
			{
				for (int j=0; j< SizeinZ-1; j++) 
				{
				repeateblockY (2 * i * Vector3.right + 2 * j * Vector3.forward + 2 * a * Vector3.up);
				}
			}
		}

	}

	public void BlockinZ(){
		// Create new empty gameobjects with meshfilter and mesh renderer
		plane1 = new GameObject ("plane1", typeof(MeshFilter), typeof(MeshRenderer));
		plane2 = new GameObject ("plane2", typeof(MeshFilter), typeof(MeshRenderer));
		plane3 = new GameObject ("plane3", typeof(MeshFilter), typeof(MeshRenderer));
		plane4 = new GameObject ("plane4", typeof(MeshFilter), typeof(MeshRenderer));
		plane5 = new GameObject ("plane5", typeof(MeshFilter), typeof(MeshRenderer));
		plane6 = new GameObject ("plane6", typeof(MeshFilter), typeof(MeshRenderer));
		plane7 = new GameObject ("plane7", typeof(MeshFilter), typeof(MeshRenderer));
		plane8 = new GameObject ("plane8", typeof(MeshFilter), typeof(MeshRenderer));
		
		plane1.transform.position = originalposition;
		plane2.transform.position = originalposition;
		plane3.transform.position = originalposition;
		plane4.transform.position = originalposition;
		plane5.transform.position = originalposition;
		plane6.transform.position = originalposition;
		plane7.transform.position = originalposition;
		plane8.transform.position = originalposition;


		// Get their meshfilters
		MeshFilter mf1 = plane1.GetComponent<MeshFilter> ();
		MeshFilter mf2 = plane2.GetComponent<MeshFilter> ();
		MeshFilter mf3 = plane3.GetComponent<MeshFilter> ();
		MeshFilter mf4 = plane4.GetComponent<MeshFilter> ();
		MeshFilter mf5 = plane5.GetComponent<MeshFilter> ();
		MeshFilter mf6 = plane6.GetComponent<MeshFilter> ();
		MeshFilter mf7 = plane7.GetComponent<MeshFilter> ();
		MeshFilter mf8 = plane8.GetComponent<MeshFilter> ();
		
		// Create two new meshes
		Mesh mesh1 = new Mesh ();
		Mesh mesh2 = new Mesh ();
		Mesh mesh3 = new Mesh ();
		Mesh mesh4 = new Mesh ();
		Mesh mesh5 = new Mesh ();
		Mesh mesh6 = new Mesh ();
		Mesh mesh7 = new Mesh ();
		Mesh mesh8 = new Mesh ();
		
		// Create vertices
		Vector3 [] vertices1 = {
			new Vector3(0,0,0),
			new Vector3(1,0,0),
			new Vector3(1,0,-1),
			new Vector3(0,0,-1)
		};
		
		Vector3 [] vertices2 = {
			new Vector3(0,0,0),
			new Vector3(0,0,-1),
			new Vector3(0,-1,-1),
			new Vector3(0,-1,0)
		};
		
		Vector3[] vertices3 = {
			new Vector3(1,0,0),
			new Vector3(1,-1,0),
			new Vector3(1,-1,-1),
			new Vector3(1,0,-1),
		};
		
		Vector3[] vertices4 = {
			new Vector3(0,-1,0),
			new Vector3(1,-1,0),
			new Vector3(1,-1,-1),
			new Vector3(0,-1,-1),
		};
		
		// Create triangles
		int [] tri1 = {
			0,1,2,
			2,3,0
		};
		
		int [] tri2 = {
			2,1,0,
			0,3,2
		};
		
		// Create uvs
		Vector2[] uv1 = {
			new Vector2(0,0),
			new Vector2(0,1),
			new Vector2(1,1),
			new Vector2(1,0)
		};
		
		Vector2 [] uv2 = {
			new Vector2(0,1),
			new Vector2(0,0),
			new Vector2(1,0),
			new Vector2(1,1)
			
		};
		Vector3 [] normal1 = {
			Vector3.up,
			Vector3.up,
			Vector3.up,
			Vector3.up
		};
		
		Vector3 [] normal2 = {
			Vector3.left,
			Vector3.left,
			Vector3.left,
			Vector3.left
		};
		
		Vector3 [] normal3 = {
			Vector3.right,
			Vector3.right,
			Vector3.right,
			Vector3.right
		};
		
		Vector3 [] normal4 = {
			Vector3.down,
			Vector3.down,
			Vector3.down,
			Vector3.down
		};
		
		Vector2 [] uv3 = {
			new Vector2(0,0),
			new Vector2(1,0),
			new Vector2(1,1),
			new Vector2(0,1)
		};
		
		mesh1.vertices = vertices1;
		mesh1.triangles = tri1;
		mesh1.uv = uv1;
		mesh1.normals = normal1;
		
		mesh2.vertices = vertices1;
		mesh2.triangles = tri2;
		
		mesh3.vertices = vertices2;
		mesh3.triangles = tri1;
		mesh3.uv = uv2;
		mesh3.normals = normal1;
		
		mesh4.vertices = vertices2;
		mesh4.triangles = tri2;
		
		mesh5.vertices = vertices3;
		mesh5.triangles = tri1;
		mesh5.uv = uv3;
		mesh5.normals = normal3;
		
		mesh6.vertices = vertices3;
		mesh6.triangles = tri2;
		
		mesh7.vertices = vertices4;
		mesh7.triangles = tri2;
		mesh7.uv = uv2;
		mesh7.normals = normal4;
		
		mesh8.vertices = vertices4;
		mesh8.triangles = tri1;
		
		// Assign mesh to meshfilter
		mf1.mesh = mesh1;
		mf2.mesh = mesh2;
		mf3.mesh = mesh3;
		mf4.mesh = mesh4;
		mf5.mesh = mesh5;
		mf6.mesh = mesh6;
		mf7.mesh = mesh7;
		mf8.mesh = mesh8;
		
		// Mesh Renderer
		MeshRenderer mr1 = plane1.GetComponent<MeshRenderer> ();
		MeshRenderer mr2 = plane2.GetComponent<MeshRenderer> ();
		MeshRenderer mr3 = plane3.GetComponent<MeshRenderer> ();
		MeshRenderer mr4 = plane4.GetComponent<MeshRenderer> ();
		MeshRenderer mr5 = plane5.GetComponent<MeshRenderer> ();
		MeshRenderer mr6 = plane6.GetComponent<MeshRenderer> ();
		MeshRenderer mr7 = plane7.GetComponent<MeshRenderer> ();
		MeshRenderer mr8 = plane8.GetComponent<MeshRenderer> ();
		
		
		// Assign material to mesh renders
		mr1.material = material1;
		mr2.material = blankmaterial;
		mr3.material = material1;
		mr4.material = blankmaterial;
		mr5.material = material1;
		mr6.material = blankmaterial;
		mr7.material = material1;
		mr8.material = blankmaterial;
	}
	public void BlockinX()
	{
		// Create new empty gameobjects with meshfilter and mesh renderer
		plane9 = new GameObject ("plane9", typeof(MeshFilter), typeof(MeshRenderer));
		plane10 = new GameObject ("plane10", typeof(MeshFilter), typeof(MeshRenderer));
		plane11 = new GameObject ("plane11", typeof(MeshFilter), typeof(MeshRenderer));
		plane12 = new GameObject ("plane12", typeof(MeshFilter), typeof(MeshRenderer));
		plane13 = new GameObject ("plane13", typeof(MeshFilter), typeof(MeshRenderer));
		plane14 = new GameObject ("plane14", typeof(MeshFilter), typeof(MeshRenderer));
		plane15 = new GameObject ("plane15", typeof(MeshFilter), typeof(MeshRenderer));
		plane16 = new GameObject ("plane16", typeof(MeshFilter), typeof(MeshRenderer));
		
		plane9.transform.position = originalposition;
		plane10.transform.position = originalposition;
		plane11.transform.position = originalposition;
		plane12.transform.position = originalposition;
		plane13.transform.position = originalposition;
		plane14.transform.position = originalposition;
		plane15.transform.position = originalposition;
		plane16.transform.position = originalposition;
		
		
		// Get their meshfilters
		MeshFilter mf1 = plane9.GetComponent<MeshFilter> ();
		MeshFilter mf2 = plane10.GetComponent<MeshFilter> ();
		MeshFilter mf3 = plane11.GetComponent<MeshFilter> ();
		MeshFilter mf4 = plane12.GetComponent<MeshFilter> ();
		MeshFilter mf5 = plane13.GetComponent<MeshFilter> ();
		MeshFilter mf6 = plane14.GetComponent<MeshFilter> ();
		MeshFilter mf7 = plane15.GetComponent<MeshFilter> ();
		MeshFilter mf8 = plane16.GetComponent<MeshFilter> ();
		
		// Create two new meshes
		Mesh mesh1 = new Mesh ();
		Mesh mesh2 = new Mesh ();
		Mesh mesh3 = new Mesh ();
		Mesh mesh4 = new Mesh ();
		Mesh mesh5 = new Mesh ();
		Mesh mesh6 = new Mesh ();
		Mesh mesh7 = new Mesh ();
		Mesh mesh8 = new Mesh ();
		
		// Create vertices
		Vector3 [] vertices1 = {
			new Vector3(-1,0,0),
			new Vector3(0,0,0),
			new Vector3(0,-1,0),
			new Vector3(-1,-1,0)
		};
		
		Vector3 [] vertices2 = {
			new Vector3(0,0,0),
			new Vector3(-1,0,0),
			new Vector3(-1,0,1),
			new Vector3(0,0,1)
		};
		
		Vector3[] vertices3 = {
			new Vector3(0,0,1),
			new Vector3(0,-1,1),
			new Vector3(-1,-1,1),
			new Vector3(-1,0,1),
		};
		
		Vector3[] vertices4 = {
			new Vector3(0,-1,0),
			new Vector3(-1,-1,0),
			new Vector3(-1,-1,1),
			new Vector3(0,-1,1),
		};

		// Create triangles
		int [] tri1 = {
			0,1,2,
			2,3,0
		};
		
		int [] tri2 = {
			2,1,0,
			0,3,2
		};
		
		// Create uvs
		Vector2[] uv1 = {
			new Vector2(0,0),
			new Vector2(0,1),
			new Vector2(1,1),
			new Vector2(1,0)
		};
		
		Vector2 [] uv2 = {
			new Vector2(0,1),
			new Vector2(0,0),
			new Vector2(1,0),
			new Vector2(1,1)
			
		};
		Vector3 [] normal1 = {
			Vector3.back,
			Vector3.back,
			Vector3.back,
			Vector3.back
		};
		
		Vector3 [] normal2 = {
			Vector3.up,
			Vector3.up,
			Vector3.up,
			Vector3.up
		};
		
		Vector3 [] normal3 = {
			Vector3.forward,
			Vector3.forward,
			Vector3.forward,
			Vector3.forward
		};
		
		Vector3 [] normal4 = {
			Vector3.down,
			Vector3.down,
			Vector3.down,
			Vector3.down
		};

		
		Vector2 [] uv3 = {
			new Vector2(0,0),
			new Vector2(1,0),
			new Vector2(1,1),
			new Vector2(0,1)
		};
		
		mesh1.vertices = vertices1;
		mesh1.triangles = tri1;
		mesh1.uv = uv3;
		mesh1.normals = normal1;
		
		mesh2.vertices = vertices1;
		mesh2.triangles = tri2;
		
		mesh3.vertices = vertices2;
		mesh3.triangles = tri1;
		mesh3.uv = uv1;
		mesh3.normals = normal1;
		
		mesh4.vertices = vertices2;
		mesh4.triangles = tri2;
		
		mesh5.vertices = vertices3;
		mesh5.triangles = tri2;
		mesh5.uv = uv1;
		mesh5.normals = normal3;
		
		mesh6.vertices = vertices3;
		mesh6.triangles = tri1;
		
		mesh7.vertices = vertices4;
		mesh7.triangles = tri2;
		mesh7.uv = uv2;
		mesh7.normals = normal4;
		
		mesh8.vertices = vertices4;
		mesh8.triangles = tri1;
		
		// Assign mesh to meshfilter
		mf1.mesh = mesh1;
		mf2.mesh = mesh2;
		mf3.mesh = mesh3;
		mf4.mesh = mesh4;
		mf5.mesh = mesh5;
		mf6.mesh = mesh6;
		mf7.mesh = mesh7;
		mf8.mesh = mesh8;
		
		// Mesh Renderer
		MeshRenderer mr1 = plane9.GetComponent<MeshRenderer> ();
		MeshRenderer mr2 = plane10.GetComponent<MeshRenderer> ();
		MeshRenderer mr3 = plane11.GetComponent<MeshRenderer> ();
		MeshRenderer mr4 = plane12.GetComponent<MeshRenderer> ();
		MeshRenderer mr5 = plane13.GetComponent<MeshRenderer> ();
		MeshRenderer mr6 = plane14.GetComponent<MeshRenderer> ();
		MeshRenderer mr7 = plane15.GetComponent<MeshRenderer> ();
		MeshRenderer mr8 = plane16.GetComponent<MeshRenderer> ();
		
		
		// Assign material to mesh renders
		mr1.material = material1;
		mr2.material = blankmaterial;
		mr3.material = material1;
		mr4.material = blankmaterial;
		mr5.material = material1;
		mr6.material = blankmaterial;
		mr7.material = material1;
		mr8.material = blankmaterial;
	}

	public void BlockinY(){
		// Create new empty gameobjects with meshfilter and mesh renderer
		plane17 = new GameObject ("plane17", typeof(MeshFilter), typeof(MeshRenderer));
		plane18 = new GameObject ("plane18", typeof(MeshFilter), typeof(MeshRenderer));
		plane19 = new GameObject ("plane19", typeof(MeshFilter), typeof(MeshRenderer));
		plane20 = new GameObject ("plane20", typeof(MeshFilter), typeof(MeshRenderer));
		plane21 = new GameObject ("plane21", typeof(MeshFilter), typeof(MeshRenderer));
		plane22 = new GameObject ("plane22", typeof(MeshFilter), typeof(MeshRenderer));
		plane23 = new GameObject ("plane23", typeof(MeshFilter), typeof(MeshRenderer));
		plane24 = new GameObject ("plane24", typeof(MeshFilter), typeof(MeshRenderer));
		
		plane17.transform.position = originalposition;
		plane18.transform.position = originalposition;
		plane19.transform.position = originalposition;
		plane20.transform.position = originalposition;
		plane21.transform.position = originalposition;
		plane22.transform.position = originalposition;
		plane23.transform.position = originalposition;
		plane24.transform.position = originalposition;
		
		
		// Get their meshfilters
		MeshFilter mf1 = plane17.GetComponent<MeshFilter> ();
		MeshFilter mf2 = plane18.GetComponent<MeshFilter> ();
		MeshFilter mf3 = plane19.GetComponent<MeshFilter> ();
		MeshFilter mf4 = plane20.GetComponent<MeshFilter> ();
		MeshFilter mf5 = plane21.GetComponent<MeshFilter> ();
		MeshFilter mf6 = plane22.GetComponent<MeshFilter> ();
		MeshFilter mf7 = plane23.GetComponent<MeshFilter> ();
		MeshFilter mf8 = plane24.GetComponent<MeshFilter> ();
		
		// Create two new meshes
		Mesh mesh1 = new Mesh ();
		Mesh mesh2 = new Mesh ();
		Mesh mesh3 = new Mesh ();
		Mesh mesh4 = new Mesh ();
		Mesh mesh5 = new Mesh ();
		Mesh mesh6 = new Mesh ();
		Mesh mesh7 = new Mesh ();
		Mesh mesh8 = new Mesh ();
		
		// Create vertices
		
		Vector3 [] vertices1 = {
			new Vector3 (0, 0, 0),
			new Vector3 (1, 0, 0),
			new Vector3 (1, 1, 0),
			new Vector3 (0, 1, 0)
		};
		
		Vector3 [] vertices2 = {
			new Vector3 (0, 0, 0),
			new Vector3 (0, 0, 1),
			new Vector3 (0, 1, 1),
			new Vector3 (0, 1, 0)
		};
		
		Vector3[] vertices3 = {
			new Vector3 (1, 0, 1),
			new Vector3 (0, 0, 1),
			new Vector3 (0, 1, 1),
			new Vector3 (1, 1, 1)
		};
		
		Vector3[] vertices4 = {
			new Vector3 (1, 0, 0),
			new Vector3 (1, 0, 1),
			new Vector3 (1, 1, 1),
			new Vector3 (1, 1, 0)
		};

		
		// Create triangles
		int [] tri1 = {
			0,1,2,
			2,3,0
		};
		
		int [] tri2 = {
			2,1,0,
			0,3,2
		};
		
		// Create uvs
		Vector2[] uv1 = {
			new Vector2(0,0),
			new Vector2(0,1),
			new Vector2(1,1),
			new Vector2(1,0)
		};
		
		Vector2 [] uv2 = {
			new Vector2(0,1),
			new Vector2(0,0),
			new Vector2(1,0),
			new Vector2(1,1)
			
		};
		Vector3 [] normal1 = {
			Vector3.back,
			Vector3.back,
			Vector3.back,
			Vector3.back
		};
		
		Vector3 [] normal2 = {
			Vector3.left,
			Vector3.left,
			Vector3.left,
			Vector3.left
		};
		
		Vector3 [] normal3 = {
			Vector3.forward,
			Vector3.forward,
			Vector3.forward,
			Vector3.forward
		};
		
		Vector3 [] normal4 = {
			Vector3.right,
			Vector3.right,
			Vector3.right,
			Vector3.right
		};
		
		Vector2 [] uv3 = {
			new Vector2(0,0),
			new Vector2(1,0),
			new Vector2(1,1),
			new Vector2(0,1)
		};
		
		mesh1.vertices = vertices1;
		mesh1.triangles = tri2;
		mesh1.uv = uv3;
		mesh1.normals = normal1;
		
		mesh2.vertices = vertices1;
		mesh2.triangles = tri1;
		
		mesh3.vertices = vertices2;
		mesh3.triangles = tri1;
		mesh3.uv = uv1;
		mesh3.normals = normal1;
		
		mesh4.vertices = vertices2;
		mesh4.triangles = tri2;
		
		mesh5.vertices = vertices3;
		mesh5.triangles = tri2;
		mesh5.uv = uv3;
		mesh5.normals = normal3;
		
		mesh6.vertices = vertices3;
		mesh6.triangles = tri1;
		
		mesh7.vertices = vertices4;
		mesh7.triangles = tri2;
		mesh7.uv = uv2;
		mesh7.normals = normal4;
		
		mesh8.vertices = vertices4;
		mesh8.triangles = tri1;
		
		// Assign mesh to meshfilter
		mf1.mesh = mesh1;
		mf2.mesh = mesh2;
		mf3.mesh = mesh3;
		mf4.mesh = mesh4;
		mf5.mesh = mesh5;
		mf6.mesh = mesh6;
		mf7.mesh = mesh7;
		mf8.mesh = mesh8;
		
		// Mesh Renderer
		MeshRenderer mr1 = plane17.GetComponent<MeshRenderer> ();
		MeshRenderer mr2 = plane18.GetComponent<MeshRenderer> ();
		MeshRenderer mr3 = plane19.GetComponent<MeshRenderer> ();
		MeshRenderer mr4 = plane20.GetComponent<MeshRenderer> ();
		MeshRenderer mr5 = plane21.GetComponent<MeshRenderer> ();
		MeshRenderer mr6 = plane22.GetComponent<MeshRenderer> ();
		MeshRenderer mr7 = plane23.GetComponent<MeshRenderer> ();
		MeshRenderer mr8 = plane24	.GetComponent<MeshRenderer> ();
		
		
		// Assign material to mesh renders
		mr1.material = material1;
		mr2.material = blankmaterial;
		mr3.material = material1;
		mr4.material = blankmaterial;
		mr5.material = material1;
		mr6.material = blankmaterial;
		mr7.material = material1;
		mr8.material = blankmaterial;
	}

	public void repeateblockZ(Vector3 translateposition)
	{
		Instantiate (plane1, originalposition + translateposition, Quaternion.identity);
		Instantiate (plane2, originalposition + translateposition, Quaternion.identity);
		Instantiate (plane3, originalposition + translateposition, Quaternion.identity);
		Instantiate (plane4, originalposition + translateposition, Quaternion.identity);
		Instantiate (plane5, originalposition + translateposition, Quaternion.identity);
		Instantiate (plane6, originalposition + translateposition, Quaternion.identity);
		Instantiate (plane7, originalposition + translateposition, Quaternion.identity);
		Instantiate (plane8, originalposition + translateposition, Quaternion.identity);

	}
	public void repeateblockX(Vector3 translateposition)
	{
		Instantiate (plane9, originalposition + translateposition, Quaternion.identity);
		Instantiate (plane10, originalposition + translateposition, Quaternion.identity);
		Instantiate (plane11, originalposition + translateposition, Quaternion.identity);
		Instantiate (plane12, originalposition + translateposition, Quaternion.identity);
		Instantiate (plane13, originalposition + translateposition, Quaternion.identity);
		Instantiate (plane14, originalposition + translateposition, Quaternion.identity);
		Instantiate (plane15, originalposition + translateposition, Quaternion.identity);
		Instantiate (plane16, originalposition + translateposition, Quaternion.identity);
		
	}

	public void repeateblockY(Vector3 translateposition)
	{
		Instantiate (plane17, originalposition + translateposition, Quaternion.identity);
		Instantiate (plane18, originalposition + translateposition, Quaternion.identity);
		Instantiate (plane19, originalposition + translateposition, Quaternion.identity);
		Instantiate (plane20, originalposition + translateposition, Quaternion.identity);
		Instantiate (plane21, originalposition + translateposition, Quaternion.identity);
		Instantiate (plane22, originalposition + translateposition, Quaternion.identity);
		Instantiate (plane23, originalposition + translateposition, Quaternion.identity);
		Instantiate (plane24, originalposition + translateposition, Quaternion.identity);
		
	}
	// Update is called once per frame
	void Update () {
		
	}
}
