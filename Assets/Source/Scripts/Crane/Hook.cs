using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] private float _minY;
    [SerializeField] private float _maxY;

    [SerializeField] private Transform _rope;
    [SerializeField] private Transform _roller;

    private a_Employee _employee;

    private void OnMouseDrag()
    {
        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.mousePosition).y, _minY, _maxY),
            0);

        _rope.localScale = new Vector3(
            _rope.localScale.x,
            (_roller.position.y - transform.position.y) / _roller.lossyScale.y,
            1);
        _rope.transform.position = new Vector3(
            _roller.position.x,
            (_roller.position.y + transform.position.y) / 2,
            1);
    }

    public void SetEmployee(EmployeeData data)
    {
        _employee = data.Prefab;
    }
}
