using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerStay(Collider collision)
	{
		SoilScript soil = collision.GetComponentInChildren<SoilScript>();
		if(soil != null && soil.enabled && Input.GetButtonDown("PlaintSeed"))
		{
			GenerationManager.Instance.PlantSeed(soil.soilId);
		}
	}
}
