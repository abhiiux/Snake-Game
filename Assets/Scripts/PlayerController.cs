using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float followSpeed;
    [SerializeField] int objPoolCount;
    [SerializeField] Transform bodyPrefab;
    [SerializeField] int snakeBodyCount;

    private List<Transform> bodyParts = new List<Transform>();
    private float spacing = 1f;
    private int currentBodyLength;


    void Start()
    {
        for (int i = 0; i < objPoolCount; i++)
        {
            Transform newbody = Instantiate(bodyPrefab);
            newbody.position -= new Vector3(1, 0);
            newbody.gameObject.SetActive(false);
            bodyParts.Add(newbody);
        }

        SnakeBody(snakeBodyCount);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Food":
                if (collision.TryGetComponent<ISnakeColliable>(out var item))
                {
                    SnakeBody(item.OnSnakeCollision());
                    GameManager.Instance.AddScore(item.OnSnakeCollision() - 1);
                }
                break;
            case "Wall":
                GameManager.Instance.StopGame();
                break;
            case "Snake":
                Debug.Log($" bitted myself");
                GameManager.Instance.WinScreen();
                break;
        }
    }
    void LateUpdate()
    {
        FollowHeadTrail();
    }

    private void SnakeBody(int value)
    {
        currentBodyLength = Mathf.Clamp(currentBodyLength + value,0,50);

        Debug.Log($" value is pressed and its {value}");
        for (int i = 0; i < bodyParts.Count; i++)
        {
            if (i < currentBodyLength)
            {
                bodyParts[i].gameObject.SetActive(true);
            }
            else
            {
                bodyParts[i].gameObject.SetActive(false);
            }
        }
    }
    private void FollowHeadTrail()
    {        
        for (int i = 0; i < bodyParts.Count; i++)
        {
            Transform target = i == 0 ? transform : bodyParts[i - 1];
            Vector2 targetPos = target.position * spacing;

            bodyParts[i].position = Vector2.Lerp(bodyParts[i].position, targetPos, followSpeed * Time.deltaTime);
            bodyParts[i].rotation = Quaternion.Lerp(bodyParts[i].rotation, target.rotation, followSpeed * Time.deltaTime);
        }
    }
}
