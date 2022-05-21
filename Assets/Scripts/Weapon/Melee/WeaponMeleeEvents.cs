using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WeaponMeleeEvents : MonoBehaviour
{
    private WeaponMeleeSystem m_WeaponMelee;

    private void Start()
    {
        m_WeaponMelee = GetComponentInParent<WeaponMeleeSystem>();
    }

    public void ApplyDamage() => m_WeaponMelee.ApplyDamage();
    public void AttackComplete() => m_WeaponMelee.AttackComplete();
}