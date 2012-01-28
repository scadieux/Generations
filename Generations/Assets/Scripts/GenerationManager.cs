using System;
using System.Collections.Generic;
using UnityEngine;

public class GenerationManager
{
	const int MAX_GENERATIONS = 3;
	const float PLAYABLE_GENERATION_RATIO = 1.2f;
	private static readonly GenerationManager instance = new GenerationManager();
	
	private List<GenerationCamera> genCamList;
	private List<float> viewportScaleY;
	private bool dirty;
	
	public Texture2D BorderTexture
	{
		get; set;
	}
	
	private GenerationManager() 
	{
		genCamList = new List<GenerationCamera>();
		viewportScaleY = new List<float>();
	}
	
	public static GenerationManager Instance
	{
		get 
		{
			return instance;
		}
	}
	
	public void PushGenerationCamera(GenerationCamera cam)
	{
		if(genCamList.Count == 0)
		{
			cam.IsPlayableCamera = true;
		}
		genCamList.Add(cam);
		dirty = true;		
	}
	
	public void PopGenerationCamera()
	{
		genCamList.RemoveAt(0);
		dirty = true;
	}
	
	public void Update()
	{
		
		if(dirty)
		{
			//dirty = false;
			
			float yViewportValueIncrement = 1.0f / (float)genCamList.Count;
			
			viewportScaleY.Clear();
			viewportScaleY.InsertRange(0, new float[genCamList.Count]);
			
			// Initialize
			for(int i = 0; i != genCamList.Count; ++i)
			{
				viewportScaleY[i] = yViewportValueIncrement;
            }
			
			// Adapt to resizable viewport
			for(int i = 0; i != genCamList.Count; ++i)
			{
				if(genCamList[i].ShownRatio < 1.0f)
				{
					dirty = true; // Have to re-update
					float inverseRation = yViewportValueIncrement * (1.0f - genCamList[i].ShownRatio);
					
					inverseRation /= (float)(genCamList.Count - 1);
					
					for(int j = 0; j != genCamList.Count; ++j)
					{
						if(i == j)
						{
							viewportScaleY[j] *= genCamList[i].ShownRatio;
						}
						else
						{
							viewportScaleY[j] += inverseRation;
						}
					}
				}
            }
			
			// Adapt to playable viewport is bigger
			float viewportRatioIncrement = 0.0f;
			for(int j = 0; j != genCamList.Count; ++j)
			{
				viewportRatioIncrement = 0.0f;
				for(int i = j; i != genCamList.Count; ++i)
				{
					if(genCamList[i].IsPlayableCamera){
						viewportRatioIncrement = (float)(viewportScaleY[i] * PLAYABLE_GENERATION_RATIO) - viewportScaleY[i];
						viewportRatioIncrement *= genCamList[i].PlayableShowRatio;
						viewportScaleY[i] += viewportRatioIncrement;
						break;
					}
				}
				if(genCamList.Count > 1)
				{
					for(int i = 0; i != genCamList.Count; ++i)
					{
						if(i != j)
						{
							viewportScaleY[i] -= viewportRatioIncrement / (float)(genCamList.Count - 1);
						}
					}
				}
			}

			
			// Set actual value on camera
			float yViewportYOffset = 1.0f;
			for(int i = 0; i != genCamList.Count; ++i)
			{
				GenerationCamera cam = genCamList[i];
				Rect rect = new Rect(cam.camera.rect.x, yViewportYOffset - viewportScaleY[i], cam.camera.rect.width, viewportScaleY[i]);
				cam.camera.rect = rect;
				
				yViewportYOffset -= viewportScaleY[i];
			}
			
			if(genCamList.Count > MAX_GENERATIONS)
			{
				genCamList[0].FlaggedForDeath = true;
				genCamList[1].IsPlayableCamera = true;
			}
		}
	}
	
	public void OnGUI()
	{
		float totalY = 0;
		for(int i = 0; i != viewportScaleY.Count - 1; ++i)
		{
			totalY += viewportScaleY[i];
			GUI.DrawTexture(new Rect (0, totalY * Screen.height - (BorderTexture.height / 2.0f) , Screen.width, BorderTexture.height), BorderTexture);
		}
	}
}


