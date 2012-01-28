using UnityEngine;
using System.Collections;

public class ManagersManager : MonoBehaviour {
	public GameObject GenerationCameraPrefab;
	
	public Texture2D Separator;
	
	float timeElapsed = 0.0f;
	
	void Start() 
	{
		GenerationManager.Instance.BorderTexture = Separator;
	}
	
	// Update is called once per frame
	void Update () {
		GenerationManager.Instance.Update();
		
		timeElapsed += Time.deltaTime;
		if(timeElapsed > 5.0f)
		{
			timeElapsed -= 20.0f;
			//GameObject.Instantiate(GenerationCameraPrefab);
		}
		
	}
	
	void OnGUI()
	{
		GenerationManager.Instance.OnGUI();
	}
}
