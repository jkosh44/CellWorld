using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScript : MonoBehaviour {
	public Font font;
	public float time = 5;
	// Use this for initialization
	void Start () {
		TextGenerationSettings settings = new TextGenerationSettings();
		settings.textAnchor = TextAnchor.MiddleCenter;
		settings.color = Color.red;
		settings.generationExtents = new Vector2(500.0F, 200.0F);
		settings.pivot = Vector2.zero;
		settings.richText = true;
		settings.font = font;
		settings.fontSize = 80;
		settings.fontStyle = FontStyle.Normal;
		settings.verticalOverflow = VerticalWrapMode.Overflow;
		TextGenerator generator = new TextGenerator();
		generator.Populate("I am a string", settings);
		Debug.Log("I generated: " + generator.vertexCount + " verts!");
//		Destroy(gameObject, time);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
