using UnityEngine;
using System.Collections;

public class PlayerAxisLock : MonoBehaviour {
	
	public bool lockOnXAxis;
	public bool lockOnYAxis;
	public bool lockOnZAxis;
	
	private Vector3 initialPosition;
	
	// Use this for initialization
	void Start () {
		initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(lockOnXAxis)
		{
			transform.position = new Vector3(initialPosition.x, transform.position.y, transform.position.z);
		}
		if(lockOnYAxis)
		{
			transform.position = new Vector3(transform.position.x, initialPosition.y, transform.position.z);
		}
		if(lockOnZAxis)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, initialPosition.z);
		}
	}
}
