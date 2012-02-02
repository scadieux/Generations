using UnityEngine;
using System.Collections;

public class ManagersManager : MonoBehaviour {
	public Texture2D Separator;
	
	public static PuzzleManager PuzzleManagerInstance = null;
	
	static private float ScreenRatio = 512.0f / 768.0f; // Width / Height
	
	static public int Width 
	{
		get
		{
			bool sizeByHeight = Screen.height * ScreenRatio <= Screen.width;
			if(sizeByHeight)
			{
				return (int)((float)Screen.height * ScreenRatio);
			}
			else
			{
				return Screen.width;
			}
		}
	}
	
	static public int Height
	{
		get
		{
			bool sizeByHeight = Screen.height * ScreenRatio <= Screen.width;
			if(sizeByHeight)
			{
				return Screen.height;
			}
			else
			{
				return (int)((float)Screen.width / ScreenRatio);
			}
		}
	}
	
	void Start() 
	{
		GenerationManager.Instance.BorderTexture = Separator;
		PuzzleManagerInstance = GetComponent<PuzzleManager>();
		Assert.Test(PuzzleManagerInstance != null, "Missing PuzzleManager component with ManagersManager");
		
	}
	
	void Update()
	{
		GenerationManager.Instance.Update();
		
		if(Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
	
	void OnGUI()
	{
		GenerationManager.Instance.OnGUI();
	}
}
