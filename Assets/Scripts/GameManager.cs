using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] Transform snakeFood;
    [SerializeField] GameObject failUI;
    [SerializeField] TMP_Text scoreUI;
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
    void Start()
    {
        scoretext = failUI.transform.GetChild(0).GetComponent<TMP_Text>();

        float aspectRatio = Camera.main.aspect;
        float orthoSize = Camera.main.orthographicSize;
        screenWidth = orthoSize * aspectRatio;
        screenHeight = (orthoSize * aspectRatio) / 2f;

        screenHeight -= 2f;
        screenWidth -= 2f;

        Invoke("SpawnFood", 5f);
    }

    public void SpawnFood()
    {
        Vector2 spawnPoint = RandomPoint();

        Transform food = Instantiate(snakeFood);
        food.position = spawnPoint;
    }

    public Vector2 RandomPoint()
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
        score += value;
        scoreUI.text = score.ToString();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
