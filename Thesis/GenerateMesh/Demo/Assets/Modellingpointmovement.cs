﻿using UnityEngine;
using System.Collections;
using System;

public class Modellingpointmovement : MonoBehaviour {
	Ray ray;
	RaycastHit hit;
	public GameObject Sphere3d;
	public int p=4;
	public int q=6;
	public float cosp,sinp,coshq,sinhq,cosh2q,sinh2q;
	public int tile_clicked=0;
	public float[,] transformationmatrix = new float[3, 3];

	// Use this for initialization
	void Start () {
		cosp=Mathf.Cos(Mathf.PI/p);
		sinp=Mathf.Sin(Mathf.PI/p);
		coshq=Mathf.Cos(Mathf.PI/q)/sinp;
		sinhq=Mathf.Sqrt(coshq*coshq-1);
		cosh2q = 2 * coshq * coshq - 1;
		sinh2q = 2 * sinhq * coshq;
		print ("cosh2q " + cosh2q);
		print("sinh2q "+ sinh2q);
		print ("coshq " + coshq);
		print ("sinhq " + sinhq);
	}

	
	// Update is called once per frame
	// This block checks the position of the mouse pointer...
	// And if it in within bounds finds the equivalent point
	// in the triply periodic polyhedron and places the
	// yellow sphere there....
	void Update()
	{

		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit)) 
		{
			LineRenderer lineRend;
			if(hit.collider.name=="0")
			{
				transform.position=hit.point;
				Vector3 position3d = get3dequivalentpointtile0 (transform.position);
				Sphere3d.transform.position = position3d;
				defaultLineWidth ();
				GameObject.Find ("0").GetComponent<LineRenderer> ().SetWidth(0.05f,0.05f);
			}
				
			else if (hit.collider.name == "9") 
			{
				tile_clicked = 9;
				transform.position = hit.point;
				Vector3 position3d = get3dequivalentpoint (transform.position,tile_clicked);
				Sphere3d.transform.position = position3d;
				defaultLineWidth ();
				GameObject.Find ("9").GetComponent<LineRenderer> ().SetWidth(0.05f,0.05f);
			}
			else if (hit.collider.name == "5") 
			{
				tile_clicked = 5;
				transform.position = hit.point;
				Vector3 position3d = get3dequivalentpoint (transform.position,tile_clicked);
				Sphere3d.transform.position = position3d;
				defaultLineWidth ();
				GameObject.Find ("5").GetComponent<LineRenderer> ().SetWidth(0.05f,0.05f);
			}
			else if (hit.collider.name == "12") 
			{
				tile_clicked = 12;
				transform.position = hit.point;
				Vector3 position3d = get3dequivalentpoint (transform.position,tile_clicked);
				Sphere3d.transform.position = position3d;
				defaultLineWidth ();
				GameObject.Find ("12").GetComponent<LineRenderer> ().SetWidth(0.05f,0.05f);
			}
			else if (hit.collider.name == "11") 
			{
				tile_clicked = 11;
				transform.position = hit.point;
				Vector3 position3d = get3dequivalentpoint (transform.position,tile_clicked);
				Sphere3d.transform.position = position3d;
				defaultLineWidth ();
				GameObject.Find ("11").GetComponent<LineRenderer> ().SetWidth(0.05f,0.05f);
			}
			else if (hit.collider.name == "10") 
			{
				tile_clicked = 10;
				transform.position = hit.point;
				Vector3 position3d = get3dequivalentpoint (transform.position,tile_clicked);
				Sphere3d.transform.position = position3d;
				defaultLineWidth ();
				GameObject.Find ("10").GetComponent<LineRenderer> ().SetWidth(0.05f,0.05f);
			}
		}
	
	}

	// Converts a point in poincare model to Klein model.
	public Vector3 ptok(Vector3 poincarepoint)
	{
		Vector3 kleinpoint = new Vector3 (2 * poincarepoint.x / (1 + poincarepoint.x * poincarepoint.x + poincarepoint.y * poincarepoint.y), 
			2 * poincarepoint.y / (1 + poincarepoint.x * poincarepoint.x + poincarepoint.y * poincarepoint.y),0);

		return kleinpoint;
	}
		

	// Applies a matrix transformation to the original poincare point after 
	// taking that point in the hyperboloid model...
	// The transformation depends on the polygon in the hyperbolic tessellation..
	// Then the final result is brought back to klein model..
	// Finally the yellow sphere is place there...
	public Vector3 TransformationPoint(Vector3 pointin2d)
	{
		
		transformationmatrix [0, 0] = -cosh2q;
		transformationmatrix [0,1]= 0f;
		transformationmatrix[0,2]= sinh2q;
		transformationmatrix [1,0]=0f;
		transformationmatrix [1,1]=1f;
		transformationmatrix [1,2]=0f;
		transformationmatrix [2,0]=-sinh2q;
		transformationmatrix [2,1]=0f;
		transformationmatrix [2,2]=cosh2q;
		
		switch (tile_clicked)
		{
		case 9:
			transformationmatrix[0,2]= -sinh2q;
			transformationmatrix [1,1]=-1f;
			transformationmatrix [2,2]=-cosh2q;
			break;
			
		case 5:
			transformationmatrix [0, 0] = 0f;
			transformationmatrix [0, 1] = -cosh2q;
			transformationmatrix [0, 2] = sinh2q;
			transformationmatrix [1, 0] = -1f;
			transformationmatrix [1, 1] = 0f;
			transformationmatrix [1, 2] = 0f;
			transformationmatrix [2, 0] = 0f;
			transformationmatrix [2, 1] = -sinh2q;
			transformationmatrix [2, 2] = cosh2q;
			break;
			
		case 12:

			transformationmatrix [0, 0] = -cosh2q;
			transformationmatrix [0,1]= 0;
			transformationmatrix[0,2]= -sinh2q;
			transformationmatrix [1,0]=0;
			transformationmatrix [1,1]=-1;
			transformationmatrix [1,2]=0f;
			transformationmatrix [2,0]=-sinh2q;
			transformationmatrix [2,1]=0;
			transformationmatrix [2,2]=-cosh2q;

			break;
			
		case 11:


			transformationmatrix [0, 0] = -cosh2q;
			transformationmatrix [0,1]= 0;
			transformationmatrix[0,2]= -sinh2q;
			transformationmatrix [1,0]=0;
			transformationmatrix [1,1]=-1;
			transformationmatrix [1,2]=0f;
			transformationmatrix [2,0]=-sinh2q;
			transformationmatrix [2,1]=0;
			transformationmatrix [2,2]=-cosh2q;

			break;
			
		case 10:

			transformationmatrix [0, 0] = -cosh2q;
			transformationmatrix [0,1]= 0;
			transformationmatrix[0,2]= -sinh2q;
			transformationmatrix [1,0]=0;
			transformationmatrix [1,1]=-1;
			transformationmatrix [1,2]=0;
			transformationmatrix [2,0]=-sinh2q;
			transformationmatrix [2,1]=0f;
			transformationmatrix [2,2]=-cosh2q;

			break;
			
			
		}
		print ("poincare point " + pointin2d.x + "  "+ pointin2d.y + " " + pointin2d.z);
		// convert poincare point to hyperboloid point
		Vector3 hyperboloidpoint = ptoh (pointin2d);
		print ("hyperboloid point " + hyperboloidpoint.x +"  "+ hyperboloidpoint.y +" "+hyperboloidpoint.z);
		
		// applying transformation....create a vector3 to save intermediate result.
		
		// transformation
		float[,] intermediateresult=matrixmult(transformationmatrix,ConvertVector3tomatrix(hyperboloidpoint));
		
		Vector3 intermediatepoint = ConvertmatrixtoVector3(intermediateresult);
		
		print ("transformed hyperboloid point " + intermediatepoint.x+ " "+intermediatepoint.y +" "+intermediatepoint.z);

		
		Vector3 finalkleinpoint = htok (intermediatepoint);
		print ("final klein  " + finalkleinpoint);
		return finalkleinpoint;
		
		
	}
		


	// Converts a point in Klein model to hyperboloid model..
	public Vector3 ktoh(Vector3 pointinklein)
	{
		
		float divisor = 1 / Mathf.Sqrt(1 - pointinklein.x * pointinklein.x - pointinklein.y * pointinklein.y);
		float hyperboloid_x = pointinklein.x / divisor;
		float hyperboloid_y = pointinklein.y/divisor;
		float hyperboloid_z = 1 / divisor;

		return new Vector3 (hyperboloid_x,hyperboloid_y,hyperboloid_z);
	}

	// Converts a point in Hyperboloid model to Klein model..
	public Vector3 htok(Vector3 hyperboloidpoint)
	{
		return new Vector3 (hyperboloidpoint.x/hyperboloidpoint.z,hyperboloidpoint.y/hyperboloidpoint.z,1);


	}

	// COnverts a point in hyperboloid model to Poincare model...
	public Vector3 htop(Vector3 hyperboloidpoint)
	{

		Vector3 poincarepoint = new Vector3 (0,0,0);
		float divisor = 1 / (hyperboloidpoint.z + 1);
		print ("div" + divisor);
		poincarepoint.x = hyperboloidpoint.x / divisor;
		print ("x " + poincarepoint.x);

		poincarepoint.y = hyperboloidpoint.y / divisor;
		print ("y" + poincarepoint.y);
		poincarepoint.z = 0;
		return poincarepoint;
	}

	// COnverts a point in poincare model to hyperboloid model..
	public Vector3 ptoh(Vector3 poincarepoint)
	{
		

		float x = poincarepoint.x;
		float y = poincarepoint.y;
		float divisor = (1 - x * x - y * y);
		float hyperboloid_x=2*x/divisor;
		float hyperboloid_y=2*y/divisor;
		float hyperboloid_z=(1+x*x+y*y)/divisor;
		return new Vector3 (hyperboloid_x,hyperboloid_y,hyperboloid_z);
	}



	// Finds the point in the triply periodic polyhedron, where
	// the yellow sphere has to be placed...
	public Vector3 get3dequivalentpoint(Vector3 twodimensionalpoint,int tile_no_clicked)
	{
		Vector3 offset = new Vector3 (0,0,0);
		Vector3 kleinpoint = ptok (twodimensionalpoint);
		print ("kleinpoint " + kleinpoint);
		Vector3 finalpoint=new Vector3(0,0,0);
		Vector3 point=TransformationPoint (twodimensionalpoint);
		switch (tile_no_clicked)
		{
			
		case 5:
			offset = new Vector3 (0f, 1.16f, 9f);
			finalpoint.x = point.x;
			finalpoint.z = point.y;
			break;
		case 9:
			offset= new Vector3 (1.16f, 0.561f, 9.58f);
			finalpoint.y = point.y;
			finalpoint.x = -point.x;
			break;
		case 10:
			offset = new Vector3 (1.16f, 1.16f, 10.16f);

			point=ktop (point);
			finalpoint.y = point.x;
			finalpoint.x = -point.y;
			Vector3 finalpoint_1=TransformationPoint(finalpoint);
			print ("finalpoint_1" +finalpoint_1);
			finalpoint.z=-finalpoint_1.x;
			finalpoint.x=-finalpoint_1.y;
			finalpoint.y=0;
			break;
		case 11:
			offset = new Vector3 (0.581f, 1.79f, 10.16f);
			point=ktop(point);
			
			finalpoint.y = point.x;
			finalpoint.x = -point.y;
			Vector3 finalpoint_2=TransformationPoint(finalpoint);
			finalpoint_2=ktop (finalpoint_2);
			
			print ("fp 2 "+finalpoint_2);
			finalpoint.y = finalpoint_2.x;
			finalpoint.x = -finalpoint_2.y;
			Vector3 finalpoint_3=TransformationPoint(finalpoint);
			finalpoint.y=-finalpoint_3.x;
			finalpoint.z=-finalpoint_3.y;
			finalpoint.x=0;
			break;

		case 12:
			offset = new Vector3 (0f, 1.743f, 9.581f);
			point=ktop (point);
			finalpoint.y = point.x;
			finalpoint.x = -point.y;
			Vector3 finalpoint_4=TransformationPoint(finalpoint);
			finalpoint_4=ktop (finalpoint_4);
			finalpoint.y = finalpoint_4.x;
			finalpoint.x = -finalpoint_4.y;
			Vector3 finalpoint_5=TransformationPoint(finalpoint);
			finalpoint_5=ktop (finalpoint_5);
			finalpoint.y = finalpoint_5.x;
			finalpoint.x = -finalpoint_5.y;
			Vector3 finalpoint_6=TransformationPoint(finalpoint);
			finalpoint.x=finalpoint_6.x;
			finalpoint.y=-finalpoint_6.y;
			finalpoint.z=0;
			break;
		}
		
		
		return finalpoint+offset;
	}

	// Find the equivalent point for the fundamental polygon...
	public Vector3 get3dequivalentpointtile0(Vector3 twodimensionalpoint)
	{
		Vector3 kleinpoint = ptok (twodimensionalpoint);
		Vector3 finalpoint = new Vector3 (0, 0, 0);
		finalpoint.y = kleinpoint.y;
		finalpoint.z = -kleinpoint.x;
		print ("final klein point " + finalpoint);
		return finalpoint + new Vector3(0.591f,0.591f,9f);;
	}
		

	// Matrix multiplication between two matrices...
	public float[,] matrixmult(float[,] a, float[,] b)
	{
		int aRows = a.GetLength(0);
		int aColumns = a.GetLength(1);
		int bRows = b.GetLength(0);
		int bColumns = b.GetLength(1);

		float [,] C = new float[aRows,bColumns];
		for (int i = 0; i < aRows; i++) {
			for (int j = 0; j < bColumns; j++) {
				C[i,j] = 0f;
			}
		}

		for (int i = 0; i < aRows; i++) 
		{
			for (int j = 0; j < bColumns; j++)
			{ 
				for (int k = 0; k < aColumns; k++) 
				{ 
					C[i,j] += a[i,k] * b[k,j];
				}
			}
		}
		return C;
	}


	// Convert vector3 to matrix
	public float[,] ConvertVector3tomatrix(Vector3 point)
	{
		float[,] matrixpoint = new float[3,1]{
			{point.x},
			{point.y},
			{point.z},
		};

		return matrixpoint;
	}

	// Convert matrix to vector3
	public Vector3 ConvertmatrixtoVector3(float[,] matrix)
	{
		Vector3 point = new Vector3 ();
		point.x = matrix [0,0];
		point.y = matrix [1,0];
		point.z = matrix [2,0];
		return point;
	}

	// Sets up the default line renderer width...
	public void defaultLineWidth()
	{
		GameObject.Find ("0").GetComponent<LineRenderer> ().SetWidth (0.02f, 0.02f);
		GameObject.Find ("5").GetComponent<LineRenderer> ().SetWidth (0.02f, 0.02f);
		GameObject.Find ("9").GetComponent<LineRenderer> ().SetWidth (0.02f, 0.02f);
		GameObject.Find ("10").GetComponent<LineRenderer> ().SetWidth (0.02f, 0.02f);
		GameObject.Find ("11").GetComponent<LineRenderer> ().SetWidth (0.02f, 0.02f);
		GameObject.Find ("12").GetComponent<LineRenderer> ().SetWidth (0.02f, 0.02f);
	}

	// Converts a point in Klein to poincare model...
	public Vector3 ktop(Vector3 b)
	{
		return 
				new Vector3 (
				b.x / (1 + Mathf.Sqrt (1 - b.x * b.x - b.y * b.y)),
				b.y / (1 + Mathf.Sqrt (1 - b.x * b.x - b.y * b.y)),
				0
				
		);
	}
	
}
