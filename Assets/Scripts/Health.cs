using UnityEngine;

public class Health : MonoBehaviour, IDamageable<float>
{
    public float health;

    public void TakeDamage(float damage)
    {
        if (health - damage < 0)
        {
            Destroy(this.gameObject);
        }

        health -= damage;
    }
}
