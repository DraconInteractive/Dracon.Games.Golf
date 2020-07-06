using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject ballPrefab;
    public Transform ballContainer;
    public static Ball activeBall;

    [Space(30)]
    public UnityEvent onBeginPlay;
    public UnityEvent onStopPlay;
    public UnityEvent onFlick;

    Vector3 lastMouse;
    bool mouseDown;
    public LineRenderer guideLine;
    public LayerMask guideRayMask;

    [Space(30)]
    public Image debugImage;
    public enum GameState
    {
        PreGame,
        Ready,
        Acting,
        Win
    }

    public static GameState gameState;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (gameState == GameState.PreGame && Input.GetKeyDown(KeyCode.Space))
        {
            BeginPlay();
        }
        RegisterInput();

        if (mouseDown && activeBall != null)
        {
            lastMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 dir = activeBall.transform.position - lastMouse;
            Ray ray = new Ray(activeBall.transform.position, dir);
            RaycastHit hit;
            Vector3 normal = Vector3.zero;
            if (Physics.Raycast(ray, out hit, 100, guideRayMask))
            {
                normal = hit.normal;
            }
            guideLine.SetPositions(new Vector3[] { lastMouse, activeBall.transform.position, activeBall.transform.position + normal });
        }
        switch (gameState)
        {
            case GameState.PreGame:
                debugImage.color = Color.grey;
                break;
            case GameState.Ready:
                debugImage.color = Color.yellow;
                break;
            case GameState.Acting:
                debugImage.color = Color.cyan;
                break;
            case GameState.Win:
                debugImage.color = Color.green;
                break;
        }
    }

    public static void BeginPlay ()
    {
        Instance.CreateBall();
        Instance.onBeginPlay?.Invoke();
        gameState = GameState.Ready;
    }

    public static void StopPlay ()
    {
        Instance.onStopPlay?.Invoke();
    }

    public void RegisterInput ()
    {
        if (gameState == GameState.Ready)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseDown = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                Vector3 dir = lastMouse - activeBall.transform.position;
                print(dir);
                activeBall?.Flick(dir);
                Instance.onFlick?.Invoke();
                mouseDown = false;

                Vector3 z = Vector3.zero;
                guideLine.SetPositions(new Vector3[] { z, z, z });

                gameState = GameState.Acting;
            }
        } else
        {
            mouseDown = false;
        }
    }

    public void CreateBall ()
    {
        if (activeBall != null)
        {
            activeBall.Destroy();
        }

        activeBall = Instantiate(ballPrefab, ballContainer).GetComponent<Ball>();
    }

    public void Win ()
    {
        gameState = GameState.Win;
    }

    public void LoadMenu ()
    {
        SceneManager.LoadScene("menu");
    }
    
}