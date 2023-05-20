using UnityEngine;

public interface IMoveType
{
    public Vector2 Direction { get; set; }
    public Vector2 Move(float speed);
}
