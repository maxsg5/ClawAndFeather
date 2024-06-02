using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private Rigidbody2D _body;

    public Rigidbody2D Body
    {
        get
        {
            if (_body == null)
            {
                _body = GetComponent<Rigidbody2D>();
            }
            return _body;
        }

        set => _body = value;
    }

    private void OnEnable()
    {
        Body = GetComponent<Rigidbody2D>();
    }
    private void OnDisable()
    {
        Body.Sleep();
    }

    /// <summary>
    /// Will despawn the <see cref="Projectile"/> after a delay in <paramref name="seconds"/>.
    /// </summary>
    public void DespawnIn(float seconds) => StartCoroutine(Despawn(seconds));

    private IEnumerator Despawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Calculates the <paramref name="initialPosition"/> of a <see cref="Projectile"/> at a point in <paramref name="time"/>,
    /// assuming gravity is <see langword="-1"/>
    /// </summary>
    public static Vector3 ProjectileMotion(float time, Vector2 initialVelocity, Vector2 initialPosition, Projectile projectile)
    {
        Vector2 G = Physics2D.gravity * (projectile == null ? 1 : projectile.Body.gravityScale); 
        return (time * time * 0.5f * G) + (initialVelocity * time) + initialPosition;
    }
}
