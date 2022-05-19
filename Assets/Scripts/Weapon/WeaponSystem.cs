using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private WeaponSystem_SO WeaponSO;

    [Header("Bullet:")]
    [SerializeField] private int bulletsInMag = 30;
    [SerializeField] private int bulletsInBag = 30;
    [SerializeField] private int maxBulletsPerMag = 30;

    private float firerateTimer = 0;
    private Vector3 startPosition;

    private bool isReloading;
    private bool isAiming;

    private Input m_Input;
    private WeaponCrosshair m_Crosshair;
    private WeaponSway m_Sway;
    private WeaponRecoil m_Recoil;
    private Camera m_Camera;

    private void Start()
    {
        m_Input = GameObject.FindObjectOfType<Input>();
        m_Crosshair = GameObject.FindObjectOfType<WeaponCrosshair>();
        m_Sway = GameObject.FindObjectOfType<WeaponSway>();
        m_Recoil = GameObject.FindObjectOfType<WeaponRecoil>();
        m_Camera = Camera.main;

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
        bool canFire = bulletsInMag > 0 && firerateTimer <= 0;
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
            isReloading = true;

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

        m_Crosshair.AddForce(WeaponSO.crosshairForce);
        m_Recoil.AddForce();

        bulletsInMag--;
        firerateTimer = !isAiming ? WeaponSO.firerate : WeaponSO.firerate * WeaponSO.aimFirerateMultiplier;
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