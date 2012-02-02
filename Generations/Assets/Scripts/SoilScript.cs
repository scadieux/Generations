using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoilScript : MonoBehaviour
{
	public int soilId;
	public PackedSprite animations;
	public GenerationObject SoilObject { get; private set; }
	public GenerationObject GenerationObjectPrefab;
	
	void Awake()
	{
	}
	
	public void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, collider.bounds.size);
	}
		
	public void OnSoilMessage(SoilMessage message)
	{
		if (message.id != soilId) 
			return;
		
		SoilMessageLogic(message);
	}
	
	private void SoilMessageLogic(SoilMessage message)
	{
		Unspawn();
		if(message.soilObject != null)
		{
			SoilObject = (GenerationObject)Instantiate(message.soilObject, transform.position, Quaternion.identity);
			if(message.action == Action.INCREMENT_GENERATION && SoilObject.SetGenerationIndex(message.soilObject.generationIndex+message.numberOfGenerationIncrement))
			{
				AddGo(SoilObject.gameObject);
			}
			else if(message.action == Action.SET && SoilObject.SetGenerationIndex(message.soilObject.generationIndex))
			{
				message.soilObject.gameObject.transform.position = transform.position;
				AddGo(message.soilObject.gameObject);
			}
		}
	}
	
	public void Unspawn()
	{
		if(SoilObject != null)
		{
			SoilObject.Destroy();
			Destroy(SoilObject.gameObject);
		}
	}
	
	private void AddGo(GameObject go)
	{
		StartCoroutine(SetParentRountine(go));
	}
	
	IEnumerator SetParentRountine(GameObject go) // WTF HACK
	{
		yield return new WaitForEndOfFrame();
		
		Vector3 localScale = go.transform.localScale; 
		Generation gen = ComponentFinder.FindComponent<Generation>(transform, ComponentFinder.Direction.Upward);
		//go.transform.parent = gen.transform;
		localScale.x /= gen.transform.localScale.x;
		localScale.y /= gen.transform.localScale.y;
		go.transform.localScale = localScale;
	}
}