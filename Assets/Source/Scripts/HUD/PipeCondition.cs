using UnityEngine;
using UnityEngine.UI;

public class PipeCondition : MonoBehaviour
{
    [SerializeField] private Image _filling;
    [SerializeField] private Pipe _pipe;


    private void Awake()
    {
        _pipe.ConditionChanged += UpdateConditionBar;
    }
    private void OnDestroy()
    {
        _pipe.ConditionChanged -= UpdateConditionBar;
    }

    private void UpdateConditionBar()
    {
        _filling.fillAmount = _pipe.Condition / 100;
    }

}