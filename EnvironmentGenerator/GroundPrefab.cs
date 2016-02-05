using UnityEngine;
using System.Collections;

public class GroundPrefab : PrefabMeta {

	public int width;
	// Use this for initialization
	void Start () {
	
	}
	
	public override float getPrefWidth(){
		return this.width;
	}

	public override Vector3 getPrefCenter(){
		Vector3 pos = new Vector3 (this.transform.position.x,transform.position.y,transform.position.z);
		return pos;
	}

	public override float getMaxBoundX(){
		return this.transform.position.x + this.width / 2;
	}
	public override float getMaxBoundY(){
		return 1;
	}
	public override float getMinBoundX(){
		return this.transform.position.x - this.width / 2;
	}
	public override float getMinBoundY(){
		return 1;
	}
}
