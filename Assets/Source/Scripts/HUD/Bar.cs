using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public static Bar Instance { get; private set; }

    [SerializeField] private Slider _slider;

    [SerializeField] private float _startValue;
    [SerializeField] private float _baseDecreaseBySecond;

    public float AdditionalDecreaseBySecond { get; set; }

    private float _value;
    public float Value
    {
        get => _value;
        private set
        {
            _value = value;
            _slider.value = value;
        }
    }
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

        _value = _startValue;
    }

    private void Update()
    {
        Value -= Time.deltaTime * (_baseDecreaseBySecond + AdditionalDecreaseBySecond);

        if (Value > 80)
        {
            Goals.Instance.CheckOxygen();
        }
    }

    public void AddOxygen(float value)
    {
        Value += value;
    }
}
