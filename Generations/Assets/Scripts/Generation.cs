using UnityEngine;
using System.Collections;

public class Generation : MonoBehaviour
{
	public GenerationCamera genCamera { get; private set; }
	public bool IsPlayable { get; set; }
	public PackedSprite background;
	public int order { get; set; }
	
	// Use this for initialization
	void Start () {
		SetupBackground();
		
		genCamera = gameObject.GetComponentInChildren<GenerationCamera>();
		Assert.Test(genCamera != null, "No Generation Camera associated with the generation");
		GenerationManager.Instance.PushGeneration(this);
		if(IsPlayable)
		{
			this.gameObject.BroadcastMessage("Spawn");
		}
	}
	
	void SetupBackground()
	{
		Assert.Test(background);
		background.PlayAnim(order % background.animations.Length);
		
		Vector3 localScale = transform.localScale;
		localScale.x /= background.width / (Screen.width * background.worldUnitsPerScreenPixel);
		localScale.y /= background.height / (Screen.height / 3 * background.worldUnitsPerScreenPixel);
		transform.localScale = localScale;
	}
	
	// Update is called once per frame
	void Update () {
		if(genCamera.ShownRatio == 0.0f && genCamera.FlaggedForDeath)
		{
			GenerationManager.Instance.PopGeneration();
			Destroy(gameObject);
		}
	}
}
