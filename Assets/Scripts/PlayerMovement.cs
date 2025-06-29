using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputAction movedirection;
    [SerializeField] Vector2 direction = Vector2.right;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;
    float value = 0;

    void OnEnable()
    {
        movedirection.Enable();
        movedirection.started += ChangeDirection;
    }
    void OnDisable()
    {
        movedirection.canceled -= ChangeDirection;
    }
    void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.forward * value * rotationSpeed * Time.deltaTime);
    }
    public void ChangeDirection(InputAction.CallbackContext context)
    {
        value = context.ReadValue<float>();
        Debug.Log($" input is working {value}");
    }
}
