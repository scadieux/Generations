using UnityEngine;
using System.Collections;

public class SoilMessage : Message
{
	public enum SoilState
	{
		Blank = 0,
		Seed,
		BabyTree,
		AdultTree,
		OldTree,
		Log,
		LogAndTrunk
	}
	
	public int id;
	public SoilState state;
	public bool playAnims;
}
