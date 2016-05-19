using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HyperbolicGeometry{
public class twodhyperbolic : MonoBehaviour {
	public Material faceMaterial;
	public Material linematerial;
	Polygon[] verti;
	public Polygon[] FinalPoints,poincarePoints,finalPoints;
	int[] rules;
	public int sides=20;
	public int polygons_at_vertice=3;
	public int max =1000, numberOfLayers=1;
	public Material mat;
	public int maxPolygons, layerPolygons;
	public bool done=false;
	int checkvalue=0;
	int polygonsinlayer=0;
	int value;
	int number=0,lineValue=1;
	int[] polygonsinalllayers;
	LineRenderer lineRenderer;
	void Start(){


			lineRenderer = gameObject.AddComponent<LineRenderer> ();
			lineRenderer.material = new Material (Shader.Find ("Particles/Additive"));
			lineRenderer.SetColors (Color.red, Color.red);
			lineRenderer.SetWidth (0.01f, 0.01f);
		do {
			polygonsinalllayers = new int[numberOfLayers + 1];
			polygonCount (numberOfLayers, sides, polygons_at_vertice, max);
			if (maxPolygons > max)
				numberOfLayers--;

		} while(maxPolygons>max);
		verti = new Polygon[maxPolygons];
		rules = new int[maxPolygons];

		verti [0] = ConstructCenterPolygon (sides, polygons_at_vertice);
		Polygon Kleinpoints = ptok (verti [0]);

		Polygon Kleinpoints_subdivided = subdivide (Kleinpoints, 15);
		Polygon Poincarepoints = ktop (Kleinpoints_subdivided);



		rules [0] = 0;
		int j = 1;





		for (int i =0; i<layerPolygons; i++) {
			j=RuleApply(i,j,sides, polygons_at_vertice);

		}

		FinalPoints = new Polygon[verti.Length];
		for (int p=0; p<verti.Length; p++) {
			FinalPoints[p]=DrawHyperbolicCenterPolygon (verti [p]);
			FinalPoints [p].vertices.Add (FinalPoints[p].vertices[0]);

		}
				
		foreach ( Vector3  v in verti[0].vertices) {
			print("verti0"+ v.x + "   "+  v.y+ "    "+v.z);
		}
		foreach ( Vector3  v in verti[1].vertices) {
			print ("verti1" + v.x + "   " + v.y + "    " + v.z);
		}
		foreach ( Vector3  v in verti[5].vertices) {
			print ("verti5" + v.x + "   " + v.y + "    " + v.z);
		}

		Polygon demo=ptok (verti [0]);
		print ( "klein point" + demo.vertices [1]);
		print (" original poincare point "+verti [0].vertices [1]);

		
		
		
	}



		void Update()
		{
				
			RenderLine ();
			int lengthofLineRenderer = finalPoints [0].vertices.Count*6;
			lineRenderer.SetVertexCount (lengthofLineRenderer);
			int k = 0;
			while (k < lengthofLineRenderer) 
			{
				for (int i = 0; i < finalPoints.Length; i++)
				{
					for(int j=0;j<FinalPoints[i].vertices.Count;j++)
					{
						Vector3 pos = new Vector3 (finalPoints [i].vertices [j].x, finalPoints [i].vertices [j].y, 0);
						lineRenderer.SetPosition (k, pos);
						k++;
					}
						
				}
			}
		}
		


	private void polygonCount(int layernumber,int side, int vertex, int maxValue){
		maxPolygons = 1;  // total polygons so far, just the center one
		layerPolygons = 0; // polygons in the inner layer
		int p = side * (vertex - 3); // polygons in the layer joined by vertex
		print ("p" + p); 
		int q = side;   // polygons in the layer joined by edge
		print ("q" + q);

		int nextP, nextQ;
		for (int i=1; i<=layernumber; ++i) {
			layerPolygons=maxPolygons;
			polygonsinalllayers [i-1] = layerPolygons;
			if(vertex == 3){
				nextP=p+q;
				nextQ=(side-6)*p+(side-5)*q;

			}
			else{
				nextP=((side-2)*(vertex-3)-1)*p+((side-3)*(vertex-3)-1)*q;
				nextQ=(side-2)*p+(side-3)*q;


			}
			maxPolygons+=p+q;
			if(maxPolygons>max)
				break;
			p=nextP;
			q=nextQ;

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
					s * Mathf.Cos((3 + 2 * i) * angleA),
					s * Mathf.Sin((3 + 2 * i) * angleA),
					0
				)
			);
		}

		return p;

	}
	public Polygon ptok (Polygon p){
		Polygon q = new Polygon ();
		foreach (Vector3 a in p.vertices) {
			q.vertices.Add(

				 new  Vector3(
					2*a.x/(1+a.x*a.x+a.y*a.y),
				    2*a.y/(1+a.x*a.x+a.y*a.y),
					0
				)
			);

		}
		return q;
	}

	public Polygon ktop(Polygon q){
		Polygon r = new Polygon ();
		foreach (Vector3 b in q.vertices) {
			r.vertices.Add (
				new Vector3(
					b.x/(1+Mathf.Sqrt(1-b.x*b.x-b.y*b.y)),
					b.y/(1+Mathf.Sqrt(1-b.x*b.x-b.y*b.y)),
					0

				)
			);


		}
		return r;
	}

	public Polygon subdivide(Polygon q,int numberofpoints)
	{
		Polygon r = new Polygon ();
		for (int i=0; i<q.vertices.Count-1; i++)        
		{
			Vector3 end1=q.vertices[i];
			Vector3 end2=q.vertices[i+1];
			float dx=end2.x-end1.x;
			float dy=end2.y-end1.y;
			float stepx = dx / numberofpoints;
			float stepy = dy / numberofpoints;
			float px = end1.x ;
			float py = end1.y ;

			for (int ix = 0; ix <= numberofpoints; ix++)
			{
				Vector3 point=new Vector3(px,py,0);
				r.vertices.Add(point);
				px = px + stepx;
				py = py + stepy;
			}
		}
		return r;
	}

	public Polygon2D subdivide2D(Polygon2D q,int numberofpoints)
	{
		Polygon2D r = new Polygon2D ();
		for (int i=0; i<q.vertices.Count-1; i++)        
		{
			Vector3 end1=q.vertices[i];
			Vector3 end2=q.vertices[i+1];
			float dx=end2.x-end1.x;
			float dy=end2.y-end1.y;
			float stepx = dx / numberofpoints;
			float stepy = dy / numberofpoints;
			float px = end1.x ;
			float py = end1.y ;


			for (int ix = 0; ix <= numberofpoints; ix++)
			{
				Vector2 point=new Vector2(px,py);
				r.vertices.Add(point);
				px = px + stepx;
				py = py + stepy;
			}
		}
		return r;
	}
	public int RuleApply(int i, int j, int n, int k ){

		int r = rules [i];
		bool special = (r == 1);
		if (special)
			r = 2;
		int start = (r == 4) ? 3 : 2;
		int quantity = (k == 3 && r != 0) ? n - r - 1 : n - r;


		for (int s=start; s<start+quantity; ++s) {

			verti[j]=createNextPolygon (verti[i],s%n,n,k); 
			rules[j]=(k==3&&s==start&&r!=0)?4:3;
			j++;
			int m;
			if(special)
				m=2;
			else if(s==2&&r!=0)
				m=1;
			else
				m=0;
			for(;m<k-3;++m)
			{
				verti[j]=createNextPolygon(verti[j-1],1,n,k);
				rules[j]=(n==3&&m==k-4)?1:2;
				j++;

			}

		}

		return j;
	}


	private Polygon createNextPolygon(Polygon P, int s , int n , int k){

		Vector3 start = P.vertices[s];
		Vector3 end = P.vertices[(s+1)%n];

		Polygon Q = new Polygon ();

		for (int i=0; i<=n; i++) {

			Q.vertices.Add(new Vector3(0,0,0));
		}
		for(int i=0;i<n;i++){
			int j=(n+s-i+1)%n;
			Q.vertices[j] = Reflect(start,end,P.vertices[i]);
		}
		Q.vertices [n] = Q.vertices [0];
		return Q;

	}

	public Vector3 Reflect(Vector3 A, Vector3 B, Vector3 R){
		float den = A.x * B.y - B.x * A.y;
		bool straight = Mathf.Abs(den)<10E-11;

		if (straight) {

			Vector3 P = A;
			den = Mathf.Sqrt((A.x-B.x)*(A.x-B.x)+(A.y-B.y)+(A.y-B.y));
			Vector3 D = new Vector3((B.x-A.x)/den,(B.y-A.y)/den,0);
			float factor = 2.0f*((R.x-P.x)*D.x+(R.y-P.y)*D.y);
			return (new Vector3(2.0f*P.x+factor*D.x-R.x,2.0f*P.y+factor*D.y-R.y,0));
		}
		else {

			float s1= (1.0f+A.x*A.x+A.y*A.y)/2.0f;
			float s2= (1.0f+B.x*B.x+B.y*B.y)/2.0f;

			Vector3 C = new Vector3((s1*B.y-s2*A.y)/den,(A.x*s2-B.x*s1)/den,0);
			float r = Mathf.Sqrt(C.x*C.x+C.y*C.y-1.0f);

			float factor = r*r/((R.x-C.x)*(R.x-C.x)+(R.y-C.y)*(R.y-C.y));
			Vector3 H = new Vector3(C.x+factor*(R.x-C.x), C.y+factor*(R.y-C.y),0);
			return H;

		}


	}
	public class Polygon {
		public List<Vector3>vertices = new List<Vector3>();
	}

	public class Polygon2D {
		public List<Vector2>vertices = new List<Vector2>();
	}


	public Polygon DrawHyperbolicCenterPolygon(Polygon a)
	{
		Vector3 center = CenterofPolygon (a);
		Polygon Kleinpints = ptok (a);
		Polygon Kleinpoints_subdivided=subdivide(Kleinpints, 10);
		Polygon Poincarepoints = ktop(Kleinpoints_subdivided);
		Polygon FinalPoincarepoints = new Polygon ();
		for (int i=0; i<Poincarepoints.vertices.Count-1; i++) 
		{
			FinalPoincarepoints.vertices.Add(Poincarepoints.vertices[i]);
			FinalPoincarepoints.vertices.Add(Poincarepoints.vertices[i+1]);
			FinalPoincarepoints.vertices.Add (center);
		}

		Vector3[] vertices = new Vector3[FinalPoincarepoints.vertices.Count];
		for (int i=0; i<vertices.Length; i++) {
			vertices[i]=FinalPoincarepoints.vertices[i];
		}

		int[] triangles = new int[FinalPoincarepoints.vertices.Count];
		for (int i=0; i<triangles.Length; i++) {
			triangles[i]=i;

		}
			GameObject tile=new GameObject(number.ToString());
		tile.tag = "Player";
		number++;
		Mesh msh = new Mesh();
		tile.AddComponent<MeshFilter> ();
		Color newColor = new Color( Random.value,Random.value,Random.value,1.0f );
		tile.AddComponent<MeshRenderer> ().material=mat;
		msh.vertices = vertices;
		msh.triangles = triangles;
		tile.AddComponent<MeshCollider> ();
		tile.GetComponent<MeshCollider> ().sharedMesh = msh;
		Vector2[] uvpoints = renderpoints (a);
		msh.uv = uvpoints;
		tile.GetComponent<MeshFilter>().mesh = msh;
		//msh.vertices = new Vector3[]{a.vertices [0],a.vertices [1],a.vertices [2],a.vertices [3]};
		//msh.uv=new Vector2[] {new Vector2(0,0), new Vector2(0,1), new Vector2(1,1), new Vector2(1,0)};
		Vector3[] normals=new Vector3[vertices.Length];
		for (int i = 0; i < normals.Length; i++) {
			normals [i] = Vector3.down;
		}
		msh.normals = normals;
		//msh.RecalculateBounds ();
		return Poincarepoints;
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


	// older version

	public Vector2[] renderpoints(Polygon polygon)
	{
		polygonsinalllayers [numberOfLayers] = maxPolygons;
		value = (polygonsinalllayers [1] - polygonsinalllayers [0])/4;
		Polygon2D b = new Polygon2D ();
		b.vertices.Add (new Vector2 (0, 0));
		b.vertices.Add (new Vector2 (0, 1));
		b.vertices.Add (new Vector2 (1, 1));
		b.vertices.Add (new Vector2 (1, 0));
		b.vertices.Add (new Vector2 (0, 0));
		if ((done && checkvalue<=value) || (checkvalue>(2*value) && checkvalue<=(3*value))) {
			Polygon2D b1 = new Polygon2D ();
			b1 = rotate90points (b);
			b = b1;
		}
		if (checkvalue >value && checkvalue <=(2*value)) {
			Polygon2D b2 = new Polygon2D ();
			b2 = rotate180points (b);
			b = b2;
		}
		if ((checkvalue >= 23 && checkvalue < 27) || (checkvalue >= 30 && checkvalue < 34) || (checkvalue >= 37 && checkvalue < 41)) {
			Polygon2D b2 = new Polygon2D ();
			b2 = rotate90points (b);
			b = b2;
		}
		if ((checkvalue >= 41 && checkvalue < 47) || (checkvalue >=51 && checkvalue<54) || (checkvalue >=58 && checkvalue<61)) {
			Polygon2D b2 = new Polygon2D ();
			b2 = rotate90points (b);
			b = b2;
		}
		if ((checkvalue >= 71 && checkvalue < 75) || (checkvalue >=78 && checkvalue<82) || (checkvalue >=85 && checkvalue<89)) {
			Polygon2D b2 = new Polygon2D ();
			b2 = rotate90points (b);
			b = b2;
		}
		if ((checkvalue >= 89 && checkvalue < 95) || (checkvalue >=99 && checkvalue<102) || (checkvalue >=106 && checkvalue<109)) {
			Polygon2D b2 = new Polygon2D ();
			b2 = rotate90points (b);
			b = b2;
		}


		checkvalue++;
		Polygon2D c = new Polygon2D ();
		c = subdivide2D (b, 10);
		Polygon2D d = new Polygon2D ();
		for(int i=0;i<c.vertices.Count-1;i++)
		{
			d.vertices.Add (c.vertices [i]);
			d.vertices.Add (c.vertices [i+1]);
			d.vertices.Add (new Vector2 (0.5f, 0.5f));

		}
		Vector2[] final = new Vector2[d.vertices.Count];
		for (int i = 0; i < final.Length; i++) {
			final [i] = d.vertices [i];
		}
		done = true;

		return final;
	}


	// new version
	/*
	public Vector2[] renderpoints(Polygon polygon)
	{
		Polygon2D b = new Polygon2D ();
		b.vertices.Add (new Vector2 (0, 0));
		b.vertices.Add (new Vector2 (0, 1));
		b.vertices.Add (new Vector2 (1, 1));
		b.vertices.Add (new Vector2 (1, 0));
		b.vertices.Add (new Vector2 (0, 0));

		int[] res = typeofpolygon (verti, polygonsinalllayers);
		if (res [checkvalue] ==1)
		{
			print ("rotate + checkvalue " + checkvalue);
			print ("res[checkvalue] " + res [checkvalue]);
			Polygon2D b1 = new Polygon2D ();
			b1 = rotate90points (b);
			b = b1;
		}

		checkvalue++;
		Polygon2D c = new Polygon2D ();
		c = subdivide2D (b, 10);
		Polygon2D d = new Polygon2D ();
		for(int i=0;i<c.vertices.Count-1;i++)
		{
			d.vertices.Add (c.vertices [i]);
			d.vertices.Add (c.vertices [i+1]);
			d.vertices.Add (new Vector2 (0.5f, 0.5f));

		}
		Vector2[] final = new Vector2[d.vertices.Count];
		for (int i = 0; i < final.Length; i++) {
			final [i] = d.vertices [i];
		}

		return final;

	}

	*/

	public Polygon2D rotate90points(Polygon2D polygon)
	{
		Polygon2D rotated = new Polygon2D ();
		for (int i = 1; i < sides + 1; i++) {
			rotated.vertices.Add (polygon.vertices[i]);
		}
		rotated.vertices.Add (polygon.vertices[1]);

		return rotated;
	}
	public Polygon2D rotate180points(Polygon2D polygon)
	{
		Polygon2D rotated180 = new Polygon2D ();
		Polygon2D rotated90 = new Polygon2D ();
		rotated90 = rotate90points (polygon);
		rotated180 = rotate90points (rotated90);
		return rotated180;
	}

	// 1 represents if two polygons share an edge and 2 represents if two polygons share a vertex
	public int[] typeofpolygon(Polygon[] allpolygons,int[] numberofpolygonsinlayers)
	{
		int[] result = new int[maxPolygons];
		result [0] = 0;

		int val1 = numberofpolygonsinlayers [1] - numberofpolygonsinlayers [0];
		for (int i = 1; i <=val1; i++) {
			if (check (allpolygons [0], allpolygons [i]))
				result [i] = 1;
			else
				result [i] = 2;
			
		}
		int val2 = numberofpolygonsinlayers [2] - numberofpolygonsinlayers [1] - numberofpolygonsinlayers [0];

		for (int i = val1+1; i <=val1+val2+1; i++) {
			for (int j = 1; j <=val1; j++) 
			{
				if (check (allpolygons [j], allpolygons [i]))
					result [i] = 1;
			}
			if (result [i] == 0)
				result [i] = 2;
		}
		return result;
	}

	public bool check(Polygon a, Polygon b)
	{
		int matching_points = 0;
		for (int i = 0; i < a.vertices.Count - 1; i++) 
		{
			for (int j = 0; j < b.vertices.Count - 1; j++) 
			{
				if (a.vertices [i] == b.vertices [j])
					matching_points++;
			}
		}
		if (matching_points == 2)
			return true;
		else
			return false;
	}

	public void RenderLine()
	{
			poincarePoints = FinalPoints;
			finalPoints = new Polygon[6];
			int[] arrayOfPolygons = new int[6]{0,5,9,10,11,12};
			int l = 0;
			int length = poincarePoints[0].vertices.Count;

			foreach (int k in arrayOfPolygons)
			{
				finalPoints[l]=new Polygon();
				for(int i=0;i<length;i++)
				{
					finalPoints[l].vertices.Add(poincarePoints[k].vertices[i]);
				}
				l++;
			}

	}

}
}