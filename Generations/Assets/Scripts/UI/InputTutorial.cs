using UnityEngine;
using System.Collections;

public class InputTutorial : MonoBehaviour
{
	public Texture2D Button;
	public Texture2D Action;
	public string ActionText;
	public Vector3 Offset;
	public int Padding = 1;
	
	
	private bool shown;
	public Vector3 ScaledOffset;
	
	public void Start()
	{
		shown = false;
	}
	
	public void OnDrawGizmos()
	{
		Gizmos.DrawSphere(transform.position + Offset, 0.1f);
	}
	
	public void Show()
	{
		shown = true;
		Generation currentGen = GenerationManager.Instance.GetPlayableGeneration();
		
		ScaledOffset = Offset;
		ScaledOffset.x *= currentGen.transform.localScale.x;
		ScaledOffset.y *= currentGen.transform.localScale.y;
		ScaledOffset.z *= currentGen.transform.localScale.z;
	}
	
	public void Hide()
	{
		shown = false;
	}
	
	public void OnGUI()
	{
		if(shown)
		{
			Generation currentGen = GenerationManager.Instance.GetPlayableGeneration();
			
			Vector3 TutorialCenter = transform.position + ScaledOffset;
			
			Vector2 UITutorialCenter = currentGen.genCamera.camera.WorldToScreenPoint(TutorialCenter);
			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			GUIContent actionLabel = new GUIContent(ActionText);
			Vector2 actionLabelSize = GUI.skin.label.CalcSize(actionLabel);
			
			int UIWidth = Mathf.Max(Button.width + Padding + Action.width, (int)actionLabelSize.x);
			int UIHeight = Mathf.Max(Button.height, Action.width) + Padding + (int)actionLabelSize.y;
			
			Vector2 UITopCorner = new Vector2(UITutorialCenter.x - UIWidth / 2, Screen.height - UITutorialCenter.y - UIHeight / 2);
			
			Rect ButtonIconRect = new Rect(UITopCorner.x, UITopCorner.y, Button.width, Button.height);
			Rect ActionIconRect = new Rect(ButtonIconRect.xMax + Padding, UITopCorner.y, Action.width, Action.height);
			Rect ActionLabelRect = new Rect(UITopCorner.x, UITopCorner.y + Padding + Mathf.Max(Button.height, Action.height), UIWidth, UIHeight - Mathf.Max(Button.height, Action.height));
			
			GUI.Box(new Rect(UITopCorner.x + UIWidth / 2, UITopCorner.y + UIHeight / 2, 5, 5), new GUIContent());
			GUI.DrawTexture(ButtonIconRect, Button);
			GUI.DrawTexture(ActionIconRect, Action);
			GUI.Label(ActionLabelRect, actionLabel);
		}
	}
	
	void OnTriggerEnter()
	{
		Show();
	}
	
	void OnTriggerExit()
	{
		Hide();
	}
}

