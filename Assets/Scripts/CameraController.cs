using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Ball target;

    public Vector3 offset;
    public float speed;

    private void Start()
    {
        GameManager.Instance.onBeginPlay.AddListener(() => OnPlayBegin());
    }

    private void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position + offset, speed * Time.deltaTime);
        }
    }
    public void OnPlayBegin ()
    {
        target = GameManager.activeBall;
    }
}
