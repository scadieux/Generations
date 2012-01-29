using UnityEngine;
using System.Collections;

public class Puzzle : MonoBehaviour
{
	private Generation generationPrefab;
	private int genIndex = 0;
	private int generationCount = 0;
	
	readonly Vector3[] puzzleSpawnLocation = new Vector3[4]
	{
		 new Vector3(0.0f, 100.0f, 0.0f),
		 new Vector3(0.0f, 0.0f, 0.0f),
		 new Vector3(0.0f, -100.0f, 0.0f),
		new Vector3(0.0f, -200.0f, 0.0f),
	};

	public void StartPuzzle(Generation generation)
	{
		generationPrefab = generation;
		((Generation)Instantiate(generationPrefab,puzzleSpawnLocation[0], Quaternion.identity)).order = generationCount++;
		((Generation)Instantiate(generationPrefab,puzzleSpawnLocation[1], Quaternion.identity)).order = generationCount++;
		((Generation)Instantiate(generationPrefab,puzzleSpawnLocation[2], Quaternion.identity)).order = generationCount++;
	}
	
	void Update()
	{
		if(Input.GetButtonDown("NextGeneration"))
		{
			if(!GenerationManager.Instance.IsTransitioning)
			{
				((Generation)Instantiate(generationPrefab,puzzleSpawnLocation[genIndex], Quaternion.identity)).order = generationCount++;
				genIndex = (genIndex + 1) % puzzleSpawnLocation.Length;
			}
		}
	}
}
