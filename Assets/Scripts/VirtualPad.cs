using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualPad : MonoBehaviour
{
	public GameObject padTab;
	public float MaxLength = 70.0f;
	public bool is4DPad = false;
	
	private GameObject player;
	private Vector2 defPos;
	private Vector2 downPos;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		defPos = padTab.GetComponent<RectTransform>().localPosition;
	}

	public void PadDown()
	{
		downPos = Input.mousePosition;
	}
	public void PadDrag()
	{
		Vector2 mousePosigion = Input.mousePosition;
		Vector2 newTabPos = (mousePosigion - downPos);
		if(is4DPad == false)
		{
			newTabPos.y = 0;
		}
		Vector2 axis = newTabPos.normalized;
		float len = Vector2.Distance(Vector2.zero, newTabPos);
		//Debug.Log(len);
		if(MaxLength < len)
		{
			newTabPos.x = axis.x * MaxLength;
			newTabPos.y = axis.y * MaxLength;
		}
		padTab.GetComponent<RectTransform>().localPosition = newTabPos + defPos;

		PlayerController pcon = player.GetComponent<PlayerController>();
		pcon.SetAxis(axis.x, axis.y);
	}
	public void PadUp()
	{
		padTab.GetComponent<RectTransform>().localPosition = defPos;
		PlayerController pcon = player.GetComponent<PlayerController>();
		pcon.SetAxis(0.0f, 0.0f);
	}
}
