using UnityEngine;
using System.Collections;

public class ManagersManager : MonoBehaviour {
	public Texture2D Separator;
	
	void Start() 
	{
		GenerationManager.Instance.BorderTexture = Separator;
	}
	
	void Update()
	{
		GenerationManager.Instance.Update();
	}
	
	void OnGUI()
	{
		GenerationManager.Instance.OnGUI();
	}
}
