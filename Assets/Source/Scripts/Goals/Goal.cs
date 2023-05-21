using UnityEngine;

[System.Serializable]
public class Goal 
{
    [SerializeField] private float _needMoreThan;
    public bool IsDone { get; set; }
    public float GoalValue => _needMoreThan;
}
