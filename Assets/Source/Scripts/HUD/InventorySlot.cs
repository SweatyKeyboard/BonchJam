using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class InventorySlot : MonoBehaviour
{
    [SerializeField] private ForPipeData _element;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private TMP_Text _tommorowText;

    private Button _button;
    public bool ButtonActive
    {
        set => _button.interactable = value;
    }

    private int _count;
    public int Count
    {
        get => _count;
        set
        {
            if (value > _count)
            {
                StartCoroutine(Blink(Color.green, Color.white, 0.5f));
            }

            _count = value;
            _text.text = _count.ToString();

        }
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void SetTommorow(int count)
    {
        _tommorowText.text = count.ToString();
    }

    public ForPipeData Element => _element;

    private IEnumerator Blink(Color fromColor, Color toColor, float duration)
    {
        _text.color = fromColor;

        float elapsed = 0;

        do
        {
            _text.color = Color.Lerp(fromColor, toColor, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        } while (elapsed < duration);

        _text.color = toColor;
    }
}
