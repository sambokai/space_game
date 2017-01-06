using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightHUD : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
	}
		
	// Update is called once per frame
	void Update () {
	}


	void OnGUI()
	{
		drawCrosshair();
		drawCursor ();
	}


	//Crosshair
	public Texture2D crosshairTexture;
	private Rect ch_pos;
	int ch_w, ch_h;

	void drawCrosshair()
	{
		ch_w = crosshairTexture.width;
		ch_h = crosshairTexture.height;
		ch_pos.Set (Screen.width / 2 - (ch_w / 2), Screen.height / 2 - (ch_h / 2), ch_w, ch_h);
		GUI.DrawTexture (ch_pos, crosshairTexture, ScaleMode.ScaleToFit);
	}

	//Cursor
	public Texture2D cursorTexture;
	private CursorMode cursormode = CursorMode.Auto;
	Vector2 hotSpot;
	void initCursor()
	{
		hotSpot.Set(cursorTexture.width / 2, cursorTexture.height / 2);
		Cursor.SetCursor (cursorTexture, hotSpot, cursormode);
	}

	private Rect crs_pos;
	float crs_w, crs_h, crs_x, crs_y;
	void drawCursor()
	{	
		crs_x = Event.current.mousePosition.x;
		crs_y = Event.current.mousePosition.y;
		crs_w = cursorTexture.width;
		crs_h = cursorTexture.height;
		crs_pos.Set (crs_x - (crs_w / 2), crs_y - (crs_h / 2), crs_w, crs_h);
		GUI.DrawTexture (crs_pos, cursorTexture, ScaleMode.ScaleToFit);	
	}
}
