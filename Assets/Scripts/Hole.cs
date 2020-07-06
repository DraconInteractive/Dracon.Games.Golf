using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hole : MonoBehaviour
{
    public UnityEvent onWin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ballIn)
        {
            timer += Time.deltaTime;
            if (timer > 1 && !winTriggered)
            {
                Win();
            }
        }
    }
    public bool ballIn;
    public float timer;
    public bool winTriggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            ballIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            ballIn = false;
            timer = 0;
        }
    }

    public virtual void Win ()
    {
        winTriggered = true;
        onWin?.Invoke();
        GameManager.Instance.Win();
    }
}
