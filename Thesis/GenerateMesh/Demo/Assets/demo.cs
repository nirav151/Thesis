using UnityEngine;
using System.Collections;

public class demo : MonoBehaviour {

	// Use this for initialization
	public Material mat;
	void Start () {
	
		GameObject tile=new GameObject("TILE");
		Mesh msh = new Mesh();
		tile.AddComponent<MeshFilter> ();
		tile.AddComponent<MeshRenderer> ().material=mat;
		Vector3[] vertices=new [] { new Vector3(0f,0f,0f), new Vector3(0f,1f,0f), new Vector3(1f,1f,0f)};
		int[] triangles = new int[]{ 2,0,1};
		msh.vertices = vertices;
		msh.triangles = triangles;
		Vector2[] uvs=new Vector2[] {new Vector2(0.5f,0.5f), new Vector2(0.5f,1), new Vector2(1,1)};
		msh.uv=uvs;
		tile.GetComponent<MeshFilter>().mesh = msh;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
