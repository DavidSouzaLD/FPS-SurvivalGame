using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeaponSystem : MonoBehaviour
{
    [System.Serializable]
    public class HitImpact
    {
        public string name;
        public GameObject prefab;
        public string tag = "Untagged";
    }

    [SerializeField] public WeaponSystem_SO WeaponSO;

    [Header("Bullet:")]
    [SerializeField] private int bulletsInMag = 30;
    [SerializeField] private int bulletsInBag = 30;
    [SerializeField] private int maxBulletsPerMag = 30;

    [Header("MuzzleFlash:")]
    [SerializeField] private Transform muzzlePosition;
    [SerializeField] private GameObject[] muzzlePrefabs;

    [Header("Hits Impact:")]
    [SerializeField] private HitImpact[] hitImpacts;
    [SerializeField] private float hitScale = 0.1f;
    [SerializeField] private float hitDestroyTime = 2f;

    private float firerateTimer = 0;
    private Vector3 startPosition;

    private bool isReloading;
    private bool isAiming;
    private bool isRunning;

    private Input m_Input;
    private AudioSource m_AudioSource;
    private Animator m_Animator;

    private PlayerCamera m_PlayerCamera;
    private WeaponCrosshair m_Crosshair;
    private WeaponSway m_Sway;
    private WeaponRecoil m_Recoil;

    public void PlaySound(AudioClip clip) => m_AudioSource.PlayOneShot(clip);

    private void Start()
    {
        m_Input = GameObject.FindObjectOfType<Input>();
        m_AudioSource = GetComponent<AudioSource>();
        m_Animator = GetComponentInChildren<Animator>();
        m_Crosshair = GameObject.FindObjectOfType<WeaponCrosshair>();
        m_Sway = GameObject.FindObjectOfType<WeaponSway>();
        m_Recoil = GameObject.FindObjectOfType<WeaponRecoil>();
        m_PlayerCamera = GameObject.FindObjectOfType<PlayerCamera>();

        startPosition = transform.localPosition;
    }

    private void Update()
    {
        // Fire
        bool canFire = firerateTimer <= 0 && !isReloading && !isRunning;
        bool autoFire = m_Input.KeyFire1();
        bool semiFire = m_Input.KeyFireTap1();

        if (firerateTimer > 0)
        {
            firerateTimer -= Time.deltaTime;
        }

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

        // Reload
        bool canReload = !isReloading && bulletsInMag < maxBulletsPerMag && bulletsInBag > 0 && !isRunning;
        bool reloadKey = m_Input.KeyReload();

        if (canReload && reloadKey)
        {
            ReloadInit();
        }

        // Aim 
        bool canAim = !isRunning;
        bool aimKey = m_Input.KeyFire2();

        if (canAim && aimKey)
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

        // Running
        bool canRun = !isReloading && !isAiming;
        bool runKey = m_Input.KeyRun();
        isRunning = runKey;
        m_Animator.SetBool("Run", runKey);
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
              m_PlayerCamera.GetCamera().transform.forward.x + Random.Range(-WeaponSO.recoilSpread * crosshairNormalized, WeaponSO.recoilSpread * crosshairNormalized),
              m_PlayerCamera.GetCamera().transform.forward.y + Random.Range(-WeaponSO.recoilSpread * crosshairNormalized, WeaponSO.recoilSpread * crosshairNormalized),
              m_PlayerCamera.GetCamera().transform.forward.z
            );

            RaycastHit[] hits = Physics.RaycastAll(m_PlayerCamera.GetCamera().transform.position, forward, WeaponSO.maxRange, WeaponSO.layerTarget);
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
                    for (int h = 0; h < hitImpacts.Length; h++)
                    {
                        GameObject hitPrefab = FindHitWithTag(hits[i].transform.tag);

                        if (hitPrefab != null)
                        {
                            GameObject hitMark = Instantiate(hitPrefab, hits[i].point * 1.0001f, Quaternion.LookRotation(hits[i].normal));
                            hitMark.transform.Translate(-hitMark.transform.forward * 0.001f);
                            hitMark.transform.localScale = Vector3.one * hitScale;
                            hitMark.transform.SetParent(hits[i].transform, true);
                            Destroy(hitMark, hitDestroyTime);
                        }
                        else if (hitImpacts[0] != null)
                        {
                            GameObject hitMark = Instantiate(hitImpacts[0].prefab, hits[i].point, Quaternion.LookRotation(hits[i].normal));
                            hitMark.transform.Translate(-hitMark.transform.forward * 0.001f);
                            hitMark.transform.localScale = Vector3.one * hitScale;
                            hitMark.transform.SetParent(hits[i].transform, true);
                            Destroy(hitMark, hitDestroyTime);
                        }
                    }

                    // Add force
                    if (target.rigidbody != null)
                    {
                        target.rigidbody.AddForceAtPosition(m_PlayerCamera.GetCamera().transform.forward * Random.Range(WeaponSO.minForce, WeaponSO.maxForce) * Time.deltaTime, hits[i].point, ForceMode.Impulse);
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

    public GameObject FindHitWithTag(string _tag)
    {
        for (int i = 0; i < hitImpacts.Length; i++)
        {
            if (hitImpacts[i].tag.ToLower() == _tag.ToLower())
            {
                return hitImpacts[i].prefab;
            }
        }

        return null;
    }

    private void OnDrawGizmos()
    {
        if (m_PlayerCamera == null)
        {
            m_PlayerCamera = GameObject.FindObjectOfType<PlayerCamera>();
        }
        else
        {
            if (WeaponSO != null && m_PlayerCamera.GetCamera() != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawRay(m_PlayerCamera.GetCamera().transform.position, m_PlayerCamera.GetCamera().transform.forward * WeaponSO.maxRange);
            }
        }
    }
}