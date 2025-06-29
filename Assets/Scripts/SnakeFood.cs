using UnityEngine;

public class SnakeFood : MonoBehaviour, ISnakeColliable
{
    [SerializeField] int bodyGrowthCount;

    public int OnSnakeCollision()
    {
        Invoke("ChangePosition", 0.5f);
        return bodyGrowthCount;
    }

    private void ChangePosition()
    {
        transform.position = GameManager.Instance.GetRandomPoint();
    }
    
}
