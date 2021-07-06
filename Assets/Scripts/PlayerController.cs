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

	public static int hp = 3;
	public static string gameState;
	private bool inDamage = false;

	private void Start()
	{
		rbody = GetComponent<Rigidbody2D>();
		preAnimation = downAnime;

		gameState = "playing";
	}

	private void Update()
	{
		if( gameState != "playing")
		{
			return;
		}

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
		if( gameState != "playing")
		{
			return;
		}
		if( inDamage)
		{
			float val = Mathf.Sin(Time.time * 50);
			//Debug.Log(val);
			gameObject.GetComponent<SpriteRenderer>().enabled = 0 < val;
			return;
		}

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

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Enemy")
		{
			GetDamage(collision.gameObject);
		}
	}

	private void GetDamage(GameObject _enemy)
	{
		if(gameState == "playing")
		{
			hp--;
			//Debug.Log("Player HP=" + hp);
			if (0 < hp)
			{
				float knockbackSpeed = 4.0f;
				rbody.velocity = Vector2.zero;
				Vector3 v = (transform.position - _enemy.transform.position).normalized;
				rbody.AddForce(
					new Vector2(v.x * knockbackSpeed, v.y * knockbackSpeed),
					ForceMode2D.Impulse);
				inDamage = true;
				Invoke("DamageEnd", 0.25f);
			}
			else
			{
				GameOver();
			}
		}
	}

	private void DamageEnd()
	{
		inDamage = false;
		gameObject.GetComponent<SpriteRenderer>().enabled = true;
	}
	private void GameOver()
	{
		//Debug.Log("ゲームオーバー");
		gameState = "gameover";

		/**
		 * ゲームオーバー演出
		 */
		GetComponent<Collider2D>().enabled = false;
		rbody.velocity = Vector2.zero;
		rbody.gravityScale = 1.0f;
		rbody.AddForce(new Vector2(0.0f, 5.0f), ForceMode2D.Impulse);
		GetComponent<Animator>().Play(deadAnime);
		Destroy(gameObject, 1.0f);
	}

}
