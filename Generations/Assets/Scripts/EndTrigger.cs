using UnityEngine;
using System.Collections;

public class EndTrigger : MonoBehaviour {
	public string NextLevelName;
	
	void OnTriggerEnter()
	{
		GenerationManager.Instance.ClearGenerations();
		ManagersManager.PuzzleManagerInstance.NextPuzzle();
	}
}
