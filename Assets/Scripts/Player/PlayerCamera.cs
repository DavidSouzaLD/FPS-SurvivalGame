using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(Volume))]
public class PlayerCamera : MonoBehaviour
{
    private Camera m_Camera;
    private Volume m_Volume;

    private void Start()
    {
        m_Camera = GetComponent<Camera>();
        m_Volume = GetComponent<Volume>();
    }

    public Camera GetCamera()
    {
        if (m_Camera == null)
        {
            m_Camera = GetComponent<Camera>();
        }

        return m_Camera;
    }
    public Volume GetVolume()
    {
        if (m_Volume == null)
        {
            m_Volume = GetComponent<Volume>();
        }

        return m_Volume;
    }
}
