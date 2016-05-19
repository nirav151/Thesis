using UnityEngine;
using System.Collections;

namespace HyperbolicGeometry{
public class TileRelation : MonoBehaviour {

	public static int[,] tilemap;
	// Use this for initialization
	void Start () {
		int[,] tilemap = new int[6, 2] { { 0, 102 }, { 5, 101 }, { 12, 104 }, { 11, 107 },{10,109} ,{9,110}};
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
}