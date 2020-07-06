using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public GameObject piecesContainer;
    public Rigidbody2D[] pieces;
    public Transform explosionCenter;
    public float force;
    public bool destroyOnDelay;
    public float delay;
    public void Do ()
    {
        piecesContainer.SetActive(true);
        foreach (var piece in pieces)
        {
            piece.AddForce((piece.transform.position - explosionCenter.transform.position).normalized * force, ForceMode2D.Impulse);
        }
        if (destroyOnDelay)
        {
            Invoke("DestroyAll", delay);
        }
    }

    public void DestroyAll ()
    {
        Destroy(this.gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        if (pieces.Length > 0 && explosionCenter != null)
        {
            foreach (var piece in pieces)
            {
                var vector = (piece.transform.position - explosionCenter.transform.position).normalized;
                Gizmos.DrawRay(explosionCenter.transform.position, vector);
            }
        }
    }
}
