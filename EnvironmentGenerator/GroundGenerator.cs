using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GroundGenerator : EnviroGenerator {
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
		Debug.Log (tile_list.Last().name.ToString());
		base.currObj = base.tile_list.First.Value;
		base.map_length = currObj.GetComponent<GroundPrefab> ().getPrefWidth () / 2;
		base.currCamLimitX = -20;

	}

	//logic for generating next ground 
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

			//random the ground prefab here,add it in scene at origin point,and update linkedlist node references
			int random_index = Random.Range(0,pInstance.prefab_instance.Count);
			float nextPrefabWidth = pInstance.prefab_instance[random_index].GetComponent<GroundPrefab>().getPrefWidth();
			GameObject newobj=(GameObject) Instantiate(pInstance.prefab_instance[random_index],Vector3.zero,currObj.transform.rotation);
			newobj.transform.parent = pInstance.initObj.transform.parent;
			tile_list.AddLast(newobj);

			//set position of new prefab
			float newPosX = base.map_length + nextPrefabWidth/2 + margin + transform.position.x;
			Vector3 newPos = new Vector3(newPosX,currObj.transform.position.y,currObj.transform.position.z);
			newobj.transform.position = newPos;
			newobj.SetActive(true);
			base.map_length += nextPrefabWidth + margin; //update current static variable map_length
			
			 //update the current node
			base.currObj = tile_list.Last.Value;
			
			//setting limit trigger for next trigger
			currCamLimitX = (int)currObj.GetComponent<GroundPrefab>().getPrefCenter().x - smoothingOffset;
			mustGenerate = false;
			
		} else if(Mathf.RoundToInt (getCamLeftBound ()) != currCamLimitX){
			mustGenerate = true;		
		}

	}

	//remove behind camera visible area recursion with linked list
	protected override void removeNotVisible(){
				LinkedListNode<GameObject> prevTile = tile_list.Last.Previous;

				if (prevTile != null) {
					GameObject prevObj = prevTile.Value;
					int maxbound =(int)prevObj.GetComponent<GroundPrefab>().getMaxBoundX();
					if(Mathf.RoundToInt (getCamLeftBound ()) >= maxbound){
						tile_list.Remove(prevObj);
						Destroy(prevObj);
					}		
				}
		}

	protected override void generateFormula(){}

	// Update is called once per frame
	void Update () {
		generateNew ();
		removeNotVisible ();
	}
}
