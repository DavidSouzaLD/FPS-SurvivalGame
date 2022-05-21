using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WeaponSystemEvents : MonoBehaviour
{
    private WeaponSystem m_Weapon;
    private Animator m_Animator;

    private void Start()
    {
        m_Weapon = GetComponentInParent<WeaponSystem>();
        m_Animator = GetComponent<Animator>();
    }

    public void ReloadPickMagazine() => m_Weapon.PlaySound(m_Weapon.WeaponSO.reloadPickMagazine);
    public void ReloadPutMagazine() => m_Weapon.PlaySound(m_Weapon.WeaponSO.reloadPutMagazine);
    public void ReloadCocking() => m_Weapon.PlaySound(m_Weapon.WeaponSO.reloadCocking);
    public void ReloadComplete() => m_Weapon.ReloadComplete();
}