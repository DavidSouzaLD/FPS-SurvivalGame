using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[ExecuteInEditMode]
public class DynamicCrosshair : MonoBehaviour
{
    [Header("Crosshair Settings:")]
    [SerializeField] private bool editMode = true;
    [SerializeField] private float startSize = 80;
    [SerializeField] private float maxSize = 200;
    [SerializeField] private float resetSpeed = 2;
    [Space]
    [SerializeField] private CustomizeCrosshair m_CustomizeCrosshair;
    [SerializeField] private Crosshair m_Crosshair;
    [HideInInspector] public RectTransform crosshairArea;

    [Header("Crosshair Save/Load:")]
    [SerializeField] private string crosshairCode;
    [SerializeField] private bool applyCode = false;
    [SerializeField] private bool loadCode = false;

    private void Start()
    {
        editMode = false;

        if (crosshairArea == null)
        {
            crosshairArea = GetComponent<RectTransform>();
        }
    }

    private void ApplyCode(string code)
    {
        string[] values = code.Split(";", 7);

        for (int i = 0; i < values.Length; i++)
        {
            switch (i)
            {
                case 0:
                    startSize = float.Parse(values[0]);
                    Debug.Log(float.Parse(values[0]));
                    break;

                case 1:
                    maxSize = float.Parse(values[1]);
                    break;

                case 2:
                    m_CustomizeCrosshair.useCross = float.Parse(values[2]) == 0 ? false : true;
                    Debug.Log(float.Parse(values[2]) == 0 ? false : true);
                    break;

                case 3:
                    m_CustomizeCrosshair.height = float.Parse(values[3]);
                    break;

                case 4:
                    m_CustomizeCrosshair.width = float.Parse(values[4]);
                    break;

                case 5:
                    m_CustomizeCrosshair.usePoint = float.Parse(values[5]) == 0 ? false : true;
                    break;

                case 6:
                    m_CustomizeCrosshair.pointScale = float.Parse(values[6]);
                    break;
            }
        }
    }

    private void Update()
    {
        if (editMode)
        {
            // Get area
            if (crosshairArea == null) crosshairArea = transform.GetComponent<RectTransform>();

            // Get crosshairs
            if (m_Crosshair.top == null
            || m_Crosshair.down == null
            || m_Crosshair.left == null
            || m_Crosshair.right == null
            || m_Crosshair.center == null)
            {
                Image[] crosshairs = GetComponentsInChildren<Image>();

                foreach (Image cross in crosshairs)
                {
                    if (cross.transform.name.ToLower() == "top")
                    {
                        m_Crosshair.top = cross;
                    }

                    if (cross.transform.name.ToLower() == "down")
                    {
                        m_Crosshair.down = cross;
                    }

                    if (cross.transform.name.ToLower() == "left")
                    {
                        m_Crosshair.left = cross;
                    }

                    if (cross.transform.name.ToLower() == "right")
                    {
                        m_Crosshair.right = cross;
                    }

                    if (cross.transform.name.ToLower() == "center")
                    {
                        m_Crosshair.center = cross;
                    }
                }
            }

            // Cross
            // 0 Top / 1 Down / 2 left / 3 Right
            for (int i = 0; i < m_Crosshair.GetList().Length; i++)
            {
                m_Crosshair.GetList()[i].enabled = m_CustomizeCrosshair.useCross;
                m_Crosshair.GetList()[i].color = m_CustomizeCrosshair.color;
                m_Crosshair.GetList()[i].GetComponent<Outline>().effectColor = m_CustomizeCrosshair.outlineColor;
                m_Crosshair.center.GetComponent<Outline>().effectColor = m_CustomizeCrosshair.outlineColor;

                if (i == 0 || i == 1) { m_Crosshair.GetList()[i].rectTransform.sizeDelta = new Vector2(m_CustomizeCrosshair.height, m_CustomizeCrosshair.width); }
                if (i == 2 || i == 3) { m_Crosshair.GetList()[i].rectTransform.sizeDelta = new Vector2(m_CustomizeCrosshair.width, m_CustomizeCrosshair.height); }
            }

            // Point center
            m_Crosshair.center.enabled = m_CustomizeCrosshair.usePoint;
            m_Crosshair.center.color = m_CustomizeCrosshair.color;

            m_Crosshair.center.rectTransform.sizeDelta = new Vector2(m_CustomizeCrosshair.pointScale, m_CustomizeCrosshair.pointScale);

            if (!Application.IsPlaying(this))
            {
                Vector2 newSize = new Vector2(startSize, startSize);

                if (crosshairArea.sizeDelta != newSize)
                {
                    crosshairArea.sizeDelta = newSize;
                }

            }

            if (applyCode)
            {
                ApplyCode(crosshairCode);
                crosshairCode = "";
                applyCode = false;
            }

            if (loadCode)
            {
                crosshairCode =
                startSize + ";" +
                 maxSize + ";" +
                 (m_CustomizeCrosshair.useCross ? 1 : 0) + ";" +
                 m_CustomizeCrosshair.height + ";" +
                 m_CustomizeCrosshair.width + ";" +
                 (m_CustomizeCrosshair.usePoint ? 1 : 0) + ";" +
                 m_CustomizeCrosshair.pointScale;

                loadCode = false;
            }
        }

        ResetCrosshair();
    }

    public void AddForce(float force)
    {
        crosshairArea.sizeDelta += new Vector2(force, force);
        crosshairArea.sizeDelta = new Vector2(
            Mathf.Clamp(crosshairArea.sizeDelta.x, 0, maxSize),
            Mathf.Clamp(crosshairArea.sizeDelta.y, 0, maxSize)
        );
    }

    public void ResetCrosshair()
    {
        if (Application.IsPlaying(this))
        {
            Vector2 _startSize = new Vector2(startSize, startSize);

            if (crosshairArea.sizeDelta != _startSize)
            {
                crosshairArea.sizeDelta = Vector2.Lerp(
                    crosshairArea.sizeDelta, _startSize, Time.deltaTime * resetSpeed);
            }
        }
    }

    [System.Serializable]
    public class Crosshair
    {
        public Image top;
        public Image down;
        public Image left;
        public Image right;
        public Image center;

        public Image[] GetList()
        {
            List<Image> images = new List<Image>();

            images.Add(top);
            images.Add(down);
            images.Add(left);
            images.Add(right);

            Image[] changeList = images.ToArray();

            return changeList;
        }
    }

    [System.Serializable]
    public class CustomizeCrosshair
    {
        public bool useCross;
        public Color color = Color.red;
        public Color outlineColor = Color.red;
        [Range(0, 30)] public float height = 2;
        [Range(0, 30)] public float width = 8;
        [Space]
        public bool usePoint;
        [Range(0, 10)] public float pointScale = 2;
    }
}