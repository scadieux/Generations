using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoilScript : MonoBehaviour
{
	public int soilId;
	private SoilMessage.SoilState state = SoilMessage.SoilState.Blank;
	public PackedSprite animations;
	public GameObject seedPrefab;
	public GameObject trunkColliderPrefab;
	public GameObject logColliderPrefab;
	public GameObject babyTreeColliderPrefab;
	public GameObject adultTreeColliderPrefab;
	public GameObject oldTreeColliderPrefab;
	public float trunkAngle = 90;
	public Vector3 logSpawnPoint;
	public Vector3 trunkPivot;
	
	private List<GameObject> spawns;
	
	void Awake()
	{
		spawns = new List<GameObject>();
	}
	
	public void OnDrawGizmos()
	{
		Gizmos.DrawLine(transform.position + trunkPivot, transform.position + trunkPivot + new Vector3(Mathf.Cos(trunkAngle * Mathf.Deg2Rad), Mathf.Sin(trunkAngle * Mathf.Deg2Rad), trunkPivot.z));
		Gizmos.DrawWireCube(transform.position + logSpawnPoint, Vector3.one * 0.5f);
		Gizmos.DrawWireCube(transform.position, collider.bounds.size);
	}
		
	public void OnSoilMessage(SoilMessage message)
	{
		if (message.id != soilId) 
			return;
		
		StartCoroutine(BigBerthaRoutine(message));
	}
	
	private IEnumerator BigBerthaRoutine(SoilMessage message)
	{
		if (state == SoilMessage.SoilState.Blank)
		{
			if (message.state == SoilMessage.SoilState.LogAndTrunk)
			{
				if (trunkAngle < -90)
				{
					SpawnLog();
					SpawnTrunk();
					animations.PlayAnim("BlankToLogAndTrunkMinus135");
					while (animations.IsAnimating())
						yield return new WaitForEndOfFrame();
				}
				if (trunkAngle < -45)
				{
					SpawnLog();
					SpawnTrunk();
					animations.PlayAnim("BlankToLogAndTrunkMinus90");
					while (animations.IsAnimating())
						yield return new WaitForEndOfFrame();
				}
				else if (trunkAngle < 45)
				{
					SpawnLog();
					SpawnTrunk();
					animations.PlayAnim("BlankToLogAndTrunkMinus45");
					while (animations.IsAnimating())
						yield return new WaitForEndOfFrame();
				}
				else if (trunkAngle < 90)
				{
					SpawnLog();
					SpawnTrunk();
					animations.PlayAnim("BlankToLogAndTrunk45");
					while (animations.IsAnimating())
						yield return new WaitForEndOfFrame();
				}
				else if (trunkAngle < 135)
				{
					SpawnLog();
					SpawnTrunk();
					animations.PlayAnim("BlankToLogAndTrunk90");
					while (animations.IsAnimating())
						yield return new WaitForEndOfFrame();
					
				}
				else
				{
					SpawnLog();
					SpawnTrunk();
					animations.PlayAnim("BlankToLogAndTrunk135");
					while (animations.IsAnimating())
						yield return new WaitForEndOfFrame();
				}
			}
			else if (message.state == SoilMessage.SoilState.BabyTree)
			{
				SpawnBabyTree();
				animations.PlayAnim("BlankToBabyTree");
				while (animations.IsAnimating())
						yield return new WaitForEndOfFrame();
			}
			else if (message.state == SoilMessage.SoilState.AdultTree)
			{
				SpawnAdultTree();
				animations.PlayAnim("BlankToAdultTree");
				while (animations.IsAnimating())
						yield return new WaitForEndOfFrame();
			}
			else if (message.state == SoilMessage.SoilState.OldTree)
			{
				SpawnOldTree();
				animations.PlayAnim("BlankToOldTree");
				while (animations.IsAnimating())
						yield return new WaitForEndOfFrame();
			}
			else if (message.state == SoilMessage.SoilState.Seed)
			{
				SpawnSeed();
			}
			else
			{
				Logger.Error("Unsupported transition : {0} -> {1}", state, message.state);
			}
		}
		else if (state == SoilMessage.SoilState.BabyTree)
		{
			if (message.state == SoilMessage.SoilState.Log)
			{
				SpawnLog();
				animations.PlayAnim("BabyTreeToLog");
				while (animations.IsAnimating())
						yield return new WaitForEndOfFrame();
			}
			else
			{
				Logger.Error("Unsupported transition : {0} -> {1}", state, message.state);
			}
		}
		else if (state == SoilMessage.SoilState.AdultTree)
		{
			if (message.state == SoilMessage.SoilState.Log)
			{
				SpawnLog();
				animations.PlayAnim("AdultTreeToLog");
				while (animations.IsAnimating())
						yield return new WaitForEndOfFrame();
			}
			else if (message.state == SoilMessage.SoilState.LogAndTrunk)
			{
				if (trunkAngle < -90)
				{
					SpawnLog();
					SpawnTrunk();
					animations.PlayAnim("AdultTreeToLogAndTrunkMinus135");
					while (animations.IsAnimating())
						yield return new WaitForEndOfFrame();
				}
				if (trunkAngle < -45)
				{
					SpawnLog();
					SpawnTrunk();
					animations.PlayAnim("AdultTreeToLogAndTrunkMinus90");
					while (animations.IsAnimating())
						yield return new WaitForEndOfFrame();
				}
				else if (trunkAngle < 45)
				{
					SpawnLog();
					SpawnTrunk();
					animations.PlayAnim("AdultTreeToLogAndTrunkMinus45");
					while (animations.IsAnimating())
						yield return new WaitForEndOfFrame();
				}
				else if (trunkAngle < 90)
				{
					SpawnLog();
					SpawnTrunk();
					animations.PlayAnim("AdultTreeToLogAndTrunk45");
					while (animations.IsAnimating())
						yield return new WaitForEndOfFrame();
				}
				else if (trunkAngle < 135)
				{
					SpawnLog();
					SpawnTrunk();
					animations.PlayAnim("AdultTreeToLogAndTrunk90");
					while (animations.IsAnimating())
						yield return new WaitForEndOfFrame();
				}
				else
				{
					SpawnLog();
					SpawnTrunk();
					animations.PlayAnim("AdultTreeToLogAndTrunk135");
					while (animations.IsAnimating())
						yield return new WaitForEndOfFrame();
				}
			}
			else
			{
				Logger.Error("Unsupported transition : {0} -> {1}", state, message.state);
			}
		}
		else if (state == SoilMessage.SoilState.OldTree)
		{
			if (message.state == SoilMessage.SoilState.Blank)
			{
				Unspawn();
				animations.PlayAnim("OldTreeToBlank");
				while (animations.IsAnimating())
						yield return new WaitForEndOfFrame();
			}
			else if (message.state == SoilMessage.SoilState.LogAndTrunk)
			{
				if (trunkAngle < -90)
				{
					SpawnLog();
					SpawnTrunk();
					animations.PlayAnim("OldTreeToLogAndTrunkMinus135");
					while (animations.IsAnimating())
						yield return new WaitForEndOfFrame();
				}
				if (trunkAngle < -45)
				{
					SpawnLog();
					SpawnTrunk();
					animations.PlayAnim("OldTreeToLogAndTrunkMinus90");
					while (animations.IsAnimating())
						yield return new WaitForEndOfFrame();
				}
				else if (trunkAngle < 45)
				{
					SpawnLog();
					SpawnTrunk();
					animations.PlayAnim("OldTreeToLogAndTrunkMinus45");
					while (animations.IsAnimating())
						yield return new WaitForEndOfFrame();
				}
				else if (trunkAngle < 90)
				{
					SpawnLog();
					SpawnTrunk();
					animations.PlayAnim("OldTreeToLogAndTrunk45");
					while (animations.IsAnimating())
						yield return new WaitForEndOfFrame();
				}
				else if (trunkAngle < 135)
				{
					SpawnLog();
					SpawnTrunk();
					animations.PlayAnim("OldTreeToLogAndTrunk90");
					while (animations.IsAnimating())
						yield return new WaitForEndOfFrame();
				}
				else
				{
					SpawnLog();
					SpawnTrunk();
					animations.PlayAnim("OldTreeToLogAndTrunk135");
					while (animations.IsAnimating())
						yield return new WaitForEndOfFrame();
				}
			}
			else
			{
				Logger.Error("Unsupported transition : {0} -> {1}", state, message.state);
			}
		}
		else
		{
			Logger.Error("Unsupported transition : {0} -> {1}", state, message.state);
		}
	}
	
	private void Unspawn()
	{
		foreach (GameObject go in spawns)
			Destroy(go);
		spawns.Clear();
	}
	
	private void SpawnLog()
	{
		Unspawn();
		AddGo(Instantiate(logColliderPrefab, transform.position + logSpawnPoint, Quaternion.identity) as GameObject);
	}
	
	private void SpawnTrunk()
	{
		Unspawn();
		GameObject go = Instantiate(trunkColliderPrefab, transform.position + logSpawnPoint, Quaternion.identity) as GameObject;
		go.transform.LookAt(new Vector3(Mathf.Cos(trunkAngle * Mathf.Deg2Rad), Mathf.Sin(trunkAngle * Mathf.Deg2Rad), trunkPivot.z) * 10);
		AddGo(go);
	}
	
	private void SpawnBabyTree()
	{
		Unspawn();
		AddGo(Instantiate(babyTreeColliderPrefab, transform.position + logSpawnPoint, Quaternion.identity) as GameObject);
	}
	
	private void SpawnAdultTree()
	{
		Unspawn();
		AddGo(Instantiate(adultTreeColliderPrefab, transform.position + logSpawnPoint, Quaternion.identity) as GameObject);
	}
	
	private void SpawnOldTree()
	{
		Unspawn();
		AddGo(Instantiate(oldTreeColliderPrefab, transform.position + logSpawnPoint, Quaternion.identity) as GameObject);
	}
	
	private void SpawnSeed()
	{
		Unspawn();
		AddGo(Instantiate(seedPrefab, transform.position + logSpawnPoint, Quaternion.identity) as GameObject);
	}
	
	
	private void AddGo(GameObject go)
	{
		StartCoroutine(SetParentRountine(go));
		spawns.Add(go);
	}
	
	IEnumerator SetParentRountine(GameObject go) // WTF HACK
	{
		yield return new WaitForEndOfFrame();
		
		Vector3 localScale = go.transform.localScale; 
		Generation gen = ComponentFinder.FindComponent<Generation>(transform, ComponentFinder.Direction.Upward);
		go.transform.parent = gen.transform;
		localScale.x /= gen.transform.localScale.x;
		localScale.y /= gen.transform.localScale.y;
		go.transform.localScale = localScale;
	}
}