using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {
	public GameObject PlayerPrefab;
	private GameObject playerInstance;
	
	void Spawn()
	{
		playerInstance = Instantiate(PlayerPrefab, transform.position, Quaternion.identity) as GameObject;
		StartCoroutine(SetParentRountine(playerInstance));
	}
	
	void Unspawn()
	{
		Destroy(playerInstance.gameObject);
	}
	
	IEnumerator SetParentRountine(GameObject go) // WTF HACK
	{
		yield return new WaitForEndOfFrame();
		Vector3 localScale = go.transform.localScale;
		go.transform.parent = transform.parent;
		localScale.x /= transform.parent.localScale.x;
		localScale.y /= transform.parent.localScale.y;
		go.transform.localScale = localScale;
		go.collider.transform.localScale = localScale;
	}
}
