using UnityEngine;
using System.Collections;

public class Puzzle : MonoBehaviour
{
	private Generation generationPrefab;
	private int genIndex = 0;
	
	readonly Vector3[] puzzleSpawnLocation = new Vector3[3]
	{
		 new Vector3(0.0f, 100.0f, 0.0f),
		 new Vector3(0.0f, 0.0f, 0.0f),
		 new Vector3(0.0f, -100.0f, 0.0f),
	};

	public void StartPuzzle(Generation generation)
	{
		generationPrefab = generation;
		Instantiate(generationPrefab,puzzleSpawnLocation[0], Quaternion.identity);
		Instantiate(generationPrefab,puzzleSpawnLocation[1], Quaternion.identity);
		Instantiate(generationPrefab,puzzleSpawnLocation[2], Quaternion.identity);
	}
	
	void Update()
	{
		if(Input.GetButtonDown("NextGeneration"))
		{
			if(!GenerationManager.Instance.IsTransitioning)
			{
				Instantiate(generationPrefab, puzzleSpawnLocation[genIndex], Quaternion.identity);
				genIndex = (genIndex + 1) % GenerationManager.MAX_GENERATIONS;
			}
		}
	}
}
