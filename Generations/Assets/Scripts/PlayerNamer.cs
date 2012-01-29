using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

public class PlayerNamer : MonoBehaviour
{

	public SpriteText text; //SpriteText = EZ Gui > Controls > Label

	public List<string> names = new List<string>();
	
	private String prevName = "";
	private string currName = "";
	
	private System.Random randomizer = new System.Random();
	
	// Use this for initialization
    void Start () {	
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
		
			// first generation:
			// choose a random male name for the hero
			int randomIndex = randomizer.Next(0, names.Count-1);
			currName = names[randomIndex];
			
			// display text on screen
			text.Text = currName;
		}
    }
   
	// Update is called once per frame
    void Update () {
			// each generation after the first:
			// choose a random male name for the hero
			// concatenate previous generation's name, ex: "Harry, the son of William"
			// this emphasizes the 'generations' and matches the GGJ12 theme
			prevName = currName;
			int randomIndex = randomizer.Next(0, names.Count-1);
			currName = names[randomIndex];
			text.Text = currName + ", the son of " + prevName;
    }
}
