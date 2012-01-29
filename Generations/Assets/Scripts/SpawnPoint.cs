using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {
	public GameObject PlayerPrefab;
	
	void Spawn()
	{
		Instantiate(PlayerPrefab, transform.position, Quaternion.identity);
	}
}
