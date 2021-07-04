using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed = 3.0f;
	public string upAnime = "PlayerUp";
	public string downAnime = "PlayerDown";
	public string rightAnime = "PlayerRight";
	public string leftAnime = "PlayerLeft";
	public string deadAnime = "PlayerDead";

	private string nowAnimation = "";
	private string preAnimation = "";

	private float axisH;
	private float axisV;
	public float angleZ = -90.0f;

	private Rigidbody2D rbody;
	private bool isMoving = false;

	private void Start()
	{
		rbody = GetComponent<Rigidbody2D>();
		preAnimation = downAnime;
	}

	private void Update()
	{
		if( isMoving == false)
		{
			axisH = Input.GetAxisRaw("Horizontal");
			axisV = Input.GetAxisRaw("Vertical");
		}
		Vector2 fromPt = transform.position;
		Vector2 toPt = new Vector2(fromPt.x + axisH, fromPt.y + axisV);
		angleZ = GetAngle(fromPt, toPt, angleZ);

		if( -45.0f <= angleZ && angleZ < 45.0f)
		{
			nowAnimation = rightAnime;
		}
		else if( 45.0f <= angleZ && angleZ < 135.0f)
		{
			nowAnimation = upAnime;
		}
		else if( -135.0f <= angleZ && angleZ < -45.0f)
		{
			nowAnimation = downAnime;
		}
		else
		{
			nowAnimation = leftAnime;
		}
		if( preAnimation != nowAnimation)
		{
			GetComponent<Animator>().Play(nowAnimation);
			preAnimation = nowAnimation;
		}
	}
	private void FixedUpdate()
	{
		rbody.velocity = new Vector2(axisH, axisV) * speed;
	}

	public void SetAxis(float _h , float _v)
	{
		axisH = _h;
		axisV = _v;
		isMoving = !(axisH == 0 && axisV == 0);
	}

	private float GetAngle(Vector2 _p1 , Vector2 _p2 , float _preAngle)
	{
		float angle;
		Vector2 delta = _p2 - _p1;
		if (delta.x != 0 || delta.y != 0)
		{
			float rad = Mathf.Atan2(delta.y, delta.x);
			angle = rad * Mathf.Rad2Deg;
		}
		else
		{
			angle = _preAngle;
		}
		return angle;
	}

}
