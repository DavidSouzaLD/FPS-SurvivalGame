using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponSO", menuName = "ProjectSouza/WeaponSO")]
public class WeaponSystem_SO : ScriptableObject
{
    public enum FireType { Semi, Automatic }

    [Header("Fire:")]
    public FireType fireType;
    public LayerMask layerTarget;
    public float firerate = 0.1f;
    public float aimFirerateMultiplier = 2f;
    public float crosshairForce = 30f;
    public float maxRange = 500f;
    public int maxPenetration = 1;

    [Header("Recoil:")]
    public float recoilSpread = 5f;

    [Header("Aim:")]
    public float aimSpeed = 5f;
    public Vector3 aimPosition;

    [Header("Damage/Force:")]
    public float minDamage = 15f;
    public float maxDamage = 30f;
    [Space]
    public float minForce = 10f;
    public float maxForce = 30f;

    [Header("Sound:")]
    public AudioClip[] fireSounds;
    public AudioClip emptySound;
    [Space]
    public AudioClip reloadPickMagazine;
    public AudioClip reloadPutMagazine;
    public AudioClip reloadCocking;
}