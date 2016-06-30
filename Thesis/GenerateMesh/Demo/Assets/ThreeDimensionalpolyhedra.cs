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

	
		// Update is called once per frame
		void Update () 
		{
			
			// Enable the tile selection for rendering line renderer only in the modelling_mode
			// Also set up a few other things in this mode
			if (twodhyperbolic.modelling_mode) {
				Mesh m1 = tile.GetComponent<MeshFilter> ().mesh;
				rotategraphiconmesh (m1);
				MakeTileSelection ();
			}

			// Adding rotation to the triply periodic gameobject using WASD keys
			GameObject g = GameObject.Find ("Main Camera");
			if (Input.GetKey (KeyCode.A))
			{
				g.transform.RotateAround (new Vector3(0.581f,1.162f,9.581f),-Vector3.up ,20f * Time.deltaTime);

			}
			if (Input.GetKey (KeyCode.D)) 
			{
				g.transform.RotateAround (new Vector3(0.581f,1.162f,9.581f),Vector3.up ,20f * Time.deltaTime);
			}
			if (Input.GetKey (KeyCode.W)) 
			{
				g.transform.RotateAround (new Vector3(0.581f,1.162f,9.581f),-Vector3.forward ,20f * Time.deltaTime);
			}
			if (Input.GetKey (KeyCode.S))
			{
				g.transform.RotateAround (new Vector3(0.581f,1.162f,9.581f),Vector3.forward ,20f * Time.deltaTime);
			}
			

		}

		// This funciton is used to construct the basic face of the triply periodic polygon using 
		// {p,q} where p is represented by n and q is represented by k.
		public Polygon ConstructCenterPolygon(int n, int k){
			Polygon p = new Polygon();

			// The angles made by the fundamental polygon with the circumscribing circle.
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

		// This block creates the gameobject based on the points obtained by constructcenterpolygon function
		// and adds a material to it.
		public void RenderSquare(Material mat)
		{
			
			tile=new GameObject("1000");
			Mesh msh = new Mesh();
			tile.AddComponent<MeshFilter> ();
				

			// if else block to render random color if not in modelling mode or render angels and demons pattern
			// if in the modelling_mode.
			if (twodhyperbolic.modelling_mode) 
			{
				tile.AddComponent<MeshRenderer> ().material = mat;
			} 
			else 
			{
				Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
				tile.AddComponent<MeshRenderer> ().material.color = newcolor;
			}

			tile.AddComponent<MeshCollider> ();
			tile.tag="Player";

			// generate names for each polygon of the triply periodic polygon starting from 1000.
			int name = 1000 + number;
			tile.name = name.ToString ();

			a = ConstructCenterPolygon (4, 6);
			Vector3 center = CenterofPolygon (a);
			print ("center " + center);
			Polygon b = new Polygon ();

			// setting up the vertices
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


			// setting up the traingles
			int[] triangles = new int[b.vertices.Count];
			for (int i = 0; i < vertices.Length; i++) {
				triangles [i] = i;
				print ("tri[i]" + triangles [i]);
			}
			msh.triangles = triangles;
			tile.GetComponent<MeshCollider> ().sharedMesh = msh;

			// setting up the uvs.
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
			if (twodhyperbolic.modelling_mode) 
			{
					msh.uv = finaluvs;
			}

			// setting up the normals
			Vector3[] normals=new Vector3[vertices.Length];
			for (int i = 0; i < normals.Length; i++) {
				normals [i] = Vector3.down;
			}
			msh.normals = normals;
			tile.GetComponent<MeshFilter>().mesh = msh;

			// move the triply periodic polyhedron away from the hyperbolic tessellation.
			tile.transform.position = new Vector3 (0f,0f,9f);

		}


		// This function finds the center of the polygon..
		// The method is to divide the sum of all vertices by the total
		// number of sides.
		public Vector3 CenterofPolygon(Polygon findcenterofpolygon)
		{
			Vector3 center = new Vector3 (0f,0f,0f);
			for (int i = 0; i < findcenterofpolygon.vertices.Count - 1; i++) {
				center += findcenterofpolygon.vertices [i];
			}
			int sides = findcenterofpolygon.vertices.Count - 1;
			return center/sides;
		}


		// A class with a list of vector3's to store vertices..
		public class Polygon {
			public List<Vector3>vertices = new List<Vector3>();
		}


		// A class with a list of vector2's to store uvs...
		public class Polygon2D {
			public List<Vector2>vertices = new List<Vector2>();
		}


		// This determines the order in which uvs are rendered...
		// This has to be modified if some other type of triply periodic polyhedron 
		// is selected....This can be automatically setup by some GUI mechansim
		// which can be added....
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

		// This function pick up the basic polygon created and replicates
		// it and creates a block of the {4,6|4} triply periodic polyhedron..
		public void Make464(Vector3 translation)
		{
			int name;
			if (translation.x != 0 && translation.y != 0 && translation.z != 0) {
				GameObject tile0 = Instantiate (tile, tile.transform.position + translation, Quaternion.Euler (new Vector3 (0f, 0f, 0f))) as GameObject;
				number++;
				name = 1000 + number;
				tile0.name=name.ToString();
			}
			tile1 =Instantiate (tile, tile.transform.position+new Vector3 (0f, 1.16f, 0f)+translation, tile.transform.rotation * Quaternion.Euler (new Vector3 (0f, 0f, 180f))) as GameObject;
			number++;
			name = 1000 + number;
			tile1.name=name.ToString();
			if (!twodhyperbolic.modelling_mode) {
				Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
				tile1.GetComponent<MeshRenderer> ().material.color = newcolor;
			}

			LineRenderer l1=new LineRenderer();
			AddLineRenderer(tile1,l1,tile1.transform.position,tile1.transform.eulerAngles);

			Mesh m1 = tile1.GetComponent<MeshFilter> ().mesh;
			rotategraphiconmesh (m1);
			tile2 =Instantiate (tile, tile.transform.position+new Vector3 (0.581f, 0.581f, 0f)+translation, tile.transform.rotation * Quaternion.Euler (new Vector3 (0f, 0f, 90f))) as GameObject;
			number++;
			name = 1000 + number;
			tile2.name=name.ToString();
			LineRenderer l2=new LineRenderer();
			AddLineRenderer(tile2,l2,tile2.transform.position,tile2.transform.eulerAngles);
			if (!twodhyperbolic.modelling_mode) {
				Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
				tile2.GetComponent<MeshRenderer> ().material.color = newcolor;
			}

			tile3 =Instantiate (tile, tile.transform.position+new Vector3 (-0.581f, 0.581f, 0f)+translation, tile.transform.rotation *  Quaternion.Euler (new Vector3 (0f, 0f, 270f))) as GameObject;
			number++;
			name = 1000 + number;
			tile3.name=name.ToString();
			if (!twodhyperbolic.modelling_mode) {
				Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
				tile3.GetComponent<MeshRenderer> ().material.color = newcolor;
			}

			tile4 =Instantiate (tile, tile.transform.position+new Vector3 (0f, 1.743f, 0.581f)+translation, tile.transform.rotation *  Quaternion.Euler (new Vector3 (90f, 0f, 0f))) as GameObject;
			number++;
			name = 1000 + number;
			tile4.name=name.ToString();
			LineRenderer l4=new LineRenderer(); 
			AddLineRenderer(tile4,l4,tile4.transform.position,tile4.transform.eulerAngles);
			if (!twodhyperbolic.modelling_mode) {
				Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
				tile4.GetComponent<MeshRenderer> ().material.color = newcolor;
			}

			tile5 =Instantiate (tile, tile.transform.position+new Vector3 (0f, 1.743f, 1.743f)+translation, tile.transform.rotation *  Quaternion.Euler (new Vector3 (270f, 0f, 0f))) as GameObject;
			number++;
			name = 1000 + number;
			tile5.name=name.ToString();
			if (!twodhyperbolic.modelling_mode) {
				Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
				tile5.GetComponent<MeshRenderer> ().material.color = newcolor;
			}
		
			tile6 =Instantiate (tile, tile.transform.position+new Vector3 (-0.581f, 1.743f, 1.16f)+translation, tile.transform.rotation * Quaternion.Euler (new Vector3 (0f, 0f, 270f))) as GameObject;
			number++;
			name = 1000 + number;
			tile6.name=name.ToString();
			if (!twodhyperbolic.modelling_mode) {
				Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
				tile6.GetComponent<MeshRenderer> ().material.color = newcolor;
			}

			tile7 =Instantiate (tile, tile.transform.position+new Vector3 (0.581f, 1.743f, 1.16f)+translation, tile.transform.rotation * Quaternion.Euler (new Vector3 (0f, 0f, 90f))) as GameObject;
			number++;
			name = 1000 + number;
			tile7.name=name.ToString();
			LineRenderer l7=new LineRenderer(); 
			AddLineRenderer(tile7,l7,tile7.transform.position,tile7.transform.eulerAngles);
			if (!twodhyperbolic.modelling_mode) {
				Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
				tile7.GetComponent<MeshRenderer> ().material.color = newcolor;
			}

			tile8 =Instantiate (tile, tile.transform.position+new Vector3 (1.16f, 0f, 1.16f)+translation, tile.transform.rotation * Quaternion.Euler (new Vector3 (0f, 0f, 0f))) as GameObject;
			number++;
			name = 1000 + number;
			tile8.name=name.ToString();
			Mesh m8 = tile8.GetComponent<MeshFilter> ().mesh;
			rotategraphiconmesh (m8);
			if (!twodhyperbolic.modelling_mode) {
				Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
				tile8.GetComponent<MeshRenderer> ().material.color = newcolor;
			}

			tile9 =Instantiate (tile, tile.transform.position+new Vector3 (1.16f, 1.16f, 1.16f)+translation, tile.transform.rotation * Quaternion.Euler (new Vector3 (0f, 0f, 180f))) as GameObject;
			number++;
			name = 1000 + number;
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
			name = 1000 + number;
			tile10.name=name.ToString();
			LineRenderer l10=new LineRenderer(); 
			AddLineRenderer(tile10,l10,tile10.transform.position,tile10.transform.eulerAngles);
			if (!twodhyperbolic.modelling_mode) {
				Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
				tile10.GetComponent<MeshRenderer> ().material.color = newcolor;
			}


			tile11 =Instantiate (tile, tile.transform.position+new Vector3 (1.16f, 0.581f, 1.743f)+translation, tile.transform.rotation * Quaternion.Euler (new Vector3 (-90f, 0f, 0f))) as GameObject;
			number++;
			name = 1000 + number;
			tile11.name=name.ToString();
			if (!twodhyperbolic.modelling_mode) {
				Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
				tile11.GetComponent<MeshRenderer> ().material.color = newcolor;
			}
		}


		// Allows to rotate the angels and deomns pattern on a polygon...
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

		// Function to convert poincare point to hyperboloid model point.
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

		// Converts a vector3 to matrix...
		public float[][] ConvertVector3tomatrix(Vector3 point)
		{
			float[][] matrixpoint = {
				new [] {point.x},
				new [] {point.y},
				new [] {point.z},
			};

			return matrixpoint;
		}


		// This function converts a point from poincare model to Klein model.
		public Vector3 ptok(Vector3 poincarepoint)
		{
			Vector3 kleinpoint = new Vector3 (2 * poincarepoint.x / (1 + poincarepoint.x * poincarepoint.x + poincarepoint.z * poincarepoint.z), 0,
				2 * poincarepoint.z / (1 + poincarepoint.x * poincarepoint.x + poincarepoint.z * poincarepoint.z));

			return kleinpoint;
		}

		// This function takes in a translation and rotation vector and applies it
		// to a set of vertices...
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


		// This function allows to add a linerenderer to a gameobject that can be specified as an input
		// argument.
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

		// This function allows the linerenderer mechanism as a part
		// of the modelling mode.
		// Based on the position of the mouse in the triply periodic polyhedron
		// the line renderer's width changes...
		// The corresponding tile in the hyperbolic tessellation is also highlighted..
		public void MakeTileSelection()
		{
			//GameObject g = GameObject.Find ("Sphere-3D");
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.name == "0") {
					defaultLineWidth ();
					GameObject.Find ("1002").GetComponent<LineRenderer> ().SetWidth (0.05f, 0.05f);
				}
				else if (hit.collider.name == "5") {
					defaultLineWidth ();
					GameObject.Find ("1001").GetComponent<LineRenderer> ().SetWidth (0.05f, 0.05f);
				}
				else if (hit.collider.name == "9") {
					defaultLineWidth ();
					GameObject.Find ("1010").GetComponent<LineRenderer> ().SetWidth (0.05f, 0.05f);
				}
				else if (hit.collider.name == "10") {
					defaultLineWidth ();
					GameObject.Find ("1009").GetComponent<LineRenderer> ().SetWidth (0.05f, 0.05f);
				}
				else if (hit.collider.name == "11") {
					defaultLineWidth ();
					GameObject.Find ("1007").GetComponent<LineRenderer> ().SetWidth (0.05f, 0.05f);
				}
				else if (hit.collider.name == "12") {
					defaultLineWidth ();
					GameObject.Find ("1004").GetComponent<LineRenderer> ().SetWidth (0.05f, 0.05f);
				}
			}
		}


		// Changes the linerenderer's width to default...
		public void defaultLineWidth()
		{
			GameObject.Find ("1001").GetComponent<LineRenderer> ().SetWidth (0.02f, 0.02f);
			GameObject.Find ("1002").GetComponent<LineRenderer> ().SetWidth (0.02f, 0.02f);
			GameObject.Find ("1004").GetComponent<LineRenderer> ().SetWidth (0.02f, 0.02f);
			GameObject.Find ("1007").GetComponent<LineRenderer> ().SetWidth (0.02f, 0.02f);
			GameObject.Find ("1009").GetComponent<LineRenderer> ().SetWidth (0.02f, 0.02f);
			GameObject.Find ("1010").GetComponent<LineRenderer> ().SetWidth (0.02f, 0.02f);
		}


		// When the {4,6|4} button is clicked in the triply_periodic mode
		// this funciton kicks in.
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

		// When the {6,6|3} button is clicked in the triply_periodic mode
		// this funciton kicks in.
		public void b_663_click()
		{
			DestroyTriplyGameObjects ();
			RenderHexagon ();
			number = 0;
			Make663_block (new Vector3(0,0,0));
			Make663_block (new Vector3(-1.7f,0,0.9f));
			Make663_block (new Vector3(0,0,1.9f));
			Make663_block (new Vector3(-0.54f,1.5f,0.93f));
			tile.SetActive (false);
		}

		// Sets up the initial setup for the triply_periodic_mode...
		public void model_Triply_setup()
		{
			number = 0;
			RenderSquare (material);
			Make464 (new Vector3(0,0,0));
		}

		// Destroys pervious gameobjects..
		public void DestroyTriplyGameObjects()
		{

			for (int i = 1000; i < 2000; i++) {
				GameObject g = GameObject.Find (i.ToString ());
				Destroy (g);
			}

		}


		// This funciton creates the hexagon for the {6,6|3} polyhedron..
		// It uses the ConstructCenterPolygon using values 6,6 and
		// automatically comes up with the vertices...
		// This block also sets up the uvs and the normals..
		public void RenderHexagon()
		{
			tile=new GameObject("1000");
			Mesh msh = new Mesh();
			tile.AddComponent<MeshFilter> ();
			tile.tag="Player";
			int name = 1000 + number;
			tile.transform.position +=new Vector3(1.21f,1.52f,9f);
			tile.transform.rotation *= Quaternion.Euler (new Vector3 (0, 0, 180f));
			tile.name = name.ToString ();
			a = ConstructCenterPolygon (6, 6);
			Vector3 center = CenterofPolygon (a);
			print ("center " + center);
			Polygon b = new Polygon ();

			// setup vertices...
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


			//setup triangles...
			int[] triangles = new int[b.vertices.Count];
			for (int i = 0; i < vertices.Length; i++) {
				triangles [i] = i;
			}
			msh.triangles = triangles;
			tile.GetComponent<MeshFilter> ().mesh = msh;
			Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
			tile.AddComponent<MeshRenderer> ().material.color = newcolor;


			// setup normals...
			Vector3[] normals=new Vector3[vertices.Length];
			for (int i = 0; i < normals.Length; i++) {
				normals [i] = Vector3.up;
			}
			msh.normals = normals;
		}

		// Replicate the basic hexagon to create a {6,6|3} block....
		public void Make663_block(Vector3 translate)
		{
			int name;
			GameObject tile0 = Instantiate (tile, tile.transform.position+new Vector3 (0f, 0f, 0f)+translate, Quaternion.Euler (new Vector3 (0f, 0f, 180f))) as GameObject;
			number++;
			name = 1000 + number;
			tile0.name=name.ToString();
			LineRenderer l0=new LineRenderer();
			//AddLineRenderer(tile,l0,tile.transform.position,tile.transform.eulerAngles);
			if (!twodhyperbolic.modelling_mode) {
				Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
				tile0.GetComponent<MeshRenderer> ().material.color = newcolor;
			}
			tile1 =Instantiate (tile, new Vector3 (0.65f, 0.76f, 9f)+translate, Quaternion.Euler (new Vector3 (0f, 0f, 290f))) as GameObject;
			number++;
			name = 1000 + number;
			tile1.name=name.ToString();
			LineRenderer l1=new LineRenderer();
			//AddLineRenderer(tile1,l1,tile1.transform.position,tile1.transform.eulerAngles);
			if (!twodhyperbolic.modelling_mode) {
				Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
				tile1.GetComponent<MeshRenderer> ().material.color = newcolor;
			}

			tile2 =Instantiate (tile, new Vector3 (1.495f, 0.758f, 9.46f)+translate, Quaternion.Euler (new Vector3 (0f, 120f, 290f))) as GameObject;
			number++;
			name = 1000 + number;
			tile2.name=name.ToString();
			LineRenderer l2=new LineRenderer();
			//AddLineRenderer(tile2,l2,tile2.transform.position,tile2.transform.eulerAngles);
			if (!twodhyperbolic.modelling_mode) {
				Color newcolor = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
				tile2.GetComponent<MeshRenderer> ().material.color = newcolor;
			}

			tile3 =Instantiate (tile, new Vector3 (1.465f, 0.769f, 8.514f)+translate, Quaternion.Euler (new Vector3 (358.7f, 240f, 290f))) as GameObject;
			number++;
			name = 1000 + number;
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
