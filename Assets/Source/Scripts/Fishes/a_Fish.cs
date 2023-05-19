using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class a_Fish : MonoBehaviour, IVictim
{
    [SerializeField] protected float _horizontalSpeed;
    [SerializeField] protected float _verticalAmplitude;
    [SerializeField] protected float _verticalSpeed;

    [SerializeField] private Color _diedTint;

    protected IMoveType Moving;

    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;

    private bool _isDead;
    public bool IsDead => _isDead;

    public bool IsFollowable { get; set; } = true;

    protected void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected void Update()
    {
        Vector2 direction = Moving.Move(_horizontalSpeed);
        _rigidBody.MovePosition((Vector2)transform.position + direction);

        if (direction.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }

    }

    public void Die()
    {
        _isDead = true;
        IsFollowable = false;
        _spriteRenderer.color = _diedTint;
        _spriteRenderer.flipY = true;
        Moving = new DeadMoving(_verticalSpeed, _verticalAmplitude);
    }

    public void Flip(Vector2 direction)
    {
        ((ComplexMove)Moving).Direction = direction;
    }
}
