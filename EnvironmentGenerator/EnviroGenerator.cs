using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class EnviroGenerator : MonoBehaviour {

	public int smoothingOffset;
	protected int currCamLimitX;
	protected float map_length;
	public bool fixedMargin = true;
	public int fix_margin,min_margin,max_margin;
	protected GameObject currObj;
	protected bool mustGenerate = true;

	public abstract void initSetting();

	//linked list for best performance in adding n removing tile
	protected LinkedList<GameObject> tile_list = new LinkedList<GameObject>();

	//get X Left bound of field of view camera
	protected float getCamLeftBound(){
		return Camera.main.transform.position.x - (Camera.main.orthographicSize * (Screen.width/Screen.height)); 
	}

	//get X Right bound of field of view camera
	protected float getCamRightBound(){
		return Camera.main.transform.position.x + (Camera.main.orthographicSize * (Screen.width/Screen.height)); 
	}

	protected abstract void generateNew(); //edit your generate algorithm here
	protected abstract void removeNotVisible();//edit your removing invisible area algorithm here
	protected abstract void generateFormula();//edit your generate algorithm here if you have generating formula
}
