using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayZoneDetector : MonoBehaviour {

	public enum Effect {Freeze, Burn};
	public Effect effect;
	public float duration;
	public float strength;

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Enemy") {
			if (effect == Effect.Freeze) {
				collider.GetComponent<EnemyController> ().Freeze (duration, strength);
			} else if (effect == Effect.Burn) {
				collider.GetComponent<EnemyController> ().Burn (duration, strength);
			}
		} 
	}

}
