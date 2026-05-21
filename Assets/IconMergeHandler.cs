using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IconMerge))]
public class IconMergeHandler : MonoBehaviour
{
    private IconMerge iconMerge;

    private void Awake()
    {
        iconMerge = GetComponent<IconMerge>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IconMerge otherIcon = collision.gameObject.GetComponent<IconMerge>();
        if (otherIcon != null)
        {
            iconMerge.MergeWith(otherIcon);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeathLine"))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            if (rb != null && rb.velocity.magnitude < 0.1f)
            {
                GameManager.Instance.GameOver();
            }
        }
    }
}
