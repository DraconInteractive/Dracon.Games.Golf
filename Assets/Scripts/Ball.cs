using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameState == GameManager.GameState.Acting)
        {
            if (rb.velocity.magnitude < 0.1f)
            {
                rb.velocity = Vector2.zero;
                rb.Sleep();
                GameManager.gameState = GameManager.GameState.Ready;
            }
        }
    }

    public void Destroy ()
    {
        Destroy(this.gameObject);
    }

    public void Flick (Vector3 dir)
    {
        if (rb.IsSleeping())
        {
            rb.WakeUp();
        }
        rb.AddForce(-dir * force, ForceMode2D.Impulse);
    }
}
