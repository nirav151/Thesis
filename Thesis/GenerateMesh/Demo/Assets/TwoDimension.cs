//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//
//
//public class TwoDimension : MonoBehaviour {
//	public Material faceMaterial;
//	Polygon[] verti;
//	int[] rules;
//	public int sides=20;
//	public int polygons_at_vertice=3;
//	public int max =1000, numberOfLayers=5;
//
//	public int maxPolygons, layerPolygons;
//
//
//	void Start(){
//
//
//
//		do {
//			polygonCount (numberOfLayers, sides, polygons_at_vertice, max);
//			if (maxPolygons > max)
//				numberOfLayers--;
//
//		} while(maxPolygons>max);
//		verti = new Polygon[maxPolygons];
//		rules = new int[maxPolygons];
//
//		verti[0]= ConstructCenterPolygon (sides, polygons_at_vertice);
//		Debug.Log ("verti" + verti [0]);
//		for (int loop = 0; loop < verti [0].vertices.Count - 1; loop++) {
//			print (verti [0].vertices[loop]);
//		}
//		rules [0] = 0;
//		int j = 1;
//		int lengthofLineRenderer = verti [0].vertices.Count;
//		LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
//		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
//		lineRenderer.SetColors(Color.yellow, Color.yellow);
//		lineRenderer.SetWidth (0.01f, 0.01f);
//		lineRenderer.SetVertexCount(lengthofLineRenderer);
//		int k = 0;
//		while (k<lengthofLineRenderer) {
//			Vector3 pos = new Vector3(verti [0].vertices[k].x,0,verti [0].vertices[k].z);
//			lineRenderer.SetPosition(k,pos);
//			k++;
//		}
//
//		/*for (int i =0; i<layerPolygons; i++) {
//			j=RuleApply(i,j,sides, polygons_at_vertice);
//		
//		}*/
//
//
//		//print (verti[1]);
//		/*for (int p=0; p<verti.Length-1; p++) {
//			Vector2[] vertices2D = new Vector2[verti[p].vertices.Count];
//			for(int s=0;s<verti[p].vertices.Count;s++){
//				vertices2D[s]=new Vector2(verti[p].vertices[s].x,verti[p].vertices[s].z);
//			}
//		
//			//print(verti[p].vertices[0]);
//			Triangulator tr = new Triangulator (vertices2D);
//			int[] indices = tr.Triangulate ();
//			// Create the Vector3 vertices
//			Vector3[] vertices = new Vector3[vertices2D.Length];
//			for (int i=0; i<vertices.Length; i++) {
//				vertices [i] = new Vector3 (vertices2D [i].x, vertices2D [i].y, 0);
//			}
//			GameObject tile = new GameObject("Tile");
//			// Create the mesh
//			//Mesh msh = new Mesh ();
//			Mesh msh = new Mesh();
//			tile.AddComponent<MeshFilter> ();
//			Color newColor = new Color( Random.value, Random.value, Random.value, 1.0f );
//			tile.AddComponent<MeshRenderer>().material.color = newColor;
//			msh.vertices = vertices;
//			msh.triangles = indices;
//			msh.RecalculateNormals ();
//			msh.RecalculateBounds ();
//			tile.GetComponent<MeshFilter>().mesh = msh;
//		
//			// Set up game object with mesh;
//			//MeshFilter filter = gameObject.AddComponent (typeof(MeshFilter)) as MeshFilter;
//			//msh.Clear();
//		}*/
//
//
//
//
//	}
//
//	private void polygonCount(int layernumber,int side, int vertex, int maxValue){
//
//		maxPolygons = 1;
//		layerPolygons = 0;
//
//		int p = side * (vertex - 3);
//		int q = side;
//
//		int nextP, nextQ;
//		for (int i=1; i<=layernumber; ++i) {
//			layerPolygons=maxPolygons;
//			if(vertex == 3){
//				nextP=p+q;
//				nextQ=(side-6)*p+(side-5)*q;
//
//			}
//			else{
//				nextP=((side-2)*(vertex-3)-1)*p+((side-3)*(vertex-3)-1)*q;
//				nextQ=(side-2)*p+(side-3)*q;
//
//
//			}
//			maxPolygons+=p+q;
//			if(maxPolygons>max)
//				break;
//			p=nextP;
//			q=nextQ;
//
//		}
//
//
//	}
//
//
//	/*public Polygon ConstructCenterPolygon(int n, int k){
//	
//		Polygon p = new Polygon();
//		float angleA = Mathf.PI / n;
//		float angleB = Mathf.PI / k;
//		float angleC = (float)(Mathf.PI / 2.0);
//		
//		// For a regular tiling, we need to compute the distance s
//		// from A to B
//		
//		float sinA = Mathf.Sin(angleA);
//		float sinB = Mathf.Sin(angleB);
//		float s =
//			Mathf.Sin(angleC - angleB - angleA) /
//				Mathf.Sqrt(1 - sinB * sinB - sinA * sinA);
//		
//		// Determine the coordinates of the n vertices of the n-gon.
//		// They're all at distance s from center of the Poincare disk
//		
//		for (int i = 0; i < n; ++i)
//		{
//			p.vertices.Add(
//				new Vector3(
//				s * Mathf.Cos((3 + 2 * i) * angleA),0,
//				s * Mathf.Sin((3 + 2 * i) * angleA)
//				)
//				);
//		}
//		return p;
//	}*/
//	public Polygon ConstructCenterPolygon(int n, int k){
//
//		Polygon p = new Polygon();
//		Polygon q = new Polygon ();
//		Polygon r = new Polygon ();
//		Polygon z = new Polygon ();
//
//		float angleA = Mathf.PI / n;
//		float angleB = Mathf.PI / k;
//		float angleC = (float)(Mathf.PI / 2.0);
//
//		// For a regular tiling, we need to compute the distance s
//		// from A to B
//
//		float sinA = Mathf.Sin(angleA);
//		float sinB = Mathf.Sin(angleB);
//		float s =
//			Mathf.Sin(angleC - angleB - angleA) /
//			Mathf.Sqrt(1 - sinB * sinB - sinA * sinA);
//
//		// Determine the coordinates of the n vertices of the n-gon.
//		// They're all at distance s from center of the Poincare disk
//
//
//		for (int i = 0; i < n; ++i)
//		{
//			p.vertices.Add(
//				new Vector3(
//					s * Mathf.Cos((3 + 2 * i) * angleA),0,
//					s * Mathf.Sin((3 + 2 * i) * angleA)
//				)
//			);
//		}
//		//q = ptok (p);
//		//r = subdivide (q,10);
//		//z = ktop (r);
//		/*foreach(Vector3 b in r.vertices){
//			print (b);
//		}*/
//		return p;
//
//	}
//	public Polygon ptok (Polygon p){
//		Polygon q = new Polygon ();
//		foreach (Vector3 a in p.vertices) {
//			q.vertices.Add(
//				new Vector3(
//					2*a.x/(1+a.x*a.x+a.z*a.z),0,
//					2*a.z/(1+a.x*a.x+a.z*a.z)
//
//				)
//			);
//
//		}
//		return q;
//	}
//
//	public Polygon ktop(Polygon q){
//		Polygon r = new Polygon ();
//		foreach (Vector3 b in q.vertices) {
//			r.vertices.Add (
//				new Vector3(
//					b.x/(1+Mathf.Sqrt(1-b.x*b.x-b.z*b.z)),0,
//					b.z/(1+Mathf.Sqrt(1-b.x*b.x-b.z*b.z))
//
//				)
//			);
//
//
//		}
//		return r;
//	}
//
//	public Polygon subdivide(Polygon q,int numberofpoints)
//	{
//		Polygon r = new Polygon ();
//		for (int i=0; i<q.vertices.Count-1; i++) 
//		{
//			Vector3 end1=q.vertices[i];
//			Vector3 end2=q.vertices[i+1];
//			float dx=end2.x-end1.x;
//			float dz=end2.z-end1.z;
//			float stepx = dx / numberofpoints;
//			float stepz = dz / numberofpoints;
//			float px = end1.x + stepx;
//			float pz = end1.z + stepz;
//			print ("End1 " + end1);
//			print ("End2 " + end2);
//
//			for (int ix = 0; ix < numberofpoints; ix++)
//			{
//				Vector3 point=new Vector3(px,0,pz);
//				r.vertices.Add(point);
//				px = px + stepx;
//				pz = px + stepz;
//				print ("Points " + point);
//				//result.Add(new Point(px, py));
//				//px = px + stepx;
//				//pz = pz + stepz;
//			}
//		}
//		return r;
//	}
//	public int RuleApply(int i, int j, int n, int k ){
//
//		int r = rules [i];
//		bool special = (r == 1);
//		if (special)
//			r = 2;
//		int start = (r == 4) ? 3 : 2;
//		int quantity = (k == 3 && r != 0) ? n - r - 1 : n - r;
//
//
//		for (int s=start; s<start+quantity; ++s) {
//
//			verti[j]=createNextPolygon (verti[i],s%n,n,k); 
//			rules[j]=(k==3&&s==start&&r!=0)?4:3;
//			j++;
//			int m;
//			if(special)
//				m=2;
//			else if(s==2&&r!=0)
//				m=1;
//			else
//				m=0;
//			for(;m<k-3;++m)
//			{
//				verti[j]=createNextPolygon(verti[j-1],1,n,k);
//				rules[j]=(n==3&&m==k-4)?1:2;
//				j++;
//
//			}
//
//		}
//
//		return j;
//	}
//
//
//	private Polygon createNextPolygon(Polygon P, int s , int n , int k){
//
//		Vector3 start = P.vertices[s];
//		Vector3 end = P.vertices[(s+1)%n];
//
//		Polygon Q = new Polygon ();
//
//		for (int i=0; i<n; i++) {
//
//			Q.vertices.Add(new Vector3(0,0,0));
//		}
//		for(int i=0;i<n;i++){
//			int j=(n+s-i+1)%n;
//			Q.vertices[j] = Reflect(start,end,P.vertices[i]);
//		}
//
//		return Q;
//
//	}
//
//	public Vector3 Reflect(Vector3 A, Vector3 B, Vector3 R){
//		float den = A.x * B.z - B.x * A.z;
//		bool straight = Mathf.Abs(den)<10E-11;
//
//		if (straight) {
//
//			Vector3 P = A;
//			den = Mathf.Sqrt((A.x-B.x)*(A.x-B.x)+(A.z-B.z)+(A.z-B.z));
//			Vector3 D = new Vector3((B.x-A.x)/den,0,(B.z-A.z)/den);
//			float factor = 2.0f*((R.x-P.x)*D.x+(R.z-P.z)*D.z);
//			return (new Vector3(2.0f*P.x+factor*D.x-R.x,0,2.0f*P.z+factor*D.z-R.z));
//		}
//		else {
//
//			float s1= (1.0f+A.x*A.x+A.z*A.z)/2.0f;
//			float s2= (1.0f+B.x*B.x+B.z*B.z)/2.0f;
//
//			Vector3 C = new Vector3((s1*B.z-s2*A.z)/den,0,(A.x*s2-B.x*s1)/den);
//			float r = Mathf.Sqrt(C.x*C.x+C.z*C.z-1.0f);
//
//			float factor = r*r/((R.x-C.x)*(R.x-C.x)+(R.z-C.z)*(R.z-C.z));
//			Vector3 H = new Vector3(C.x+factor*(R.x-C.x),0, C.z+factor*(R.z-C.z));
//			return H;
//
//		}
//
//
//	}
//	public class Polygon {
//		public List<Vector3>vertices = new List<Vector3>();
//
//	}
//
//}