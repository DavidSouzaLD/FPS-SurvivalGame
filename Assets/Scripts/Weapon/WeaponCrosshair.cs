using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[ExecuteInEditMode]
public class WeaponCrosshair : MonoBehaviour
{
    [Header("Crosshair Settings:")]
    public bool enable;
    [SerializeField] private WeaponCrosshair_SO crossData;
    [SerializeField] private float startSize = 80;
    [SerializeField] private float maxSize = 200;
    [SerializeField] private float resetSpeed = 2;
    [Space]
    [SerializeField] private CustomizeCrosshair m_CustomizeCrosshair;
    [SerializeField] private Crosshair m_Crosshair;
    [HideInInspector] public RectTransform crosshairArea;

    private void Start()
    {
        if (crosshairArea == null)
        {
            crosshairArea = GetComponent<RectTransform>();
        }
    }

    private void LateUpdate()
    {
        if (crossData != null)
        {
            CustomizeCrosshair cc = m_CustomizeCrosshair;

            if (cc.color != crossData.color)
                crossData.color = cc.color;

            if (cc.outlineColor != crossData.outlineColor)
                crossData.outlineColor = cc.outlineColor;

            if (cc.useCross != crossData.useCross)
                cc.useCross = crossData.useCross;

            if (cc.usePoint != crossData.usePoint)
                cc.usePoint = crossData.usePoint;

            if (startSize != crossData.startSize)
                startSize = crossData.startSize;

            if (maxSize != crossData.maxSize)
                maxSize = crossData.maxSize;

            if (cc.height != crossData.height)
                cc.height = crossData.height;

            if (cc.width != crossData.width)
                cc.width = crossData.width;

            if (cc.pointScale != crossData.pointScale)
                cc.pointScale = crossData.pointScale;
        }

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
            m_Crosshair.GetList()[i].enabled = enable;
            m_Crosshair.center.enabled = enable;

            m_Crosshair.GetList()[i].enabled = enable && m_CustomizeCrosshair.useCross;
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