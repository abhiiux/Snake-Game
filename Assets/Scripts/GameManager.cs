using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] Transform snakeFood;
    [SerializeField] Transform eatRock;
    [SerializeField] GameObject failUI;
    [SerializeField] TMP_Text scoreUI;
    [SerializeField] GameObject winUI;
    private float screenWidth;
    private float screenHeight;
    private int score;
    private TMP_Text scoretext;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        failUI.SetActive(false);
    }
    IEnumerator Start()
    {
        scoretext = failUI.transform.GetChild(0).GetComponent<TMP_Text>();

        float aspectRatio = Camera.main.aspect;
        float orthoSize = Camera.main.orthographicSize;
        screenWidth = orthoSize * aspectRatio;
        screenHeight = orthoSize;

        screenHeight -= 2f;
        screenWidth -= 2f;

        yield return new WaitForSeconds(5f);
        SpawnItem(snakeFood);
        yield return new WaitForSeconds(12f);
        SpawnItem(eatRock);
    }

    public void SpawnItem(Transform item)
    {
        Vector2 spawnPoint = GetRandomPoint();

        Transform food = Instantiate(item);
        food.position = spawnPoint;
    }

    public Vector2 GetRandomPoint()
    {
        Vector2 point = new Vector2(UnityEngine.Random.Range
                                        (-screenWidth, screenWidth), UnityEngine.Random.Range(screenHeight, -screenHeight));
        return point;
    }
    public void StopGame()
    {
        Time.timeScale = 0f;
        scoretext.text = score.ToString();
        failUI.SetActive(true);
    }

    public void AddScore(int value)
    {
        score = Mathf.Clamp(score + value,0,50);
        scoreUI.text = score.ToString();
    }
    public void WinScreen()
    {
        Time.timeScale = 0f;
        winUI.SetActive(true);
    }
}
