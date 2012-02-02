using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PuzzleManager : MonoBehaviour {
	
	public Puzzle puzzlePrefab;
	public List<Generation> puzzles;
	public int index = 0;
	private Puzzle puzzleInstance;
	
	// Use this for initialization
	void Start () {
		puzzleInstance = (Puzzle)Instantiate(puzzlePrefab);
		puzzleInstance.StartPuzzle(puzzles[index]);
	}
	
	// Update is called once per frame
	void Update () {
	}
		
	public void NextPuzzle()
	{
		++index;
		if(index < puzzles.Count)
		{
			Destroy(puzzleInstance);
			puzzleInstance = (Puzzle)Instantiate(puzzlePrefab);
			puzzleInstance.StartPuzzle(puzzles[index]);
		}
		else
		{
			Application.LoadLevel("Intro");
		}
	}
}
