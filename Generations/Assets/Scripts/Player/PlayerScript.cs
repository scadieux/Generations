using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	// Use this for initialization
	
	bool IsJumping = false; // True while in the air
	bool JustJumped = false; // True on the frame he jumped
	bool JustLanded = false; // True on the frame he landed
	bool IsRunning = false;
	
	bool JustPlanting = false;
	bool Planting = false;
	
	bool JustPicking = false;
	bool WasPicking = false;
	
	bool IsAnimationLocked = false;
	bool WasAnimationLocked = false;
	
	PackedSprite sprites;
	
	void Start ()
	{
		sprites = GetComponentInChildren<PackedSprite>();
	}
	
	// Update is called once per frame
	void Update () {
		WasPicking = JustPicking;
		
		JustPlanting = Input.GetButtonDown("PlantSeed");
		
		bool ActionOnce = JustPlanting || JustPicking || JustJumped;
		
		if(Input.GetAxis("Horizontal") != 0.0f && !ActionOnce && !Planting)
		{
			transform.localScale = 
				new Vector3(
		            (Input.GetAxis("Horizontal") > 0 ? 1.0f : -1.0f), 1.0f, 1.0f);
			if(!IsRunning) PlayAnimation("Run");
			IsRunning = true;
		}
		else if(JustJumped)
		{
			IsJumping = true;
			IsRunning = false;
		}
		else if(!IsJumping && !ActionOnce && !Planting)
		{
			PlayAnimation("Idle");
			IsRunning = false;
		}
		else if(JustLanded && !IsAnimationLocked)
		{
			IsJumping = false;
			PlayAnimation("Idle");
		}
		else if(JustPlanting || Planting)
		{
			if(!Planting) 
			{
				PlayAnimation("Plant");
				Planting = true;
				JustPlanting = false;
			}
			else
			{
				Planting = sprites.IsAnimating();
			}
		}
		else if(JustPicking)
		{
			if(!JustPicking) PlayAnimation("Pickup");
			JustPicking = sprites.IsAnimating();
		}
		
		if(IsAnimationLocked != WasAnimationLocked)
		{
			GetComponentInChildren<PlayerAxisLock>().lockOnXAxis = IsAnimationLocked;
			GetComponentInChildren<PlayerAxisLock>().lockOnYAxis = IsAnimationLocked;
		}
	}      
	
	public void PlayAnimation(string name)
	{
		sprites.PlayAnim(name);
	}
	
	void OnTriggerStay(Collider collision)
	{
		SoilScript soil = collision.GetComponentInChildren<SoilScript>();
		if(soil != null && soil.enabled && Input.GetButtonDown("PlantSeed"))
		{
			GenerationManager.Instance.PlantSeed(soil.soilId);
		}
	}
}
