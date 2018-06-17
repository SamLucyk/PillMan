using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MenuController{

	public static GameObject FindObject(this GameObject parent, string name)
	{
		Component[] trs = parent.GetComponentsInChildren(typeof(Transform), true);
		foreach(Component t in trs){
			if(t.name == name){
				return t.gameObject;
			}
		}
		return null;
	}

}
