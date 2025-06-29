using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float followSpeed;
    [SerializeField] int objPoolCount;
    [SerializeField] Transform bodyPrefab;

    private List<Transform> bodyParts = new List<Transform>();
    private float spacing = 1f;


    void Start()
    {
        for (int i = 0; i < objPoolCount; i++)
        {
            Transform newbody = Instantiate(bodyPrefab);
            bodyParts.Add(newbody);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Food":
                if (collision.TryGetComponent<ISnakeColliable>(out var item))
                {
                    GrowSnake(item.OnSnakeCollision());
                }
                break;
            case "Wall":
                Debug.Log($" hit wall {collision.gameObject.name}");
                GameManager.Instance.StopGame();
                break;
            case "Snake":
                Debug.Log($" bitted myself");
                GameManager.Instance.StopGame();
                break;
        }


    }
    void LateUpdate()
    {
        FollowHeadTrail();
    }

    private void GrowSnake(int value)
    {
        for (int i = 0; i < value; i++)
        {
            Transform newbody = Instantiate(bodyPrefab);
            newbody.position = transform.position;
            bodyParts.Add(newbody);
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
