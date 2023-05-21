using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Goals : MonoBehaviour
{
    [SerializeField] private Image[] _checks;
    [SerializeField] private Goal _oxygenGoal;
    [SerializeField] private Goal _deadGoal;
    [SerializeField] private Goal _daysGoal;
    [SerializeField] private Goal _fishesGoal;

    public int DeadTook { get; set; }

    public static Goals Instance { get; set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void CheckOxygen()
    {
        if (Bar.Instance.Value >= _oxygenGoal.GoalValue)
        {
            _checks[0].gameObject.SetActive(true);
            _oxygenGoal.IsDone = true;
        }

    }

    public void CheckDays()
    {
        if (Day.Instance.DayNumber >= _daysGoal.GoalValue)
        {
            _checks[1].gameObject.SetActive(true);
            _daysGoal.IsDone = true;
        }
    }

    public void CheckDeads()
    {
        if (DeadTook >= _deadGoal.GoalValue)
        {
            _checks[3].gameObject.SetActive(true);
            _deadGoal.IsDone = true;
        }
    }

    public void CheckFishes()
    {
        if (FindObjectsOfType<Fish>().Where(x => !x.IsDead).ToList().Count >= _fishesGoal.GoalValue)
        {
            _checks[2].gameObject.SetActive(true);
            _fishesGoal.IsDone = true;
        }
    }
}