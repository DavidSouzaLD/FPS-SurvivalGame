using UnityEngine;

public class Input : MonoBehaviour
{
    private InputMap m_InputMap;

    private void Awake() => m_InputMap = new InputMap();
    private void OnEnable() => m_InputMap.Enable();
    private void OnDisable() => m_InputMap.Disable();

    public Vector2 KeyAxis() => m_InputMap.Player.KeyAxis.ReadValue<Vector2>();
    public Vector2 MouseAxis() => m_InputMap.Player.MouseAxis.ReadValue<Vector2>();
    public bool KeyFire1() => m_InputMap.Player.Fire1.ReadValue<float>() != 0;
    public bool KeyFire2() => m_InputMap.Player.Fire2.ReadValue<float>() != 0;
    public bool KeyFireTap1() => m_InputMap.Player.FireTap1.ReadValue<float>() != 0;
    public bool KeyFireTap2() => m_InputMap.Player.FireTap2.ReadValue<float>() != 0;
    public bool KeyRun() => m_InputMap.Player.Run.ReadValue<float>() != 0 && KeyAxis().y > 0;
    public bool KeyJump() => m_InputMap.Player.Jump.ReadValue<float>() != 0;
    public bool KeyCrouch() => m_InputMap.Player.Crouch.ReadValue<float>() != 0;
    public bool KeyReload() => m_InputMap.Player.Reload.ReadValue<float>() != 0;
    public bool KeyESC() => m_InputMap.Player.ESC.ReadValue<float>() != 0;
    public bool KeyZoom() => m_InputMap.Player.Zoom.ReadValue<float>() != 0;
}
