using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    arrow,
    key,
    life,
}

public class ItemData : MonoBehaviour
{
    public ItemType type;
    public int count = 1;

    public int arangeId = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.gameObject.tag == "Player")
        {
            if( type == ItemType.key)
            {
                ItemKeeper.hasKeys += 1;
            }
            else if( type == ItemType.arrow)
            {
                ItemKeeper.hasArrows += count;
            }
            else if( type == ItemType.life)
            {
                if( PlayerController.hp < 3)
                {
                    PlayerController.hp += 1;
                }
            }

            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            Rigidbody2D itemRigidbody = GetComponent<Rigidbody2D>();

            itemRigidbody.gravityScale = 2.5f;
            itemRigidbody.AddForce(new Vector2(0.0f, 6.0f), ForceMode2D.Impulse);
            Destroy(gameObject, 0.5f);
        }
    }
}
