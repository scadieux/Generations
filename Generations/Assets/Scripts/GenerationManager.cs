
using System;
using System.Collections.Generic;
using UnityEngine;

public class GenerationManager
{
	private Queue<Message> messages = new Queue<Message>();
	
	public const int MAX_GENERATIONS = 3;
	const float PLAYABLE_GENERATION_RATIO = 1.2f;
	private static readonly GenerationManager instance = new GenerationManager();
	
	private List<Generation> genList;
	private List<float> viewportScaleY;
	private bool dirty;
	
	public bool IsTransitioning { get;  private set; }
	
	public Texture2D BorderTexture
	{
		get; set;
	}
	
	private GenerationManager() 
	{
		genList = new List<Generation>();
		viewportScaleY = new List<float>();
		IsTransitioning = false;
	}
	
	public static GenerationManager Instance
	{
		get 
		{
			return instance;
		}
	}
	
	public void ChangeSoilObject(int soilId, GenerationObject seedObject)
	{
		GenerationObject parentSoil = seedObject;
		
		SoilMessage m = new SoilMessage();
		m.id = soilId;
		m.soilObject = parentSoil;
		m.Broadcast(genList[MAX_GENERATIONS - 1].gameObject);
		
		for (int i = MAX_GENERATIONS - 2; i >= 0; --i)
		{
			m = new SoilMessage();
			m.id = soilId;
			m.soilObject = parentSoil;
			m.action = Action.INCREMENT_GENERATION;
			m.numberOfGenerationIncrement = MAX_GENERATIONS - i - 1;
			m.Broadcast(genList[i].gameObject);
		}
	}
	
	public void UnPlantSeed(int soilId)
	{
		for (int i = MAX_GENERATIONS - 1; i >= 0; --i)
		{
			SoilMessage m = new SoilMessage();
			m.id = soilId;
			m.soilObject = null;
			m.Broadcast(genList[i].gameObject);
		}
	}
	
	public void PushGeneration(Generation gen)
	{
		if(genList.Count == 0)
		{
			gen.genCamera.IsPlayableCamera = true;
			gen.IsPlayable = true;
		}
		else
		{
			SoilScript[] soils = genList[0].GetComponentsInChildren<SoilScript>();
			
			foreach(SoilScript soil in soils)
			{
				SoilMessage m = new SoilMessage();
				m.id = soil.soilId;
				m.soilObject = soil.SoilObject;
				m.action = Action.INCREMENT_GENERATION;
				m.Broadcast(gen.gameObject);
			}
		}
		
		if(genList.Count < MAX_GENERATIONS)
		{
			gen.genCamera.SkipTransition();
		}
		
		genList.Insert(0, gen);
		
		if(genList.Count > MAX_GENERATIONS)
		{
			genList[genList.Count - 1].genCamera.FlaggedForDeath = true;
			IsTransitioning = true;
		}
		
		while (messages.Count != 0)
		{
			messages.Dequeue().Broadcast(gen.gameObject);
		}
		
		dirty = true;		
	}
	
	public Generation GetPlayableGeneration()
	{
		return genList[genList.Count - 1];
	}
	
	public void PopGeneration()
	{
		genList.RemoveAt(genList.Count - 1);
		genList[genList.Count - 1].IsPlayable = true;
		genList[genList.Count - 1].BroadcastMessage("Spawn");
		dirty = true;
	}
	
	public void ClearGenerations()
	{
		foreach(Generation gen in genList)
		{
			foreach(SoilScript soil in gen.GetComponentsInChildren<SoilScript>())
			{
				soil.Unspawn();

			}
			GameObject.Destroy(gen.genCamera);
			GameObject.Destroy(gen.gameObject);
		}
		
		genList.Clear();
	}
	
	public void Update()
	{
		
		if(dirty)
		{
			dirty = false;
			
			float yViewportValueIncrement = 1.0f / (float)genList.Count;
			
			List<float> viewportScaleFactors = new List<float>(new float[genList.Count]);
			viewportScaleY.Clear();
			
			
			// Initialize
			for(int i = 0; i != genList.Count; ++i)
			{
				viewportScaleFactors[i] = yViewportValueIncrement;
            }
			
			// Adapt to resizable viewport
			for(int i = 0; i != genList.Count; ++i)
			{
				if(genList[i].genCamera.ShownRatio < 1.0f)
				{
					dirty = true; // Have to re-update
					float inverseRation = yViewportValueIncrement * (1.0f - genList[i].genCamera.ShownRatio);
					
					inverseRation /= (float)(genList.Count - 1);
					
					for(int j = 0; j != genList.Count; ++j)
					{
						if(i == j)
						{
							viewportScaleFactors[j] *= genList[i].genCamera.ShownRatio;
						}
						else
						{
							viewportScaleFactors[j] += inverseRation;
						}
					}
				}
            }
			
			// Adapt to playable viewport is bigger
			/*float viewportRatioIncrement = 0.0f;
			for(int j = 0; j != genList.Count; ++j)
			{
				viewportRatioIncrement = 0.0f;
				for(int i = j; i != genList.Count; ++i)
				{
					if(genList[j].genCamera.IsPlayableCamera){
						viewportRatioIncrement = (float)(viewportScaleFactors[i] * PLAYABLE_GENERATION_RATIO) - viewportScaleFactors[i];
						viewportRatioIncrement *= genList[i].genCamera.PlayableShowRatio;
						viewportScaleFactors[i] += viewportRatioIncrement;
						break;
					}
				}
				if(genList.Count > 1)
				{
					for(int i = 0; i != genList.Count; ++i)
					{
						if(i != j)
						{
							viewportScaleFactors[i] -= viewportRatioIncrement / (float)(genList.Count - 1);
						}
					}
				}
			}*/
			
			// Set actual value on camera
			float yViewportYOffset = 0.0f;
			viewportScaleY.InsertRange(0, viewportScaleFactors);
			for(int i = 0; i != genList.Count; ++i)
			{
				GenerationCamera cam = genList[i].genCamera;
				Rect rect = new Rect(cam.camera.rect.x, yViewportYOffset, cam.camera.rect.width, viewportScaleFactors[i]);
				cam.camera.orthographicSize = cam.OrthographicSize * viewportScaleY[i];
				cam.camera.rect = rect;
				
				yViewportYOffset += viewportScaleFactors[i];
			}
			
			IsTransitioning = dirty;
		}
	}
	
	public void OnGUI()
	{
		float totalY = 0;
		
		for(int i = 0; i < viewportScaleY.Count - 1; ++i)
		{
			totalY += viewportScaleY[i];
			int posX = (Screen.width - BorderTexture.width) / 2;
			GUI.DrawTexture(new Rect (posX,  (1.0f - totalY) * ManagersManager.Height - (BorderTexture.height / 2.0f) , BorderTexture.width, BorderTexture.height), BorderTexture);
		}
	}
}


