using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
	public void Attack()
	{
		//ItemKeeper.hasArrows += 1;
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player != null)
		{
			ArrowShoot shooter = player.GetComponent<ArrowShoot>();
			shooter.Attack();
		}
	}
}
