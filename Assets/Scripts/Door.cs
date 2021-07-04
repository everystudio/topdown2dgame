using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	public int arrangeId = 0;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if( collision.gameObject.tag == "Player")
		{
			if( 0 < ItemKeeper.hasKeys)
			{
				ItemKeeper.hasKeys -= 1;
				Destroy(gameObject);
			}
		}
	}

}
