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
		int sinWave = (int)Mathf.Sin(order * Mathf.PI / 2.0f - Mathf.PI / 2.0f) + 1;
		background.PlayAnim(sinWave);
		
		Vector3 localScale = transform.localScale;
		localScale.x /= background.width / (ManagersManager.Width * background.worldUnitsPerScreenPixel);
		localScale.y /= background.height / (ManagersManager.Height / 3 * background.worldUnitsPerScreenPixel);
		transform.localScale = localScale * 1.01f;
	}
	
	// Update is called once per frame
	void Update () {
		if (genCamera == null)
		{	
			Logger.Warn("FRANK FAIT DU CODE PAS PROPRE!!!");
		
			return;
		}
		
		if(genCamera.ShownRatio == 0.0f && genCamera.FlaggedForDeath)
		{
			GenerationManager.Instance.PopGeneration();
			this.gameObject.BroadcastMessage("Unspawn");
			
			Destroy(gameObject);
			
			
		}
	}
}
