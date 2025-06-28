using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform bodyPrefab;
    [SerializeField] float followSpeed;
    [SerializeField] float spacing;
    public InputAction movedirection;
    public Vector2 direction = Vector2.right;
    public float moveSpeed;
    public float rotationSpeed;
    float value = 0;

    private List<Transform> bodyParts = new List<Transform>();
    private List<Vector2> positionHistory = new List<Vector2>();

    private void OnEnable()
    {
        movedirection.Enable();
        movedirection.started += ChangeDirection;
    }
    private void OnDisable()
    {
        movedirection.canceled -= ChangeDirection;
    }
    private void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.forward * value * rotationSpeed * Time.deltaTime);

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            GrowSnake();
        }
    }
    private void LateUpdate()
    {
        FollowHeadTrail();
    }

    public void ChangeDirection(InputAction.CallbackContext context)
    {
        value = context.ReadValue<float>();
        Debug.Log($" input is working {value}");
    }

    private void GrowSnake()
    {
        Vector2 spawnPos = bodyParts.Count > 0 ? bodyParts[bodyParts.Count - 1].position : transform.position;
        Transform newbody = Instantiate(bodyPrefab);
        newbody.position = spawnPos;
        bodyParts.Add(newbody);
    }
    private void FollowHeadTrail()
    {
        positionHistory.Insert(0, transform.position);
        
        for (int i = 0; i < bodyParts.Count; i++)
        {
            Transform target = i == 0 ? transform : bodyParts[i - 1];
            Vector2 targetPos = target.position * spacing;

            bodyParts[i].position = Vector2.Lerp(bodyParts[i].position, targetPos, followSpeed * Time.deltaTime);
            bodyParts[i].rotation = Quaternion.Lerp(bodyParts[i].rotation, target.rotation, followSpeed * Time.deltaTime);
        }
    }
}
