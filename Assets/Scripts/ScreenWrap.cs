using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    [SerializeField] float leftBound;
    [SerializeField] float rightBound;
    [SerializeField] float topBound;
    [SerializeField] float bottomBound;

    void LateUpdate()
    {
        WrapPosition();
    }

    private void WrapPosition()
    {
        Vector2 pos = transform.position;

        if (pos.x > rightBound)
        {
            pos.x = leftBound;
        }
        else if (pos.x < leftBound)
        {
            pos.x = rightBound;
        }
        else if (pos.y > topBound)
        {
            pos.y = bottomBound;
        }
        else if (pos.y < bottomBound)
        {
            pos.y = topBound;
        }

        transform.position = pos;
    }
}
