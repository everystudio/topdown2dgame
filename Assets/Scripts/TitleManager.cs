using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
	public GameObject startButton;
	public GameObject continueButton;

	public void StartButtonClicked()
	{
		Debug.Log("StartButtonClicked");
	}
	public void ContinueButtonClicked()
	{
		Debug.Log("ContinueButtonClicked");
	}

}
