using UnityEngine;

public interface ISelectable
{
    public System.Action<Transform> Clicked { get; set; }
    public bool IsSelected { get; set; }
    public void Select();
    public void Deselect();
}
