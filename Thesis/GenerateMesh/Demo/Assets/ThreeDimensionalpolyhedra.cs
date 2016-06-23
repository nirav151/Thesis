using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HyperbolicGeometry{
	
public class ThreeDimensionalpolyhedra : MonoBehaviour 
	{

		public Material material;
		GameObject tile,tile1,tile2,tile3,tile4,tile5,tile6,tile7,tile8,tile9,tile10,tile11;
		Vector3[] vertices;
		LineRenderer linerenderer;
		LineRenderer linerenderer2;
		Polygon a;
		Ray ray;
		RaycastHit hit;
		int number=0;
		// Use this for initialization
		void Start () 
		{


		}
	
	// Update is called once per frame
		void Update () {

			if (twodhyperbolic.modelling_mode) {
				Mesh m1 = tile.GetComponent<MeshFilter> ().mesh;
				rotategraphiconmesh (m1);
				MakeTileSelection ();
			}
					GameObject g = GameObject.Find ("Main Camera");

					if (Input.GetKey (KeyCode.A)) {
						g.transform.RotateAround (new Vector3(0.581f,1.162f,9.581f),-Vector3.up ,20f * Time.deltaTime);

					}
					if (Input.GetKey (KeyCode.D)) {
						g.transform.RotateAround (new Vector3(0.581f,1.162f,9.581f),Vector3.up ,20f * Time.deltaTime);
					}
					if (Input.GetKey (KeyCode.W)) {
						g.transform.RotateAround (new Vector3(0.581f,1.162f,9.581f),-Vector3.forward ,20f * Time.deltaTime);
					}
					if (Input.GetKey (KeyCode.S)) {
						g.transform.RotateAround (new Vector3(0.581f,1.162f,9.581f),Vector3.forward ,20f * Time.deltaTime);
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
				if (twodhyperbolic.modelling_mode) {
					tile.AddComponent<MeshRenderer> ().material = mat;
				} else {
					Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
					tile.AddComponent<MeshRenderer> ().material.color = newcolor;
				}
			tile.AddComponent<MeshCollider> ();
			tile.tag="Player";
			int name = 100 + number;
			tile.name = name.ToString ();
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
				if (twodhyperbolic.modelling_mode) {
					msh.uv = finaluvs;
				}
			Vector3[] normals=new Vector3[vertices.Length];
			for (int i = 0; i < normals.Length; i++) {
				normals [i] = Vector3.down;
			}
			msh.normals = normals;
			tile.GetComponent<MeshFilter>().mesh = msh;

			tile.transform.position = new Vector3 (0f,0f,9f);

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

		public void Make464(Vector3 translation)
		{
			int name;
			if (translation.x != 0 && translation.y != 0 && translation.z != 0) {
				GameObject tile0 = Instantiate (tile, tile.transform.position + translation, Quaternion.Euler (new Vector3 (0f, 0f, 0f))) as GameObject;
				number++;
				name = 100 + number;
				tile0.name=name.ToString();
			}
			tile1 =Instantiate (tile, tile.transform.position+new Vector3 (0f, 1.16f, 0f)+translation, tile.transform.rotation * Quaternion.Euler (new Vector3 (0f, 0f, 180f))) as GameObject;
			number++;
			name = 100 + number;
			tile1.name=name.ToString();
			if (!twodhyperbolic.modelling_mode) {
				Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
				tile1.GetComponent<MeshRenderer> ().material.color = newcolor;
			}
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
			tile2 =Instantiate (tile, tile.transform.position+new Vector3 (0.581f, 0.581f, 0f)+translation, tile.transform.rotation * Quaternion.Euler (new Vector3 (0f, 0f, 90f))) as GameObject;
			number++;
			name = 100 + number;
			tile2.name=name.ToString();
			LineRenderer l2=new LineRenderer();
			AddLineRenderer(tile2,l2,tile2.transform.position,tile2.transform.eulerAngles);
			if (!twodhyperbolic.modelling_mode) {
				Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
				tile2.GetComponent<MeshRenderer> ().material.color = newcolor;
			}

			tile3 =Instantiate (tile, tile.transform.position+new Vector3 (-0.581f, 0.581f, 0f)+translation, tile.transform.rotation *  Quaternion.Euler (new Vector3 (0f, 0f, 270f))) as GameObject;
			number++;
			name = 100 + number;
			tile3.name=name.ToString();
			if (!twodhyperbolic.modelling_mode) {
				Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
				tile3.GetComponent<MeshRenderer> ().material.color = newcolor;
			}

			tile4 =Instantiate (tile, tile.transform.position+new Vector3 (0f, 1.743f, 0.581f)+translation, tile.transform.rotation *  Quaternion.Euler (new Vector3 (90f, 0f, 0f))) as GameObject;
			number++;
			name = 100 + number;
			tile4.name=name.ToString();
			LineRenderer l4=new LineRenderer(); 
			AddLineRenderer(tile4,l4,tile4.transform.position,tile4.transform.eulerAngles);
			if (!twodhyperbolic.modelling_mode) {
				Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
				tile4.GetComponent<MeshRenderer> ().material.color = newcolor;
			}

			tile5 =Instantiate (tile, tile.transform.position+new Vector3 (0f, 1.743f, 1.743f)+translation, tile.transform.rotation *  Quaternion.Euler (new Vector3 (270f, 0f, 0f))) as GameObject;
			number++;
			name = 100 + number;
			tile5.name=name.ToString();
			if (!twodhyperbolic.modelling_mode) {
				Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
				tile5.GetComponent<MeshRenderer> ().material.color = newcolor;
			}
		
			tile6 =Instantiate (tile, tile.transform.position+new Vector3 (-0.581f, 1.743f, 1.16f)+translation, tile.transform.rotation * Quaternion.Euler (new Vector3 (0f, 0f, 270f))) as GameObject;
			number++;
			name = 100 + number;
			tile6.name=name.ToString();
			if (!twodhyperbolic.modelling_mode) {
				Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
				tile6.GetComponent<MeshRenderer> ().material.color = newcolor;
			}

			tile7 =Instantiate (tile, tile.transform.position+new Vector3 (0.581f, 1.743f, 1.16f)+translation, tile.transform.rotation * Quaternion.Euler (new Vector3 (0f, 0f, 90f))) as GameObject;
			number++;
			name = 100 + number;
			tile7.name=name.ToString();
			LineRenderer l7=new LineRenderer(); 
			AddLineRenderer(tile7,l7,tile7.transform.position,tile7.transform.eulerAngles);
			if (!twodhyperbolic.modelling_mode) {
				Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
				tile7.GetComponent<MeshRenderer> ().material.color = newcolor;
			}

			tile8 =Instantiate (tile, tile.transform.position+new Vector3 (1.16f, 0f, 1.16f)+translation, tile.transform.rotation * Quaternion.Euler (new Vector3 (0f, 0f, 0f))) as GameObject;
			number++;
			name = 100 + number;
			tile8.name=name.ToString();
			Mesh m8 = tile8.GetComponent<MeshFilter> ().mesh;
			rotategraphiconmesh (m8);
			if (!twodhyperbolic.modelling_mode) {
				Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
				tile8.GetComponent<MeshRenderer> ().material.color = newcolor;
			}

			tile9 =Instantiate (tile, tile.transform.position+new Vector3 (1.16f, 1.16f, 1.16f)+translation, tile.transform.rotation * Quaternion.Euler (new Vector3 (0f, 0f, 180f))) as GameObject;
			number++;
			name = 100 + number;
			tile9.name=name.ToString();
			Mesh m9 = tile9.GetComponent<MeshFilter> ().mesh;
			rotategraphiconmesh (m9);
			LineRenderer l9=new LineRenderer(); 
			AddLineRenderer(tile9,l9,tile9.transform.position,tile9.transform.eulerAngles);
			if (!twodhyperbolic.modelling_mode) {
				Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
				tile9.GetComponent<MeshRenderer> ().material.color = newcolor;
			}
		
			tile10 =Instantiate (tile, tile.transform.position+new Vector3 (1.16f, 0.581f, 0.581f)+translation, tile.transform.rotation * Quaternion.Euler (new Vector3 (90f, 0f, 0f))) as GameObject;
			number++;
			name = 100 + number;
			tile10.name=name.ToString();
			LineRenderer l10=new LineRenderer(); 
			AddLineRenderer(tile10,l10,tile10.transform.position,tile10.transform.eulerAngles);
			if (!twodhyperbolic.modelling_mode) {
				Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
				tile10.GetComponent<MeshRenderer> ().material.color = newcolor;
			}
			tile11 =Instantiate (tile, tile.transform.position+new Vector3 (1.16f, 0.581f, 1.743f)+translation, tile.transform.rotation * Quaternion.Euler (new Vector3 (-90f, 0f, 0f))) as GameObject;
			number++;
			name = 100 + number;
			tile11.name=name.ToString();

			if (!twodhyperbolic.modelling_mode) {
				Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
				tile11.GetComponent<MeshRenderer> ().material.color = newcolor;
			}
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


		public void b_464_click()
		{
			DestroyTriplyGameObjects ();
			RenderSquare (material);
			number = 0;
			Make464 (new Vector3(0,0,0));
			Make464 (new Vector3 (0, 0, 2.32f));
			Make464 (new Vector3 (2.32f, 0, 2.32f));
			Make464 (new Vector3 (2.32f, 0, 0));
		}
		public void b_644_click()
		{
		}
		public void b_663_click()
		{
			DestroyTriplyGameObjects ();
			RenderHexagon ();
			Make663_block (new Vector3(0,0,0));
			Make663_block (new Vector3(-1.7f,0,0.9f));
			Make663_block (new Vector3(0,0,1.9f));
			Make663_block (new Vector3(-0.54f,1.5f,0.93f));
			tile.SetActive (false);
		}
		public void model_Triply_setup()
		{
			RenderSquare (material);
			Make464 (new Vector3(0,0,0));
		}

		public void DestroyTriplyGameObjects()
		{

			for (int i = 100; i < 200; i++) {
				GameObject g = GameObject.Find (i.ToString ());
				Destroy (g);
			}

		}

		public void RenderHexagon()
		{
			tile=new GameObject("100");
			Mesh msh = new Mesh();
			tile.AddComponent<MeshFilter> ();
			tile.tag="Player";
			int name = 100 + number;
			tile.transform.position +=new Vector3(1.21f,1.52f,9f);
			tile.transform.rotation *= Quaternion.Euler (new Vector3 (0, 0, 180f));
			tile.name = name.ToString ();
			a = ConstructCenterPolygon (6, 6);
			Vector3 center = CenterofPolygon (a);
			print ("center " + center);
			Polygon b = new Polygon ();
			for (int i=0; i<a.vertices.Count-1; i++) 
			{
				b.vertices.Add(ptok(a.vertices[i]));
				print ("ptok " + ptok (a.vertices [i]).x + " "+ptok (a.vertices [i]).z) ;
				print("first vertice"+ptok(a.vertices[i]));
				b.vertices.Add(ptok(a.vertices[i+1]));
				print("second vertice"+ptok(a.vertices[i+1]));
				b.vertices.Add (ptok(center));
			}
			vertices = new Vector3[b.vertices.Count];
			for (int i = 0; i < vertices.Length; i++) {
				vertices [i] = b.vertices [i];
			}
			msh.vertices = vertices;


			int[] triangles = new int[b.vertices.Count];
			for (int i = 0; i < vertices.Length; i++) {
				triangles [i] = i;
			}
			msh.triangles = triangles;
			tile.GetComponent<MeshFilter> ().mesh = msh;
			Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
			tile.AddComponent<MeshRenderer> ().material.color = newcolor;
			Vector3[] normals=new Vector3[vertices.Length];
			for (int i = 0; i < normals.Length; i++) {
				normals [i] = Vector3.up;
			}
			msh.normals = normals;
		}
		public void Make663_block(Vector3 translate)
		{
			int name;
			GameObject tile0 = Instantiate (tile, tile.transform.position+new Vector3 (0f, 0f, 0f)+translate, Quaternion.Euler (new Vector3 (0f, 0f, 180f))) as GameObject;
			number++;
			name = 100 + number;
			tile0.name=name.ToString();
			LineRenderer l0=new LineRenderer();
			//AddLineRenderer(tile,l0,tile.transform.position,tile.transform.eulerAngles);
			if (!twodhyperbolic.modelling_mode) {
				Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
				tile0.GetComponent<MeshRenderer> ().material.color = newcolor;
			}
			tile1 =Instantiate (tile, new Vector3 (0.65f, 0.76f, 9f)+translate, Quaternion.Euler (new Vector3 (0f, 0f, 290f))) as GameObject;
			number++;
			name = 100 + number;
			tile1.name=name.ToString();
			LineRenderer l1=new LineRenderer();
			//AddLineRenderer(tile1,l1,tile1.transform.position,tile1.transform.eulerAngles);
			if (!twodhyperbolic.modelling_mode) {
				Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
				tile1.GetComponent<MeshRenderer> ().material.color = newcolor;
			}

			tile2 =Instantiate (tile, new Vector3 (1.495f, 0.758f, 9.46f)+translate, Quaternion.Euler (new Vector3 (0f, 120f, 290f))) as GameObject;
			number++;
			name = 100 + number;
			tile2.name=name.ToString();
			LineRenderer l2=new LineRenderer();
			//AddLineRenderer(tile2,l2,tile2.transform.position,tile2.transform.eulerAngles);
			if (!twodhyperbolic.modelling_mode) {
				Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
				tile2.GetComponent<MeshRenderer> ().material.color = newcolor;
			}

			tile3 =Instantiate (tile, new Vector3 (1.465f, 0.769f, 8.514f)+translate, Quaternion.Euler (new Vector3 (358.7f, 240f, 290f))) as GameObject;
			number++;
			name = 100 + number;
			tile3.name=name.ToString();
			LineRenderer l3=new LineRenderer();
			//AddLineRenderer(tile3,l3,tile3.transform.position,tile3.transform.eulerAngles);
			if (!twodhyperbolic.modelling_mode) {
				Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
				tile3.GetComponent<MeshRenderer> ().material.color = newcolor;
			}
		}
	}
}
