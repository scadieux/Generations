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
	bool Picking = false;
	
	bool WasIdle = true;
	
	bool IsAnimationLocked = false;
	bool WasAnimationLocked = false;
	float lastHorDir = 0.0f;
	
	bool WasGrounded = false;
	
	PackedSprite sprites;
	
	void Start ()
	{
		sprites = GetComponentInChildren<PackedSprite>();
	}
	
	// Update is called once per frame
	void Update () {
		JustJumped = GetComponent<CharacterController>().isGrounded && Input.GetButtonDown("Jump");
		
		JustLanded = !WasGrounded && GetComponent<CharacterController>().isGrounded;
		
		bool ActionOnce = JustPlanting || JustPicking || JustJumped;
		
		if(Input.GetAxis("Horizontal") != 0.0f && !ActionOnce && !IsJumping && !Planting && !Picking)
		{
			if(Mathf.Sign(lastHorDir) != Mathf.Sign(Input.GetAxis("Horizontal")))
			{
				lastHorDir =Input.GetAxis("Horizontal");
				transform.localScale = 
					new Vector3(
			            transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);
			}
			if(!IsRunning) PlayAnimation("Run");
			IsRunning = true;
			WasIdle = false;
		}
		else if(JustJumped)
		{
			IsJumping = true;
			IsRunning = false;
			WasIdle = false;
			PlayAnimation("Jump");
		}
		else if(!IsJumping && !ActionOnce && !Planting && !WasIdle && !Picking)
		{
			PlayAnimation("Idle");
			IsRunning = false;
			WasIdle = true;
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
			WasIdle = false;
		}
		else if(JustPicking || Picking)
		{
			if(!Picking)
			{
				PlayAnimation("Pickup");
				Picking = true;
				JustPicking = false;
			}
			else
			{
				Picking = sprites.IsAnimating();
			}
			WasIdle = false;
		}
		
		if(IsAnimationLocked != WasAnimationLocked)
		{
			GetComponentInChildren<PlayerAxisLock>().lockOnXAxis = IsAnimationLocked;
			GetComponentInChildren<PlayerAxisLock>().lockOnYAxis = IsAnimationLocked;
		}
		
		WasGrounded = GetComponent<CharacterController>().isGrounded;
	}      
	
	public void PlayAnimation(string name)
	{
		sprites.PlayAnim(name);
	}
	
	void OnTriggerStay(Collider collision)
	{
		SoilScript soil = collision.GetComponentInChildren<SoilScript>();
		if(soil != null && Input.GetButtonDown("PlantSeed"))
		{
			if(soil.state == SoilMessage.SoilState.Blank)
			{
				GenerationManager.Instance.PlantSeed(soil.soilId);
				JustPlanting = true;
			}
			else if(soil.state == SoilMessage.SoilState.Seed)
			{
				GenerationManager.Instance.UnPlantSeed(soil.soilId);
				JustPicking = true;
			}
		}
	}
}
