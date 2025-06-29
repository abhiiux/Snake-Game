using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform snakeFood;
    private float screenWidth;
    private float screenHeight;
    void Start()
    {
        float aspectRatio = Camera.main.aspect;
        float orthoSize = Camera.main.orthographicSize;
        screenWidth = orthoSize * aspectRatio;
        screenHeight = (orthoSize * aspectRatio) / 2f;

        screenHeight -= 2f;
        screenWidth -= 2f;

        Debug.Log($"screen width : {screenWidth}");
        Debug.Log($" screen height : {screenHeight}");
    }
    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            SpawnFood();
        }
    }
    public void SpawnFood()
    {
        Vector2 spawnPoint = new Vector2(UnityEngine.Random.Range
                                        (-screenWidth, screenWidth), UnityEngine.Random.Range(screenHeight, -screenHeight));

        Transform food = Instantiate(snakeFood);
        food.position = spawnPoint;
    }
}
