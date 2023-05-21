using UnityEngine;

public class CraneHorizontalMove : MonoBehaviour
{
    [SerializeField] private float _limitX;

    private void OnMouseDrag()
    {
        transform.position = new Vector3(
            Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, -_limitX, _limitX),
            transform.position.y,
            0);
    }
}
