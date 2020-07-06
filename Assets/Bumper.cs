using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public Transform bumpTransform;
    public float force;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            print("Bumping");
            Ball.Instance.Bump(bumpTransform.up, force);
        }
    }
}
