using UnityEngine;

[RequireComponent(typeof(Input))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement:")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityMultiplier;

    [Header("Ground Check:")]
    [SerializeField] private float heightCheck;
    [SerializeField] private float radiusCheck;
    [SerializeField] private LayerMask layersCheck;
    [Space]
    [SerializeField] private MouseLook m_MouseLook;

    // Components
    private CharacterController m_CharacterController;
    private Rigidbody m_Rigidbody;

    // Hidden
    private Camera m_Camera;
    private Input m_Input;

    private void Start()
    {
        // Get InputMap
        if (m_Input == null)
        {
            if (GetComponent<Input>() != null)
            {
                m_Input = GetComponent<Input>();
            }
            else
            {
                m_Input = gameObject.AddComponent<Input>();
            }
        }

        // Get camera transform
        m_Camera = GetComponentInChildren<Camera>();
        m_MouseLook = new MouseLook(m_Camera.transform, m_MouseLook.m_Sensitivity);
        m_MouseLook.LockCursor(true);

        // Get components
        m_CharacterController = GetComponent<CharacterController>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector2 mouseAxis = m_Input.MouseAxis();

        m_MouseLook.ChangeRotation(
            new Vector2(
                mouseAxis.x,
                mouseAxis.y
            ),
            this.transform
        );
    }

    private void FixedUpdate()
    {
        // Movement
        Vector2 keyAxis = m_Input.KeyAxis();
        float currentSpeed = m_Input.KeyRun() ? runSpeed : moveSpeed;

        Vector3 direction = transform.right * keyAxis.x + transform.forward * keyAxis.y;
        Vector3 move = direction * currentSpeed;

        if (IsGrounded())
        {
            // Jump
            if (m_Input.KeyJump())
            {
                move.y += (jumpForce * 100f) * Time.fixedDeltaTime;
            }
        }
        else
        {
            // Gravity
            move.y += m_CharacterController.velocity.y + (Physics.gravity.y * gravityMultiplier);
        }

        m_CharacterController.Move(move * Time.fixedDeltaTime);
    }

    private bool IsGrounded()
    {
        Collider[] colliders = Physics.OverlapSphere(new Vector3(
             transform.position.x,
             transform.position.y + heightCheck,
             transform.position.z
         ), radiusCheck, layersCheck);

        if (colliders.Length > 0)
        {
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = IsGrounded() ? Color.green : Color.red;
        Gizmos.DrawWireSphere(new Vector3(
            transform.position.x,
            transform.position.y + heightCheck,
            transform.position.z
        ), radiusCheck);
    }

    [System.Serializable]
    partial class MouseLook
    {
        public float m_Sensitivity;
        private Transform m_CameraTransform;
        private Vector2 cameraRot;
        private Quaternion originalRotation;

        public MouseLook(Transform m_GetCamera, float m_GetSensisivity)
        {
            m_CameraTransform = m_GetCamera;
            m_Sensitivity = m_GetSensisivity;

            Init();
        }

        public void Init()
        {
            originalRotation = m_CameraTransform.localRotation;
        }

        public void ChangeRotation(Vector2 m_GetRotation, Transform m_BodyTransform)
        {
            cameraRot.x += m_GetRotation.x * m_Sensitivity;
            cameraRot.y += m_GetRotation.y * m_Sensitivity;

            cameraRot.x = Mathf.Clamp(cameraRot.x, -360f, 360f);
            cameraRot.y = Mathf.Clamp(cameraRot.y, -85f, 90f);

            //Quaternion xQuaternion = Quaternion.AngleAxis(cameraRot.x, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(cameraRot.y, -Vector3.right);

            m_CameraTransform.localRotation = originalRotation * yQuaternion;
            m_BodyTransform.Rotate(Vector3.up * m_GetRotation.x * m_Sensitivity);
        }

        public void LockCursor(bool m_LockedCursor)
        {
            if (m_LockedCursor)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
