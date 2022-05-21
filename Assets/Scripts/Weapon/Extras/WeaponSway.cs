using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Range(0, 1)] public float accuracy = 1;
    [Space]
    [SerializeField] private float smoothSpeed = 2;
    [SerializeField] private float resetSpeed = 5;
    [SerializeField] private float moveAmount = 1;
    private Input m_Input;
    private Quaternion startRot;

    private void Start()
    {
        m_Input = GameObject.FindObjectOfType<Input>();
        startRot = this.transform.localRotation;
    }

    private void Update()
    {
        // Apply movement
        Vector2 mouseAxis = m_Input.MouseAxis();

        if (mouseAxis != Vector2.zero)
        {
            Quaternion quat = Quaternion.Euler((mouseAxis.y * moveAmount * 2) * accuracy, (-mouseAxis.x * moveAmount) * accuracy, transform.localRotation.z);
            transform.localRotation = Quaternion.Lerp(transform.localRotation,
            transform.localRotation * quat, Time.deltaTime * smoothSpeed);
        }

        // Reset for start rotation
        transform.localRotation = Quaternion.Lerp(transform.localRotation,
                        startRot, Time.deltaTime * resetSpeed);
    }
}
