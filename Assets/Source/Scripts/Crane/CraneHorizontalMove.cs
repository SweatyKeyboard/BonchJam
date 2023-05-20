using UnityEngine;

public class CraneHorizontalMove : MonoBehaviour
{
    [SerializeField] private float _limitX;
    [SerializeField] private GameObject _choosePanel;
    private bool _isPanelOpened;

    private float _timeSinceClick = 0f;

    private void OnMouseDrag()
    {
        _timeSinceClick += Time.deltaTime;

        if (_timeSinceClick > 0.5f)
        {
            transform.position = new Vector3(
                Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, -_limitX, _limitX),
                transform.position.y,
                0);

            _isPanelOpened = false;
            _choosePanel.SetActive(_isPanelOpened);
        }
    }

    private void OnMouseDown()
    {
        _timeSinceClick = 0;
        _isPanelOpened = !_isPanelOpened;
        _choosePanel.SetActive(_isPanelOpened);
    }
}
