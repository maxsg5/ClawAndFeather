using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public Rigidbody2D Body { get; set; }

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
    /// Calculates the <paramref name="position"/> of a <see cref="Projectile"/> at a point in <paramref name="time"/>, assuming gravity is <see langword="1"/>
    /// </summary>
    public static void ProjectileMotion(float time, Vector2 Vi, out Vector2 position) => position = new(Vi.x * time, 0.5f * (time * time) + Vi.y * time);
}
