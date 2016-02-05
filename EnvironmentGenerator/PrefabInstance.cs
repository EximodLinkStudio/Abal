using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PrefabInstance : MonoBehaviour {
	public GameObject[] prefab_list; 
	[HideInInspector]public List<GameObject> prefab_instance = new List<GameObject>();
	public GameObject initObj;
	// Use this for initialization
	void Start () {
		makeInstance ();
	}

	public void makeInstance(){
		for (int i=0; i<prefab_list.Length; i++) {
			GameObject newObj = (GameObject)Instantiate(prefab_list[i],initObj.transform.position,initObj.transform.rotation);
			prefab_instance.Add(newObj);
			prefab_instance.Last().SetActive(false);
		}
	}

}
