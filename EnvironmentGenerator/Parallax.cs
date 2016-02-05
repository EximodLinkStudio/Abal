using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {
	public int offset;
	private float camInitPosX;
	// Use this for initialization
	void Start () {
		camInitPosX = Camera.main.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 translateVector = new Vector3((camInitPosX - Camera.main.transform.position.x)/offset,0,0);
		transform.position = translateVector;
	}
}
