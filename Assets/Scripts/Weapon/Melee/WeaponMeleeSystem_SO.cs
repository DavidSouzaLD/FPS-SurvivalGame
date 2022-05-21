using UnityEngine;

[CreateAssetMenu(fileName = "New MeleeSO", menuName = "ProjectSouza/Weapons/MeleeSO")]
public class WeaponMeleeSystem_SO : ScriptableObject
{
    [Header("Settings:")]
    public LayerMask layerTarget;
    public float maxRange;
    public float areaDamage;
    public int maxAttackAnimations;

    [Header("Damage/Force:")]
    public float minDamage = 15f;
    public float maxDamage = 30f;
    [Space]
    public float minForce = 10f;
    public float maxForce = 30f;

    [Header("Sounds:")]
    public AudioClip[] attackSounds;
}