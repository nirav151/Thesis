//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using HyperbolicGeometry;
//
//public class LineRender : MonoBehaviour {
//	public static twodhyperbolic.Polygon[] poincarePoints;
//	public Polygon[] finalPoints;
//	static Material lineMaterial;
//	void Start(){
//		
//	
//	}
//	void Update(){
//		RenderLine ();
//		int lengthofLineRenderer = finalPoints [0].vertices.Count;
//		LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
//		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
//		lineRenderer.SetColors(Color.yellow, Color.yellow);
//		lineRenderer.SetWidth (0.01f, 0.01f);
//		lineRenderer.SetVertexCount(lengthofLineRenderer);
//		int k = 0;
//		while (k<lengthofLineRenderer) {
//			print ("in this loop");
//			Vector3 pos = new Vector3(finalPoints [0].vertices[k].x,finalPoints [0].vertices[k].y,0);
//			lineRenderer.SetPosition(k,pos);
//			k++;
//		}
//	
//	}
//
//	static void CreateLineMaterial(){
//		if (!lineMaterial) {
//			lineMaterial = new Material (Shader.Find("Specular"));
//			lineMaterial.hideFlags = HideFlags.HideAndDontSave;
//			lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
//		
//		}
//	
//	
//	}
//	public void RenderLine(){
//		poincarePoints = GameObject.Find ("GameObject").GetComponent<twodhyperbolic> ().FinalPoints;
//		finalPoints = new Polygon[6];
//		print (" poincare points"+poincarePoints[0].vertices[0]);
//		int[] arrayOfPolygons = new int[6]{0,5,9,10,11,12};
//		int l = 0;
//		int length = poincarePoints[0].vertices.Count;
//
//		foreach (int k in arrayOfPolygons)
//		{
//			print ("k value " + k);
//			finalPoints[l]=new Polygon();
//			for(int i=0;i<length;i++)
//			{
//				finalPoints[l].vertices.Add(poincarePoints[k].vertices[i]);
//			}
//			l++;
//		}
//	
//	}
//
////	void OnPostRender(){
////		RenderLine ();
////		GL.Begin (GL.LINES);
////		GL.Color (Color.red);
////
////
////	
////		
////		//CreateLineMaterial ();
////		//lineMaterial.SetPass (0);
////
////		//for (int i = 0; i < finalPoints.Length; i++) 
////		//{
////
////			for (int j = 0; j < finalPoints [0].vertices.Count - 1; j++) 
////			{
////				
////				print ("First point"+ finalPoints [0].vertices [j]);
////				GL.Vertex (finalPoints [0].vertices [j]);
////				GL.Vertex (finalPoints [0].vertices [j+1]);
////			print ("Second point"+ finalPoints [0].vertices [j]);
////
////			}
////
////		//}
////		GL.End ();
////	
////	
////	}
//	public class Polygon {
//		public List<Vector3>vertices = new List<Vector3>();
//	}
//}
