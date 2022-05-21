using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeaponSystem : MonoBehaviour
{
    [SerializeField] public WeaponSystem_SO WeaponSO;

    [Header("Bullet:")]
    [SerializeField] private int bulletsInMag = 30;
    [SerializeField] private int bulletsInBag = 30;
    [SerializeField] private int maxBulletsPerMag = 30;

    [Header("MuzzleFlash:")]
    [SerializeField] private Transform muzzlePosition;
    [SerializeField] private GameObject[] muzzlePrefabs;

    private float firerateTimer = 0;
    private Vector3 startPosition;

    private bool isReloading;
    private bool isAiming;

    private Input m_Input;
    private Camera m_Camera;
    private AudioSource m_AudioSource;
    private Animator m_Animator;

    private WeaponCrosshair m_Crosshair;
    private WeaponSway m_Sway;
    private WeaponRecoil m_Recoil;

    public void PlaySound(AudioClip clip) => m_AudioSource.PlayOneShot(clip);

    private void Start()
    {
        m_Input = GameObject.FindObjectOfType<Input>();
        m_Camera = Camera.main;
        m_AudioSource = GetComponent<AudioSource>();
        m_Animator = GetComponentInChildren<Animator>();

        m_Crosshair = GameObject.FindObjectOfType<WeaponCrosshair>();
        m_Sway = GameObject.FindObjectOfType<WeaponSway>();
        m_Recoil = GameObject.FindObjectOfType<WeaponRecoil>();

        startPosition = transform.localPosition;
    }

    private void Update()
    {
        FireUpdate();
        ReloadUpdate();
        AimUpdate();
    }

    private void FireUpdate()
    {
        // Fire
        bool canFire = firerateTimer <= 0 && !isReloading;
        bool autoFire = m_Input.KeyFire1();
        bool semiFire = m_Input.KeyFireTap1();

        if (canFire)
        {
            switch (WeaponSO.fireType)
            {
                case WeaponSystem_SO.FireType.Semi:
                    if (semiFire)
                    {
                        Fire();
                    }
                    break;

                case WeaponSystem_SO.FireType.Automatic:
                    if (autoFire)
                    {
                        Fire();
                    }
                    break;
            }
        }

        // Firerate
        if (firerateTimer > 0)
        {
            firerateTimer -= Time.deltaTime;
        }
    }

    private void ReloadUpdate()
    {
        bool canReload = !isReloading && bulletsInMag < maxBulletsPerMag && bulletsInBag > 0;
        bool reloadKey = m_Input.KeyReload();

        if (canReload && reloadKey)
        {
            ReloadInit();
        }
    }

    public void ReloadInit()
    {
        isReloading = true;
        m_Animator.Play("Reload", 0);
    }

    public void ReloadComplete()
    {
        for (int i = 0; i < maxBulletsPerMag; i++)
        {
            if (bulletsInBag > 0 && bulletsInMag < maxBulletsPerMag)
            {
                bulletsInMag++;
                bulletsInBag--;
            }
        }

        isReloading = false;
    }

    private void AimUpdate()
    {
        bool aimKey = m_Input.KeyFire2();

        if (aimKey)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, WeaponSO.aimPosition, WeaponSO.aimSpeed * Time.deltaTime);
            m_Sway.accuracy = 0.1f;
            m_Crosshair.crosshairArea.sizeDelta = Vector2.zero;
            m_Crosshair.enable = false;

            isAiming = true;
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, startPosition, WeaponSO.aimSpeed * Time.deltaTime);
            m_Sway.accuracy = 1f;
            m_Crosshair.enable = true;

            isAiming = false;
        }
    }

    private void Fire()
    {
        if (bulletsInMag < 1)
        {
            m_AudioSource.PlayOneShot(WeaponSO.emptySound);
            firerateTimer = !isAiming ? WeaponSO.firerate : WeaponSO.firerate * WeaponSO.aimFirerateMultiplier;
        }
        else
        {
            float crosshairNormalized = Mathf.InverseLerp(m_Crosshair.startSize, m_Crosshair.maxSize, m_Crosshair.crosshairArea.sizeDelta.x);

            Vector3 forward = new Vector3(
              m_Camera.transform.forward.x + Random.Range(-WeaponSO.recoilSpread * crosshairNormalized, WeaponSO.recoilSpread * crosshairNormalized),
              m_Camera.transform.forward.y + Random.Range(-WeaponSO.recoilSpread * crosshairNormalized, WeaponSO.recoilSpread * crosshairNormalized),
              m_Camera.transform.forward.z
            );

            RaycastHit[] hits = Physics.RaycastAll(m_Camera.transform.position, forward, WeaponSO.maxRange, WeaponSO.layerTarget);
            int maxTargets = hits.Length;

            if (maxTargets > WeaponSO.maxPenetration)
            {
                maxTargets = WeaponSO.maxPenetration;
            }

            for (int i = 0; i < maxTargets; i++)
            {
                RaycastHit target = hits[i];

                if (target.collider != null)
                {
                    // Hit mark
                    GameObject hitMark = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    hitMark.transform.position = hits[i].point;
                    hitMark.transform.localScale = Vector3.one * 0.1f;
                    Destroy(hitMark.GetComponent<Collider>());
                    Destroy(hitMark, 2f);

                    // Add force
                    if (target.rigidbody != null)
                    {
                        target.rigidbody.AddForceAtPosition(m_Camera.transform.forward * Random.Range(WeaponSO.minForce, WeaponSO.maxForce) * Time.deltaTime, hits[i].point, ForceMode.Impulse);
                    }

                    // Damage
                    IDamageable<float> damageable = target.collider.GetComponent<IDamageable<float>>();

                    if (damageable != null)
                    {
                        damageable.TakeDamage(Random.Range(WeaponSO.minDamage, WeaponSO.maxDamage));
                    }
                }
                else
                {
                    break;
                }
            }

            // Extras
            m_Crosshair.AddForce(WeaponSO.crosshairForce);
            m_Recoil.AddForce();

            // Sounds
            int randomShoot = Random.Range(0, WeaponSO.fireSounds.Length);
            PlaySound(WeaponSO.fireSounds[randomShoot]);

            // Animator
            m_Animator.Play("Fire", 0);

            // MuzzleFlash
            int muzzleIndex = Random.Range(0, muzzlePrefabs.Length);
            GameObject muzzle = Instantiate(muzzlePrefabs[muzzleIndex], muzzlePosition.position, muzzlePosition.rotation);
            muzzle.transform.parent = muzzlePosition;
            Destroy(muzzle, 0.05f);

            // Shoot reset
            bulletsInMag--;
            firerateTimer = !isAiming ? WeaponSO.firerate : WeaponSO.firerate * WeaponSO.aimFirerateMultiplier;
        }
    }

    private void OnDrawGizmos()
    {
        if (m_Camera == null)
        {
            m_Camera = Camera.main;
        }
        else if (WeaponSO != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(m_Camera.transform.position, m_Camera.transform.forward * WeaponSO.maxRange);
        }
    }
}