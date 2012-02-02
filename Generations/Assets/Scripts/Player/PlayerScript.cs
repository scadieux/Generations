using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	public static PlayerScript PlayerInstance;
	bool IsJumping = false; // True while in the air
	bool JustJumped = false; // True on the frame he jumped
	bool JustLanded = false; // True on the frame he landed
	bool IsRunning = false;
	
	bool JustPlanting = false;
	bool Planting = false;
	
	bool JustPicking = false;
	bool Picking = false;
	
	bool JustAxing = false;
	bool Axing = false;
	
	bool WasIdle = true;
	
	bool IsAnimationLocked = false;
	bool WasAnimationLocked = false;
	float lastHorDir = 0.0f;
	
	bool WasGrounded = false;
	
	PackedSprite sprites;
	
	void Start ()
	{
		sprites = GetComponentInChildren<PackedSprite>();
		PlayerInstance= this;
	}
	
	// Update is called once per frame
	void Update () {
		JustJumped = GetComponent<CharacterController>().isGrounded && Input.GetButtonDown("Jump");
		
		JustLanded = !WasGrounded && GetComponent<CharacterController>().isGrounded;
		
		bool ActionOnce = JustPlanting || JustPicking || JustJumped || JustAxing;
		bool PlayerSkipWalkCycle = ActionOnce || IsJumping || Planting || Picking || Axing;
		
		if(Input.GetAxis("Horizontal") != 0.0f && !PlayerSkipWalkCycle)
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
		else if(!IsJumping && !WasIdle && !PlayerSkipWalkCycle)
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
		else if(JustAxing || Axing)
		{
			if(!Axing)
			{
				PlayAnimation("Axe");
				Axing = true;
				JustAxing = false;
			}
			else
			{
				Axing = sprites.IsAnimating();
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
	
	void Chop(SoilScript chopSoil)
	{
		Tree treeComponent = chopSoil.SoilObject.GenerationPrebabs[chopSoil.SoilObject.generationIndex].GetComponent<Tree>();
		
		if(treeComponent != null && treeComponent.IsChoppable)
		{
			JustAxing = true;
		
			GenerationManager.Instance.ChangeSoilObject(chopSoil.soilId, treeComponent.ChoppedPrefab);
		}
	}
	
	void OnTriggerStay(Collider collision)
	{
		SoilScript soil = collision.GetComponentInChildren<SoilScript>();
		if(soil != null && Input.GetButtonDown("PlantSeed"))
		{
			if(soil.SoilObject == null)
			{
				GenerationManager.Instance.ChangeSoilObject(soil.soilId, soil.GenerationObjectPrefab);
				JustPlanting = true;
			}
			else if(soil.SoilObject.Type == GenerationObjectType.Tree && soil.SoilObject.generationIndex == 0) // We need a system so log don't get pickedup
			{
				GenerationManager.Instance.UnPlantSeed(soil.soilId);
				JustPicking = true;
			}
		}
		
		if(Input.GetButtonDown("Chop"))
		{
			Chop(soil);
		}
	}
}
