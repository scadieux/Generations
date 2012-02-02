using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GenerationObjectType
{
	Unrelevant,
	Tree,
}

public class GenerationObject : MonoBehaviour {
	public bool Loops;
	public GenerationObjectType Type;
	
	public List<GameObject> GenerationPrebabs;
	private GameObject instantiatedPrefab = null;
	
	private int _generationIndex = 0;
	public int generationIndex 
	{ 
		get { return _generationIndex; } 
		private set { _generationIndex = value; } 
	}
	
	public bool SetGenerationIndex(int index)
	{
		bool valid = true;
		// If index is out of range
		if(index >= GenerationPrebabs.Count)
		{
			if(Loops)
			{
				// Loop index
				generationIndex = index - GenerationPrebabs.Count;
			}
			else
			{
				// Destroy object
				Destroy(gameObject);
				valid = false;
			}
		}
		else
		{
			generationIndex = index;
		}
		
		if(valid)
		{
			instantiatedPrefab = (GameObject)Instantiate(GenerationPrebabs[generationIndex], transform.position, Quaternion.identity);
		}
		
		return valid;
	}
	
	public void Destroy()
	{
		if(instantiatedPrefab != null)
		{
			Destroy(instantiatedPrefab);
		}
	}
}
