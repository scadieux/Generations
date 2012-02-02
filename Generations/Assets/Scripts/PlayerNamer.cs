using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

public class PlayerNamer : MonoBehaviour
{

	public SpriteText text; //SpriteText = EZ Gui > Controls > Label

	public static List<string> names = new List<string>();
	
	private static String prevName = "";
	private static string currName = "";
	
	private static System.Random randomizer = new System.Random();
	
	private bool namesAreLoaded = false;
	
//	static PlayerNamer()
//	{
//	}
	
	private void loadNames() 
	{
		namesAreLoaded = true;
		TextAsset namesFile = (TextAsset)Resources.Load("male-names", typeof(TextAsset));
        StringReader reader = new StringReader(namesFile.text);
		if ( reader == null )
		{
   			Debug.Log("male-names.txt not found or not readable");
		}
		else 
		{
			string name = reader.ReadLine();
			while (name != null) {
				names.Add(name);
	            name = reader.ReadLine();
	        }
		}
	}
   
    void Spawn()
	{
		if (!namesAreLoaded)
			loadNames();
		StartCoroutine(ShowNameCoroutine());
    }
	
	IEnumerator ShowNameCoroutine()
	{
		
		prevName = currName;
		
		if (currName == "") {
			// first generation:
			// choose a random male name for the hero
			int randomIndex = randomizer.Next(0, names.Count-1);
			currName = names[randomIndex];
			text.Text = currName;
		}
		else {
			// each generation after the first:
			// choose a random male name for the hero
			// concatenate previous generation's name, ex: "Harry, the son of William"
			// this emphasizes the 'generations' and matches the GGJ12 theme
			int randomIndex = randomizer.Next(0, names.Count-1);
			currName = names[randomIndex];
			text.Text = currName + ", the son of " + prevName;
		}
				
		yield return new WaitForSeconds(2);
		
		text.Hide(true);
	}
}
