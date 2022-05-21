using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeaponMeleeSystem : MonoBehaviour
{
    [SerializeField] private WeaponMeleeSystem_SO MeleeSO;
    private bool isAttacking;
    private Input m_Input;
    private Camera m_Camera;
    private AudioSource m_AudioSource;
    private Animator m_Animator;
    private WeaponCrosshair m_Crosshair;

    public void PlaySound(AudioClip clip) => m_AudioSource.PlayOneShot(clip);

    private void Start()
    {
        m_Input = GameObject.FindObjectOfType<Input>();
        m_Camera = Camera.main;
        m_AudioSource = GetComponent<AudioSource>();
        m_Animator = GetComponentInChildren<Animator>();
        m_Crosshair = GameObject.FindObjectOfType<WeaponCrosshair>();
    }

    private void Update()
    {
        AttackUpdate();
    }

    private void AttackUpdate()
    {
        bool keyAttack = m_Input.KeyFireTap1() && !isAttacking;

        if (keyAttack)
        {
            AttackInit();
        }
    }

    private void AttackInit()
    {
        int attackAnimation = Random.Range(1, MeleeSO.maxAttackAnimations + 1) - 1;
        string animationName = "Attack" + attackAnimation;

        m_Animator.Play(animationName, 0, 0f);
        isAttacking = true;
    }

    public void ApplyDamage()
    {
        // Damage
        Ray ray = new Ray(m_Camera.transform.position, m_Camera.transform.forward * MeleeSO.maxRange);
        Collider[] colliders = Physics.OverlapSphere(ray.GetPoint(MeleeSO.maxRange), MeleeSO.areaDamage, MeleeSO.layerTarget);

        for (int i = 0; i < colliders.Length; i++)
        {
            Collider target = colliders[i];

            if (target != null)
            {
                // Add force
                if (target.attachedRigidbody != null)
                {
                    target.attachedRigidbody.AddForceAtPosition(m_Camera.transform.forward * Random.Range(MeleeSO.minForce, MeleeSO.maxForce) * Time.deltaTime, colliders[i].transform.position, ForceMode.Impulse);
                }

                // Damage
                IDamageable<float> damageable = target.GetComponent<IDamageable<float>>();

                if (damageable != null)
                {
                    damageable.TakeDamage(Random.Range(MeleeSO.minDamage, MeleeSO.maxDamage));
                }
            }
            else
            {
                break;
            }
        }

        // Sound
        int attackSound = Random.Range(0, MeleeSO.attackSounds.Length);
        PlaySound(MeleeSO.attackSounds[attackSound]);

        // Extras
        m_Crosshair.AddForce(m_Crosshair.maxSize);
    }

    public void AttackComplete()
    {
        isAttacking = false;
    }

    private void OnDrawGizmos()
    {
        if (m_Camera != null && MeleeSO != null)
        {
            // Ray
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(m_Camera.transform.position, m_Camera.transform.forward * MeleeSO.maxRange);

            // Local damage
            Gizmos.color = Color.red;
            Ray ray = new Ray(m_Camera.transform.position, m_Camera.transform.forward * MeleeSO.maxRange);
            Gizmos.DrawWireSphere(ray.GetPoint(MeleeSO.maxRange), MeleeSO.areaDamage);
        }
        else
        {
            m_Camera = Camera.main;
        }
    }
}
