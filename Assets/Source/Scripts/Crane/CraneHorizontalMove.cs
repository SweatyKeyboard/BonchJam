using UnityEngine;

public class CraneHorizontalMove : MonoBehaviour
{
    [SerializeField] private float _limitX;

    public bool IsLocked { get; set; } = false;

    private void OnMouseDrag()
    {
        if (IsLocked)
            return;

        transform.position = new Vector3(
            Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, -_limitX, _limitX),
            transform.position.y,
            0);
    }
}
