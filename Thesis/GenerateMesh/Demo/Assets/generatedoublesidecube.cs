using UnityEngine;
using System.Collections;

public class generatedoublesidecube : MonoBehaviour {

	// Use this for initialization
	public Material material;
	public Material material1;
	public GameObject plane1,plane2,plane3,plane4;
	public Vector3 originalposition;
	public int SizeinX,SizeinY,SizeinZ;
	void Start () 
	{
	
		plane1 = new GameObject ("plane1", typeof(MeshFilter), typeof(MeshRenderer));
		plane2 = new GameObject ("plane2", typeof(MeshFilter), typeof(MeshRenderer));

		//plane3 = new GameObject ("plane3", typeof(MeshFilter), typeof(MeshRenderer));
		//plane4 = new GameObject ("plane4", typeof(MeshFilter), typeof(MeshRenderer));


		MeshFilter mf1 = plane1.GetComponent<MeshFilter> ();
		MeshFilter mf2 = plane2.GetComponent<MeshFilter> ();

		//MeshFilter mf3 = plane2.GetComponent<MeshFilter> ();
		//MeshFilter mf4 = plane4.GetComponent<MeshFilter> ();



		// Custom translate vectors
		originalposition = new Vector3 (0, 2f, 0);

		Mesh mesh1 = new Mesh ();
		Mesh mesh2 = new Mesh ();

		Mesh mesh3 = new Mesh ();
		Mesh mesh4 = new Mesh ();

		plane1.transform.position=originalposition;
		plane2.transform.position=originalposition;

		//plane3.transform.position=originalposition;
		//plane4.transform.position=originalposition;

		// move and rotate plane 1 to show material on -y direction
		plane1.transform.Translate (new Vector3 (0, 0, 1f));
		plane1.transform.Rotate (new Vector3(0,180f,180f));

		// vertices
		Vector3[] vertices1 = {
			new Vector3(0,0,0),
			new Vector3(1,0,0),
			new Vector3(1,0,1),
			new Vector3(0,0,1),

		};

		Vector3[] vertices2 = {
			new Vector3(0,0,0),
			new Vector3(1,0,0),
			new Vector3(1,0,1),
			new Vector3(0,0,1),
		};

		Vector3[] vertices3 = {
			new Vector3(1,1,0),
			new Vector3(1,2,0),
			new Vector3(1,2,1),
			new Vector3(1,1,1),
		};

		//
		int[] tri1 = {
			1,0,2,
			2,0,3
		};
		int[] tri2 = {
			2,1,0,
			0,3,2
		};

		int[] tri3 = {
			2,1,0,
			0,3,2
		};

		// normals
		Vector3[] normals1 = {
			Vector3.down,
			Vector3.down,
			Vector3.down,
			Vector3.down

		};
		Vector3[] normals2 = {
			Vector3.right,
			Vector3.right,
			Vector3.right,
			Vector3.right
			
		};
		mesh1.vertices = vertices1;
		mesh2.vertices = vertices2;
		//mesh3.vertices = vertices3;
		mesh1.triangles = tri1;
		mesh2.triangles = tri2;
		//mesh3.triangles = tri3;
		mesh1.normals = normals1;
		mesh2.normals = normals2;
		mf1.mesh = mesh1;
		mf2.mesh = mesh2;
		//mf3.mesh = mesh3;

		MeshRenderer mr1 = plane1.GetComponent<MeshRenderer> ();

		MeshRenderer mr2 = plane2.GetComponent<MeshRenderer> ();

		mr1.material = material;
		mr2.material = material1;

//		for (int i=0; i<SizeinX; i++) 
//		{
//			for (int j=0; j< SizeinZ-1; j++)
//			{
//				baseshape((2 * i * Vector3.right)+ 2 * j * Vector3.forward);
//			}
//		}

		baseshape1(new Vector3(0,0,0));

	}

	public void baseshape1(Vector3 translateposition)
	{
		// top face
		Instantiate (plane1, originalposition + translateposition, Quaternion.identity);
		Instantiate (plane2, originalposition + translateposition, Quaternion.identity);
		// bottom face
		Instantiate(plane1,originalposition + new Vector3(0,-1f,0) + translateposition,Quaternion.identity);
		Instantiate (plane2, originalposition + new Vector3 (0, -1f, 1f) + translateposition, Quaternion.Euler (new Vector3 (0, 180f, 180f)));

		// face 3 outside 
		Instantiate (plane1, originalposition + translateposition, Quaternion.Euler (90f, 0, 0));
		Instantiate (plane2, originalposition + new Vector3 (1f, 0, 0) + translateposition, Quaternion.Euler (90f, 180f, 0));

		// face 4 inside
		Instantiate (plane1, originalposition + new Vector3 (0, -1f, 1f) + translateposition, Quaternion.Euler (270f, 0, 0));
		Instantiate (plane2, originalposition + new Vector3 (0, 0, 1f) + translateposition, Quaternion.Euler (90f, 0, 0));
	}

	public void baseshape2()
	{

	}

}
