using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[AddComponentMenu("Scripts/Claw and Feather/Entities/Player Controller")]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    #region Movement
    private Rigidbody2D _body;
    public int Direction { get; private set; } = 1; // 1 is right, -1 is left
    [field: Header("Movement Settings")]
    [field: SerializeField] public float Speed { get; set; } = 5f;
    [field: SerializeField] public float ImpulseMultiplier { get; set; } = 2f;
    [field: SerializeField] public float MaxVelocity { get; set; } = 10f;
    #endregion

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _body.AddForce(Direction * Speed * Vector2.right);
        _body.velocity = Vector2.ClampMagnitude(_body.velocity, MaxVelocity);
    }
    
    public void ButtonControl(InputAction.CallbackContext context)
    {
        if (context.performed && Time.timeScale != 0)
        {
            switch (context.interaction)
            {
                case TapInteraction: // Reverse Direction
                    Direction *= -1;
                    _body.AddForce(Direction * Speed * ImpulseMultiplier * Vector2.right, ForceMode2D.Impulse);
                    break;
                case HoldInteraction: // Pause
                    Singleton.Global.State.Pause();
                    break;
            }
        }
    }

    #region Animation
    [Header("Animation Settings")]
    [SerializeField] private Animator _animator;
    [SerializeField, Min(0)] private float _deathLength;
    private IEnumerator DieAnimation()
    {
        yield return new WaitForSeconds(_deathLength);
        _animator.SetTrigger("Dead");
        GameState.ChangeScene(1); // TEMPORARY
    }
    
    #endregion

    #region Heath
    [field: Header("Health Settings")]
    [field: SerializeField, Range(0, 9)] public int Lives { get; set; } = 9;
    [field: SerializeField] public bool IsInvulnerable { get; set; } = false;
    [field: SerializeField] public float InvulnerableTime { get; set; } = 2.0f;

    [Space]
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Color _hitColour;
    [SerializeField, Min(0)] private float _hitDuration;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!IsInvulnerable)
        {
            var obstacle = collider.GetComponentInParent<Obstacle>();
            if (obstacle != null && obstacle.colliders.Contains(collider))
            {
                Lives--;
                _animator.SetInteger("Lives", Lives);
                if (Lives <= 0)
                {
                    _animator.SetTrigger("Falling");
                    StartCoroutine(DieAnimation());
                }
                else
                {
                    StartCoroutine(TakeDamage(InvulnerableTime));
                } 
            }
        }
    }

    private IEnumerator TakeDamage(float invulnerableTime)
    {
        IsInvulnerable = true;
        float currentTime = 0;
        _renderer.color = _hitColour;
        while (currentTime < invulnerableTime)
        {
            _renderer.color = Color.Lerp(_renderer.color, Color.white, 0.1f);
            currentTime += Time.deltaTime;
            yield return null;
        }
        _renderer.color = Color.white;
        IsInvulnerable = false;
    }
    #endregion
}
