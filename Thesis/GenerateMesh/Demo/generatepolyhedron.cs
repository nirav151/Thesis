using UnityEngine;
using System.Collections;

public class generatepolyhedron : MonoBehaviour {

	public Material material;
	public Vector3 originalposition;
	// Use this for initialization
	void Start () 
	{
		GameObject plane1 = new GameObject ("plane1", typeof(MeshFilter), typeof(MeshRenderer));
		MeshFilter mf = plane1.GetComponent<MeshFilter> ();
		// Custom translate vectors

		Vector3 translate1 = new Vector3 (0, 2f, 1f);
		Vector3 translate2 = new Vector3 (2f, 0, 1f);


		Mesh mesh = new Mesh ();
		mf.mesh = mesh;

		// move skew polyhedra to new original position
		plane1.transform.position = originalposition;

		// vertices
		Vector3[] vertices = {
		// top face
			new Vector3 (0, 1, 0), 
			new Vector3 (1, 1, 0),
			new Vector3 (1, 1, 1), 
			new Vector3 (0, 1, 1),
		// 
			new Vector3 (1, 0, 0),
			new Vector3 (1, 0, 1),
			new Vector3 (1, 1, 1),
			new Vector3 (1, 1, 0),

		//face 3
			new Vector3 (0, 0, 0),
			new Vector3 (1, 0, 0),
			new Vector3 (1, 0, 1),
			new Vector3 (0, 0, 1),

		// face 4
			new Vector3 (0, 0, 0),
			new Vector3 (0, 0, 1),
			new Vector3 (0, 1, 1),
			new Vector3 (0, 1, 0)
		};

		// triangles
		int[] tri = {
		//triangle 1
			1,0,2,
		//trinagle 2
			2,0,3,

		//triangle 3
			4,7,6,
		// triangle 4
			6,5,4,

		// triangle 5
			9,10,8,
		// triangle 6
			8,10,11,

		//triangle 7
			12,13,15,
		// triangle 8
			15,13,14
		};
		// uvs
		Vector2[] uvs = {
			new Vector2 (0, 0),
			new Vector2 (1, 0),
			new Vector2 (1, 1),
			new Vector2 (0, 1),

			new Vector2 (0, 0),
			new Vector2 (1, 0),
			new Vector2 (1, 1),
			new Vector2 (0, 1),

			new Vector2 (0, 0),
			new Vector2 (1, 0),
			new Vector2 (1, 1),
			new Vector2 (0, 1),

			new Vector2 (0, 0),
			new Vector2 (1, 0),
			new Vector2 (1, 1),
			new Vector2 (0, 1),

		};

		// assign them
		mesh.vertices = vertices;
		mesh.triangles = tri;
		mesh.uv = uvs;


		MeshRenderer mr = plane1.GetComponent<MeshRenderer> ();
		mr.material = material;

		// original along with plane 1
		GameObject base2 = (GameObject)Instantiate (plane1, originalposition + translate1, Quaternion.Euler (new Vector3 (90f, 0, 0)));
		Instantiate (plane1, originalposition + translate2, Quaternion.Euler (new Vector3 (0, -90f, 0)));

//		// Clone in Y for 2 layers k=1,2
//		for (int k=0; k<2; k++) 
//		{
//
//			originalposition = originalposition + k * (Vector3.up);
//
//			if (k == 0) {
//				for (int i=1; i<2; i++)
//				{
//					// clone in -X
//					Instantiate (plane1, originalposition + 2 * i * (Vector3.left), Quaternion.identity);
//					Instantiate (plane1, originalposition + 2 * i * (Vector3.left) + translate1, Quaternion.Euler (new Vector3 (90f, 0, 0)));
//					Instantiate (plane1, originalposition + 2 * i * (Vector3.left) + translate2, Quaternion.Euler (new Vector3 (0, -90f, 0)));
//
//					// clone in Z
//					Instantiate (plane1, originalposition + 2 * i * (Vector3.forward), Quaternion.identity);
//					Instantiate (plane1, originalposition + 2 * i * (Vector3.forward) + translate1, Quaternion.Euler (new Vector3 (90f, 0, 0)));
//					Instantiate (plane1, originalposition + 2 * i * (Vector3.forward) + translate2, Quaternion.Euler (new Vector3 (0, -90f, 0)));	
//
//					// clone in -XZ
//					Instantiate (plane1, originalposition + 2 * i * (Vector3.forward + Vector3.left), Quaternion.identity);
//					Instantiate (plane1, originalposition + 2 * i * (Vector3.forward + Vector3.left) + translate1, Quaternion.Euler (new Vector3 (90f, 0, 0)));
//					Instantiate (plane1, originalposition + 2 * i * (Vector3.forward + Vector3.left) + translate2, Quaternion.Euler (new Vector3 (0, -90f, 0)));	
//				}
//			} 
//			else
//			{
//				for (int i=1; i<2; i++) 
//				{
//					// original
//					Instantiate (plane1, originalposition, Quaternion.identity);
//					Instantiate (plane1, originalposition + translate1, Quaternion.Euler (new Vector3 (90f, 0, 0)));
//					Instantiate (plane1, originalposition + translate2, Quaternion.Euler (new Vector3 (0, -90f, 0)));
//
//					// clone in -X
//					Instantiate (plane1, originalposition + 2 * i * (Vector3.left), Quaternion.identity);
//					Instantiate (plane1, originalposition + 2 * i * (Vector3.left) + translate1, Quaternion.Euler (new Vector3 (90f, 0, 0)));
//					Instantiate (plane1, originalposition + 2 * i * (Vector3.left) + translate2, Quaternion.Euler (new Vector3 (0, -90f, 0)));
//					
//					// clone in Z
//					Instantiate (plane1, originalposition + 2 * i * (Vector3.forward), Quaternion.identity);
//					Instantiate (plane1, originalposition + 2 * i * (Vector3.forward) + translate1, Quaternion.Euler (new Vector3 (90f, 0, 0)));
//					Instantiate (plane1, originalposition + 2 * i * (Vector3.forward) + translate2, Quaternion.Euler (new Vector3 (0, -90f, 0)));	
//					
//					// clone in -XZ
//					Instantiate (plane1, originalposition + 2 * i * (Vector3.forward + Vector3.left), Quaternion.identity);
//					Instantiate (plane1, originalposition + 2 * i * (Vector3.forward + Vector3.left) + translate1, Quaternion.Euler (new Vector3 (90f, 0, 0)));
//					Instantiate (plane1, originalposition + 2 * i * (Vector3.forward + Vector3.left) + translate2, Quaternion.Euler (new Vector3 (0, -90f, 0)));	
//				}
//			}

//		}

	}
}
