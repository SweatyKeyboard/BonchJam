using UnityEngine;

[CreateAssetMenu(menuName = "Employee")]
public class EmployeeData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private a_Employee _prefab;

    public string Name => _name;
    public a_Employee Prefab => _prefab;
}
