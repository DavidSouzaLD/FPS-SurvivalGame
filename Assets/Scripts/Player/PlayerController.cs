using UnityEngine;

[RequireComponent(typeof(Input))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement:")]
    [SerializeField] private float moveSpeed = 4;
    [SerializeField] private float runSpeed = 7;
    [SerializeField] private float crouchMoveSpeed = 7;
    [SerializeField] private float jumpForce = 4;
    [SerializeField] private float gravityMultiplier = 0.05f;

    [Header("Crouch:")]
    [SerializeField] private float crouchSpeed = 7;
    [SerializeField] private float crouchUpJumpForce = 5f;
    [SerializeField] private float crouchCrosshairSize = 20;
    [Space]
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private float normalHeight = 2f;
    [SerializeField] private float currentCheckHeight = 2f;
    [SerializeField] private float crouchCheckHeight = 0.5f;
    [SerializeField] private float normalCheckHeight = 2f;

    [Header("Ground Check:")]
    [SerializeField] private float radiusCheck = 0.2f;
    [SerializeField] private LayerMask layersCheck;

    [Header("Affect Weapon:")]
    [SerializeField] private float crosshairForceInWalk = 35f;
    [SerializeField] private float crosshairForceInJump = 200f;

    [Space]
    [SerializeField] private MouseLook m_MouseLook;

    private WeaponCrosshair m_Crosshair;
    private CharacterController m_CharacterController;
    private Rigidbody m_Rigidbody;
    private Camera m_Camera;
    private Input m_Input;
    private bool cancelCrouch;

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
        m_Crosshair = GameObject.FindObjectOfType<WeaponCrosshair>();
    }

    private void Update()
    {
        // Camera rotation
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
        bool keyCrouch = m_Input.KeyCrouch();
        bool keyRun = m_Input.KeyRun();
        Vector2 keyAxis = m_Input.KeyAxis();
        float currentSpeed = keyCrouch ? crouchMoveSpeed : (keyRun ? runSpeed : moveSpeed);

        Vector3 direction = transform.right * keyAxis.x + transform.forward * keyAxis.y;
        Vector3 move = direction * currentSpeed;

        if (keyAxis != Vector2.zero)
        {
            m_Crosshair.AddForce(m_Input.KeyRun() ? crosshairForceInWalk * 2 * Time.deltaTime : crosshairForceInWalk * Time.deltaTime);
        }

        if (IsGrounded())
        {
            // Jump
            if (m_Input.KeyJump())
            {
                move.y += (jumpForce * 100f) * Time.fixedDeltaTime;
                m_Crosshair.AddForce(crosshairForceInJump);
            }

            //Crouch
            if (keyCrouch)
            {
                m_CharacterController.height = Mathf.Lerp(m_CharacterController.height, crouchHeight, crouchSpeed * Time.deltaTime);
                currentCheckHeight = Mathf.Lerp(currentCheckHeight, crouchCheckHeight, crouchSpeed * Time.deltaTime);

                cancelCrouch = false;
            }
            else
            {
                if (!cancelCrouch)
                {
                    move.y += crouchUpJumpForce;
                    cancelCrouch = true;
                }

                m_CharacterController.height = Mathf.Lerp(m_CharacterController.height, normalHeight, crouchSpeed * Time.deltaTime);
                currentCheckHeight = Mathf.Lerp(currentCheckHeight, normalCheckHeight, crouchSpeed * Time.deltaTime);
            }
        }

        if (!m_CharacterController.isGrounded)
        {
            // Gravity
            move.y += m_CharacterController.velocity.y + (Physics.gravity.y * gravityMultiplier);
        }

        m_CharacterController.Move(move * Time.fixedDeltaTime);
    }

    private bool IsGrounded()
    {
        if (m_Input != null)
        {
            bool keyCrouch = m_Input.KeyCrouch();

            Collider[] colliders = Physics.OverlapSphere(new Vector3(
                 transform.position.x,
                 transform.position.y - currentCheckHeight,
                 transform.position.z
             ), radiusCheck, layersCheck);

            if (colliders.Length > 0)
            {
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = IsGrounded() ? Color.green : Color.red;
        Gizmos.DrawWireSphere(new Vector3(
            transform.position.x,
            transform.position.y - currentCheckHeight,
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
