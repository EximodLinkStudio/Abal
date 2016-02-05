using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class PrefabMeta:MonoBehaviour {
	
	public GameObject getPrefab(){
		return gameObject;
	}

	public abstract float getPrefWidth();
	public abstract Vector3 getPrefCenter();
	public abstract float getMaxBoundX();
	public abstract float getMaxBoundY();
	public abstract float getMinBoundX();
	public abstract float getMinBoundY();
}
