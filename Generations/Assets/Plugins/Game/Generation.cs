using UnityEngine;
using System.Collections;

public class Generation : MonoBehaviour
{
	public PackedSprite background;
	public float width;
	public float height;
	
	void Start()
	{
		Assert.Test(background);
		background.SetSize(width, height);
	}
}
