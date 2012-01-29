using UnityEngine;
using System.Collections;

public class Generation : MonoBehaviour {
	
	public GenerationCamera genCamera { get; private set; }
	public bool IsPlayable { get; set; }
	// Use this for initialization
	void Start () {
		genCamera = gameObject.GetComponentInChildren<GenerationCamera>();
		Assert.Test(genCamera != null, "No Generation Camera associated with the generation");
		GenerationManager.Instance.PushGeneration(this);
		if(IsPlayable)
		{
			this.gameObject.BroadcastMessage("Spawn");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(genCamera.ShownRatio == 0.0f && genCamera.FlaggedForDeath)
		{
			GenerationManager.Instance.PopGeneration();
			this.gameObject.BroadcastMessage("Unspawn");
			
			Destroy(gameObject);
		}
	}
}
