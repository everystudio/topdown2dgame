using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
	public static int doorNumber = 0;

	private void Start()
	{
		GameObject[] enters = GameObject.FindGameObjectsWithTag("Exit");
		for( int i = 0; i < enters.Length; i++)
		{
			GameObject doorObj = enters[i];
			Exit exit = doorObj.GetComponent<Exit>();
			if( doorNumber == exit.doorNumber)
			{
				float x = doorObj.transform.position.x;
				float y = doorObj.transform.position.y;
				if(exit.direction == ExitDirection.up)
				{
					y += 1;
				}
				else if (exit.direction == ExitDirection.right)
				{
					x += 1;
				}
				else if (exit.direction == ExitDirection.down)
				{
					y -= 1;
				}
				else if (exit.direction == ExitDirection.left)
				{
					x -= 1;
				}
				else
				{
					Debug.LogError("ê›íËÉ~ÉX");
				}
				GameObject player = GameObject.FindGameObjectWithTag("Player");
				player.transform.position = new Vector3(x, y);
				break;
			}
		}
	}
	public static void ChangeScene(string scenename , int doornum)
	{
		doorNumber = doornum;
		SceneManager.LoadScene(scenename);
	}
}
