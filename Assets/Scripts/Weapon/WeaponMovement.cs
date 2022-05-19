using UnityEngine;

public class WeaponMovement : MonoBehaviour
{
    [SerializeField] private float smoothSpeed = 2;
    [SerializeField] private float resetSpeed = 3;
    [SerializeField] private float moveAmount = 5;
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
        Vector2 keyAxis = m_Input.KeyAxis();

        if (keyAxis.x != 0)
        {
            Quaternion quat = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, -keyAxis.x * moveAmount * 2);
            transform.localRotation = Quaternion.Slerp(transform.localRotation,
            transform.localRotation * quat, Time.deltaTime * smoothSpeed);
        }

        // Reset for start rotation
        transform.localRotation = Quaternion.Lerp(transform.localRotation,
                        startRot, Time.deltaTime * resetSpeed);
    }
}
