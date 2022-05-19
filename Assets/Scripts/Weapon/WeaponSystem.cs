using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public enum FireType { Semi, Automatic }

    [Header("Fire:")]
    [SerializeField] private FireType fireType;
    [SerializeField] private LayerMask layerTarget;
    [SerializeField] private float firerate = 0.1f;
    [SerializeField] private float crosshairForce = 30f;
    [SerializeField] private float maxRange = 500f;
    [SerializeField] private int maxPenetration = 1;

    [Header("Recoil:")]
    [SerializeField] private float recoilSpread = 5f;

    [Header("Bullet:")]
    [SerializeField] private int bulletsInMag = 30;
    [SerializeField] private int bulletsInBag = 30;
    [SerializeField] private int maxBulletsPerMag = 30;

    [Header("Aim:")]
    [SerializeField] private float aimSpeed = 5f;
    [SerializeField] private Vector3 aimPosition;

    [Header("Damage/Force:")]
    [SerializeField] private float minDamage = 15f;
    [SerializeField] private float maxDamage = 30f;
    [Space]
    [SerializeField] private float minForce = 10f;
    [SerializeField] private float maxForce = 30f;



    private float firerateTimer = 0;
    private float recoilSpreadTimer = 0;
    private Vector3 startPosition;

    private bool isReloading;
    private bool isAiming;

    private Input m_Input;
    private WeaponCrosshair m_Crosshair;
    private WeaponSway m_Sway;
    private Camera m_Camera;

    private void Start()
    {
        m_Input = GameObject.FindObjectOfType<Input>();
        m_Crosshair = GameObject.FindObjectOfType<WeaponCrosshair>();
        m_Sway = GameObject.FindObjectOfType<WeaponSway>();
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
            switch (fireType)
            {
                case FireType.Semi:
                    if (semiFire)
                    {
                        Fire();
                    }
                    break;

                case FireType.Automatic:
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

        // Recoil
        if (recoilSpreadTimer > 0)
        {
            recoilSpreadTimer -= Time.deltaTime;
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
            transform.localPosition = Vector3.Lerp(transform.localPosition, aimPosition, aimSpeed * Time.deltaTime);
            m_Sway.accuracy = 0.1f;
            m_Crosshair.crosshairArea.sizeDelta = Vector2.zero;
            m_Crosshair.enable = false;
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, startPosition, aimSpeed * Time.deltaTime);
            m_Sway.accuracy = 1f;
            m_Crosshair.enable = true;
        }
    }

    private void Fire()
    {
        float crosshairNormalized = Mathf.InverseLerp(m_Crosshair.startSize, m_Crosshair.maxSize, m_Crosshair.crosshairArea.sizeDelta.x);

        Vector3 forward = new Vector3(
          m_Camera.transform.forward.x + Random.Range(-recoilSpread * crosshairNormalized, recoilSpread * crosshairNormalized),
          m_Camera.transform.forward.y + Random.Range(-recoilSpread * crosshairNormalized, recoilSpread * crosshairNormalized),
          m_Camera.transform.forward.z
        );

        RaycastHit[] hits = Physics.RaycastAll(m_Camera.transform.position, forward, maxRange, layerTarget);
        int maxTargets = hits.Length;

        if (maxTargets > maxPenetration)
        {
            maxTargets = maxPenetration;
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
                    target.rigidbody.AddForceAtPosition(m_Camera.transform.forward * Random.Range(minForce, maxForce) * Time.deltaTime, hits[i].point, ForceMode.Impulse);
                }

                // Damage
                IDamageable<float> damageable = target.collider.GetComponent<IDamageable<float>>();

                if (damageable != null)
                {
                    damageable.TakeDamage(Random.Range(minDamage, maxDamage));
                }
            }
            else
            {
                break;
            }
        }

        m_Crosshair.AddForce(crosshairForce);

        bulletsInMag--;
        firerateTimer = firerate;
    }

    private void OnDrawGizmos()
    {
        if (m_Camera == null)
        {
            m_Camera = Camera.main;
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(m_Camera.transform.position, m_Camera.transform.forward * maxRange);
    }
}