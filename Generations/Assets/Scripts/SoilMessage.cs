using UnityEngine;
using System.Collections;

public enum Action
{
	SET,
	INCREMENT_GENERATION
}

public class SoilMessage : Message
{
	public int id;
	public GenerationObject soilObject;
	public Action action = Action.SET;
	public int numberOfGenerationIncrement = 1;
}
