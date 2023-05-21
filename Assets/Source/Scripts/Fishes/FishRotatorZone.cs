using UnityEngine;

public class FishRotatorZone : MonoBehaviour
{
    [SerializeField] private Vector2 _direction;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out a_Fish fish))
        {
            fish.Flip(_direction);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1, 0.2f);
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
