using UnityEngine;
using System.Collections;

public class GenerationCamera : MonoBehaviour {
	public bool ShownRatioEnabled = true;
	public float ShownRatioIncrease; //PerSeconds
	public float PlayableShownRatioIncrease; //PerSeconds
	public float OrthographicSize;
	
	public float ShownRatio 
	{
		get; 
		private set;
	}
	
	public float PlayableShowRatio
	{
		get; 
		private set;
	}
	
	public bool FlaggedForDeath
	{
		get; 
		set;
	}
	
	public bool IsPlayableCamera
	{
		get; 
		set;
	}
	
	// Use this for initialization
	void Start () 
	{
		FlaggedForDeath = false;
		PlayableShowRatio = ShownRatioEnabled ? 0.0f : 1.0f;
		ShownRatio = ShownRatioEnabled ? 0.0f : 1.0f;
		camera.backgroundColor = new Color(Random.value, Random.value, Random.value);
	}
	
	// Update is called once per frame
	void Update () {
		if(!FlaggedForDeath)
		{
			ShownRatio = Mathf.Min(ShownRatio + ShownRatioIncrease * Time.deltaTime, 1.0f);
			if(IsPlayableCamera)
			{
				PlayableShowRatio = Mathf.Min(PlayableShowRatio + PlayableShownRatioIncrease * Time.deltaTime, 1.0f);
			}
			else
			{
				PlayableShowRatio = Mathf.Max(PlayableShowRatio - PlayableShownRatioIncrease * Time.deltaTime, 0.0f);
			}
		}
		else
		{
			ShownRatio = Mathf.Max(ShownRatio - ShownRatioIncrease * Time.deltaTime, 0.0f);
		}
	}
}
