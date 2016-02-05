using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundGenerator : EnviroGenerator {
	private PrefabInstance pInstance;
	// Use this for initialization
	void Start () {
		pInstance = GetComponent<PrefabInstance>();
		initSetting ();
	}

	public override void initSetting(){
		GameObject go = (GameObject)Instantiate (pInstance.initObj,pInstance.initObj.transform.position,pInstance.initObj.transform.rotation);
		go.SetActive (false);
		base.tile_list.AddFirst (go);
		base.currObj = base.tile_list.First.Value;
		base.map_length = currObj.GetComponent<SpriteRenderer> ().bounds.size.x / 2;
		base.currCamLimitX = (int)currObj.GetComponent<SpriteRenderer> ().bounds.center.x;
	}

	protected override void generateNew(){
		if (Mathf.RoundToInt (getCamLeftBound ()) == currCamLimitX && mustGenerate) {
			
			//set random margin beetwen 2 values
			int margin = 0; 
			if(fixedMargin){
				margin = fix_margin;
			}
			else{
				margin = (int)Random.Range(min_margin,max_margin);
			}
			
			//random the prefab here and add it in scene at origin point
			int random_index = Random.Range(0,pInstance.prefab_instance.Count);

			float nextPrefabWidth = pInstance.prefab_instance[random_index].GetComponent<SpriteRenderer>().bounds.size.x;
			GameObject newobj=(GameObject) Instantiate(pInstance.prefab_instance[random_index],Vector3.zero,currObj.transform.rotation);
			newobj.transform.parent = pInstance.initObj.transform.parent;
			
			//set position of new prefab
			float newPosX = base.map_length + nextPrefabWidth/2 + margin + transform.position.x;
			Vector3 newPos = new Vector3(newPosX,currObj.transform.position.y,currObj.transform.position.z);
			newobj.transform.position = newPos;
			newobj.SetActive(true);
			base.map_length += nextPrefabWidth + margin; //update current variable map_length
			
			tile_list.AddLast(newobj); //update the last node of linkedlist
			base.currObj = tile_list.Last.Value;
			
			//setting limit trigger for next trigger
			currCamLimitX = (int)currObj.transform.position.x - smoothingOffset;
			mustGenerate = false;
			
		} else if(Mathf.RoundToInt (getCamLeftBound ()) != currCamLimitX){
			mustGenerate = true;		
		}
	}
	protected override void removeNotVisible(){
		LinkedListNode<GameObject> prevTile = tile_list.Last.Previous;
		if (prevTile != null) {
			GameObject prevObj = prevTile.Value;
			int maxbound =(int)prevObj.GetComponent<SpriteRenderer>().bounds.max.x;
			if(Mathf.RoundToInt (getCamLeftBound ()) >= maxbound+5){
				tile_list.Remove(prevObj);
				Destroy(prevObj);
			}		
		}
	}
	protected override void generateFormula(){

	}
	
	// Update is called once per frame
	void Update () {
		removeNotVisible ();
		generateNew ();
	}
}
