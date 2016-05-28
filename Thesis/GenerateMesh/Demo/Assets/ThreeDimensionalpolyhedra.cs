using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ThreeDimensionalpolyhedra : MonoBehaviour {

	public Material material;
	GameObject tile,tile1,tile2,tile3,tile4,tile5,tile6,tile7,tile8,tile9,tile10,tile11;
	Vector3[] vertices;
	LineRenderer linerenderer;
	LineRenderer linerenderer2;
	Polygon a;
	Ray ray;
	RaycastHit hit;
	// Use this for initialization
	void Start () {
		RenderSquare (material);
		Make464 ();

	}
	
	// Update is called once per frame
	void Update () {
		Mesh m1 = tile.GetComponent<MeshFilter> ().mesh;
		rotategraphiconmesh (m1);
		MakeTileSelection ();
		GameObject g = GameObject.Find ("Main Camera");

		if (Input.GetKey (KeyCode.A)) {
			g.transform.Rotate (Vector3.up * 30f * Time.deltaTime);

		}
		if (Input.GetKey (KeyCode.D)) {
			g.transform.Rotate (-Vector3.up * 30f * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.W)) {
			g.transform.Rotate (Vector3.left * 30f * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.S)) {
			g.transform.Rotate (-Vector3.left * 30f * Time.deltaTime);
		}

	}

	public Polygon ConstructCenterPolygon(int n, int k){

		Polygon p = new Polygon();


		float angleA = Mathf.PI / n;
		float angleB = Mathf.PI / k;
		float angleC = (float)(Mathf.PI / 2.0);

		// For a regular tiling, we need to compute the distance s
		// from A to B

		float sinA = Mathf.Sin(angleA);
		float sinB = Mathf.Sin(angleB);
		float s =
			Mathf.Sin(angleC - angleB - angleA) /
			Mathf.Sqrt(1 - sinB * sinB - sinA * sinA);

		// Determine the coordinates of the n vertices of the n-gon.
		// They're all at distance s from center of the Poincare disk


		for (int i = 0; i <= n; ++i)
		{
			p.vertices.Add(
				new Vector3(
					s * Mathf.Cos((3 + 2 * i) * angleA),0,
					s * Mathf.Sin((3 + 2 * i) * angleA)
				)
			);
		}

		return p;

	}
	public void RenderSquare(Material mat)
	{
		tile=new GameObject("100");
		Mesh msh = new Mesh();
		tile.AddComponent<MeshFilter> ();
		tile.AddComponent<MeshRenderer> ().material=mat;
		tile.AddComponent<MeshCollider> ();
		tile.tag="Player";
		a = ConstructCenterPolygon (4, 6);
		Vector3 center = CenterofPolygon (a);
		print ("center " + center);
		Polygon b = new Polygon ();
		for (int i=0; i<a.vertices.Count-1; i++) 
		{
			print ("i " + i);
			b.vertices.Add(ptok(a.vertices[i]));
			b.vertices.Add(ptok(a.vertices[i+1]));
			b.vertices.Add (ptok(center));
		}
		print ("final point" + b.vertices [b.vertices.Count-1]);
		vertices = new Vector3[b.vertices.Count];
		for (int i = 0; i < vertices.Length; i++) {
			vertices [i] = b.vertices [i];
			print ("b.verties" + b.vertices [i].x);
		}
		msh.vertices = vertices;


		int[] triangles = new int[b.vertices.Count];
		for (int i = 0; i < vertices.Length; i++) {
			triangles [i] = i;
			print ("tri[i]" + triangles [i]);
		}
		msh.triangles = triangles;
		tile.GetComponent<MeshCollider> ().sharedMesh = msh;
		Vector2[] uv = new Vector2[5];
		uv [0] = new Vector2 (0, 1);
		uv [1] = new Vector2 (1, 1);
		uv [2] = new Vector2 (1, 0);
		uv [3] = new Vector2 (0, 0);
		uv [4] = new Vector2 (0, 1);

		Polygon2D c = new Polygon2D ();

		for(int i=0;i<uv.Length-1;i++)
		{
			c.vertices.Add (uv[i]);
			c.vertices.Add (uv[i+1]);
			c.vertices.Add (new Vector2 (0.5f, 0.5f));

		}
		Vector2[] finaluvs = new Vector2[vertices.Length];
		for (int i = 0; i < finaluvs.Length; i++) {
			finaluvs [i] = c.vertices [i];
		}
		msh.uv = finaluvs;

		Vector3[] normals=new Vector3[vertices.Length];
		for (int i = 0; i < normals.Length; i++) {
			normals [i] = Vector3.down;
		}
		msh.normals = normals;
		tile.GetComponent<MeshFilter>().mesh = msh;

		tile.transform.position = new Vector3 (0f,0f,3f);

	}

	public Vector3 CenterofPolygon(Polygon findcenterofpolygon)
	{
		Vector3 center = new Vector3 (0f,0f,0f);
		for (int i = 0; i < findcenterofpolygon.vertices.Count - 1; i++) {
			center += findcenterofpolygon.vertices [i];
		}
		int sides = findcenterofpolygon.vertices.Count - 1;
		return center/sides;
	}

	public class Polygon {
		public List<Vector3>vertices = new List<Vector3>();
	}

	public class Polygon2D {
		public List<Vector2>vertices = new List<Vector2>();
	}

	public Vector2[] GraphicsOrder()
	{
		Vector2[] final= new Vector2[5];
		final [0] = new Vector2 (1, 1);
		final [1] = new Vector2 (1, 0);
		final [2] = new Vector2 (0, 0);
		final [3] = new Vector2 (0, 1);
		final [4] = new Vector2 (1, 1);

		Polygon2D c = new Polygon2D ();

		for(int i=0;i<final.Length-1;i++)
		{
			c.vertices.Add (final[i]);
			c.vertices.Add (final[i+1]);
			c.vertices.Add (new Vector2 (0.5f, 0.5f));

		}
		Vector2[] finaluvs = new Vector2[vertices.Length];
		for (int i = 0; i < finaluvs.Length; i++) {
			finaluvs [i] = c.vertices [i];
		}

		return finaluvs;
	}

	public void Make464()
	{
		tile1 =Instantiate (tile, tile.transform.position+new Vector3 (0f, 1.16f, 0f), tile.transform.rotation * Quaternion.Euler (new Vector3 (0f, 0f, 180f))) as GameObject;
		tile1.name="101";
//		tile1.AddComponent<LineRenderer> ();
//		linerenderer = tile1.GetComponent<LineRenderer> ();
//		linerenderer.material = new Material (Shader.Find ("Particles/Additive"));
//		linerenderer.SetColors (Color.red, Color.red);
//		linerenderer.SetWidth (0.02f, 0.02f);
//		Vector3[] o = pointsrotatedandtranslated (new Vector3 (0f, 1.16f, 0f),new Vector3 (0f, 0f, 180f));
//		linerenderer.SetVertexCount (o.Length);
//		for (int i = 0; i < o.Length; i++) {
//			linerenderer.SetPosition (i, o[i]);
//		}
		LineRenderer l1=new LineRenderer();
		AddLineRenderer(tile1,l1,tile1.transform.position,tile1.transform.eulerAngles);

		Mesh m1 = tile1.GetComponent<MeshFilter> ().mesh;
		rotategraphiconmesh (m1);
		tile2 =Instantiate (tile, tile.transform.position+new Vector3 (0.581f, 0.581f, 0f), tile.transform.rotation * Quaternion.Euler (new Vector3 (0f, 0f, 90f))) as GameObject;
		tile2.name="102";
		LineRenderer l2=new LineRenderer();
		AddLineRenderer(tile2,l2,tile2.transform.position,tile2.transform.eulerAngles);

		tile3 =Instantiate (tile, tile.transform.position+new Vector3 (-0.581f, 0.581f, 0f), tile.transform.rotation *  Quaternion.Euler (new Vector3 (0f, 0f, 270f))) as GameObject;
		tile3.name="103";

		tile4 =Instantiate (tile, tile.transform.position+new Vector3 (0f, 1.743f, 0.581f), tile.transform.rotation *  Quaternion.Euler (new Vector3 (90f, 0f, 0f))) as GameObject;
		tile4.name="104";
		LineRenderer l4=new LineRenderer(); 
		AddLineRenderer(tile4,l4,tile4.transform.position,tile4.transform.eulerAngles);

	    tile5 =Instantiate (tile, tile.transform.position+new Vector3 (0f, 1.743f, 1.743f), tile.transform.rotation *  Quaternion.Euler (new Vector3 (270f, 0f, 0f))) as GameObject;
		tile5.name="105";

		tile6 =Instantiate (tile, tile.transform.position+new Vector3 (-0.581f, 1.743f, 1.16f), tile.transform.rotation * Quaternion.Euler (new Vector3 (0f, 0f, 270f))) as GameObject;
		tile6.name="106";

		tile7 =Instantiate (tile, tile.transform.position+new Vector3 (0.581f, 1.743f, 1.16f), tile.transform.rotation * Quaternion.Euler (new Vector3 (0f, 0f, 90f))) as GameObject;
		tile7.name="107";
		LineRenderer l7=new LineRenderer(); 
		AddLineRenderer(tile7,l7,tile7.transform.position,tile7.transform.eulerAngles);

		tile8 =Instantiate (tile, tile.transform.position+new Vector3 (1.16f, 0f, 1.16f), tile.transform.rotation * Quaternion.Euler (new Vector3 (0f, 0f, 0f))) as GameObject;
		tile8.name="108";
		Mesh m8 = tile8.GetComponent<MeshFilter> ().mesh;
		rotategraphiconmesh (m8);

		tile9 =Instantiate (tile, tile.transform.position+new Vector3 (1.16f, 1.16f, 1.16f), tile.transform.rotation * Quaternion.Euler (new Vector3 (0f, 0f, 180f))) as GameObject;
		tile9.name="109";
		Mesh m9 = tile9.GetComponent<MeshFilter> ().mesh;
		rotategraphiconmesh (m9);
		LineRenderer l9=new LineRenderer(); 
		AddLineRenderer(tile9,l9,tile9.transform.position,tile9.transform.eulerAngles);

		tile10 =Instantiate (tile, tile.transform.position+new Vector3 (1.16f, 0.581f, 0.581f), tile.transform.rotation * Quaternion.Euler (new Vector3 (90f, 0f, 0f))) as GameObject;
		tile10.name="110";
		LineRenderer l10=new LineRenderer(); 
		AddLineRenderer(tile10,l10,tile10.transform.position,tile10.transform.eulerAngles);

		tile11 =Instantiate (tile, tile.transform.position+new Vector3 (1.16f, 0.581f, 1.743f), tile.transform.rotation * Quaternion.Euler (new Vector3 (-90f, 0f, 0f))) as GameObject;
		tile11.name="111";
	}

	public void rotategraphiconmesh(Mesh mesh1)
	{
		Vector2[] uvs = new Vector2[5];
		uvs = GraphicsOrder ();
		mesh1.uv=uvs;
	}

	public void TransformationPoint(Vector3 pointin2d)
	{
		Vector3 hyperboloidpoint = Poincaretohyperboloid (pointin2d);

	}

	public Vector3 Poincaretohyperboloid(Vector3 poincarepoint)
	{
		float x = poincarepoint.x;
		float z = poincarepoint.z;
		float divisor = (1 - x * x - z * z);
		float hyperboloid_x=2*x/divisor;
		float hyperboloid_y=(1+x*x+z*z)/divisor;
		float hyperboloid_z=2*z/divisor;
		return new Vector3 (hyperboloid_x,hyperboloid_y,hyperboloid_z);
	}

	public float[][] ConvertVector3tomatrix(Vector3 point)
	{
		float[][] matrixpoint = {
			new [] {point.x},
			new [] {point.y},
			new [] {point.z},
		};

		return matrixpoint;
	}

	public Vector3 ptok(Vector3 poincarepoint)
	{
		Vector3 kleinpoint = new Vector3 (2 * poincarepoint.x / (1 + poincarepoint.x * poincarepoint.x + poincarepoint.z * poincarepoint.z), 0,
			2 * poincarepoint.z / (1 + poincarepoint.x * poincarepoint.x + poincarepoint.z * poincarepoint.z));

		return kleinpoint;
	}

	public Vector3[] pointsrotatedandtranslated(Vector3 translation,Vector3 rotation)
	{
		Vector3[] output = new Vector3[a.vertices.Count];
		print ("a.vertice.count " + a.vertices.Count);
		Vector3[] b = new Vector3[a.vertices.Count];
		for (int i = 0; i < output.Length; i++) 
		{
			b[i] = ptok (a.vertices [i]);
			output [i] = Quaternion.Euler (rotation) * b[i] + translation;
		}
		return output;
	}

	public void AddLineRenderer(GameObject gameobject, LineRenderer linerenderer, Vector3 translation,Vector3 rotation)
	{
		gameobject.AddComponent<LineRenderer> ();
		linerenderer = gameobject.GetComponent<LineRenderer> ();
		linerenderer.material = new Material (Shader.Find ("Particles/Additive"));
		linerenderer.SetColors (Color.red, Color.red);
		linerenderer.SetWidth (0.02f, 0.02f);
		Vector3[] o = pointsrotatedandtranslated (translation,rotation);
		linerenderer.SetVertexCount (o.Length);
		for (int i = 0; i < o.Length; i++) {
			linerenderer.SetPosition (i, o[i]);
		}
	}

	public void MakeTileSelection()
	{
		//GameObject g = GameObject.Find ("Sphere-3D");
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit)) {
			if (hit.collider.name == "0") {
				defaultLineWidth ();
				GameObject.Find ("102").GetComponent<LineRenderer> ().SetWidth (0.05f, 0.05f);
			}
			else if (hit.collider.name == "5") {
				defaultLineWidth ();
				GameObject.Find ("101").GetComponent<LineRenderer> ().SetWidth (0.05f, 0.05f);
			}
			else if (hit.collider.name == "9") {
				defaultLineWidth ();
				GameObject.Find ("110").GetComponent<LineRenderer> ().SetWidth (0.05f, 0.05f);
			}
			else if (hit.collider.name == "10") {
				defaultLineWidth ();
				GameObject.Find ("109").GetComponent<LineRenderer> ().SetWidth (0.05f, 0.05f);
			}
			else if (hit.collider.name == "11") {
				defaultLineWidth ();
				GameObject.Find ("107").GetComponent<LineRenderer> ().SetWidth (0.05f, 0.05f);
			}
			else if (hit.collider.name == "12") {
				defaultLineWidth ();
				GameObject.Find ("104").GetComponent<LineRenderer> ().SetWidth (0.05f, 0.05f);
			}
		}
	}


	public void defaultLineWidth()
	{
		GameObject.Find ("101").GetComponent<LineRenderer> ().SetWidth (0.02f, 0.02f);
		GameObject.Find ("102").GetComponent<LineRenderer> ().SetWidth (0.02f, 0.02f);
		GameObject.Find ("104").GetComponent<LineRenderer> ().SetWidth (0.02f, 0.02f);
		GameObject.Find ("107").GetComponent<LineRenderer> ().SetWidth (0.02f, 0.02f);
		GameObject.Find ("109").GetComponent<LineRenderer> ().SetWidth (0.02f, 0.02f);
		GameObject.Find ("110").GetComponent<LineRenderer> ().SetWidth (0.02f, 0.02f);
	}
}


