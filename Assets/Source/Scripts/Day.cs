using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Day : MonoBehaviour
{
    [SerializeField] private TMP_Text _dayCounter;
    [SerializeField] private Image _progress;
    [SerializeField] private float _dayDuration;

    [SerializeField] private SupplyManager _supplies;

    private float _time;

    private int _dayNumber = 1;
    public int DayNumber {
        get => _dayNumber;
        set
        {
            _dayNumber = value;
            _dayCounter.text = value.ToString();
        }
    }

    private void Update()
    {
        _time += Time.deltaTime;
        _progress.fillAmount = _time / _dayDuration;

        if (_time >= _dayDuration)
        {
            DayNumber++;
            _time = 0;
            _supplies.GetSupply();
        }
    }
}
