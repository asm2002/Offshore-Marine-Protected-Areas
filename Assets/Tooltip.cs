using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

public class Tooltip : MonoBehaviour
{

    [SerializeField][Range(0.01f, 2f)] private float m_fadeTime = 0.5f;
    [SerializeField][Range(0f, 2f)] private float m_hoverStrength = 0.5f;
    [SerializeField][Range(0.01f, 2f)] private float m_hoverFrequency = 0.5f;

    private float m_positionY;
    private float m_hoverTimeElapsed;
    private float m_fadeTimeElapsed;

    private void Start()
    {
        m_positionY = transform.position.y;
        m_hoverTimeElapsed = 0;
        StartCoroutine(Fade(true));
    }

    void Update()
    {
        FaceCamera();
        Hover();
    }

    private void Hover()
    {
        transform.position = new Vector3(
            transform.position.x, 
            Mathf.Sin(m_hoverFrequency * m_hoverTimeElapsed) * m_hoverStrength + m_positionY + m_hoverStrength, 
            transform.position.z);
        m_hoverTimeElapsed += Time.deltaTime;
        m_hoverTimeElapsed %= 2 * Mathf.PI;
    }

    private void FaceCamera()
    {
        Vector3 direction = transform.position - Camera.main.transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    
    private IEnumerator Fade(bool dir)
    {

        float start;
        float end;
        if (dir)
        {
            start = 0; 
            end = 1;
        }
        else
        {
            start = 1; 
            end = 0;
        }

        m_fadeTimeElapsed = 0;
        while (m_fadeTimeElapsed < m_fadeTime)
        {
            float t = m_fadeTimeElapsed / m_fadeTime;
            FadeCanvasContents(start, end, t);
            m_fadeTimeElapsed += Time.deltaTime;
            yield return null;
        }

        FadeCanvasContents(start, end, 1);

    }

    private void FadeCanvasContents(float start, float end, float t)
    {
        foreach (Transform child in transform)
        {
            TMP_Text text = child.GetComponent<TMP_Text>();
            if (text != null)
            {
                Color color = text.color;
                color.a = Mathf.Lerp(start, end, t);
                text.color = color;
            }

            Image image = child.GetComponent<Image>();
            if (image != null)
            {
                Color color = image.color;
                color.a = Mathf.Lerp(start, end, t);
                image.color = color;
            }
        }
        
    }

    public IEnumerator DestroyTooltip()
    {
        yield return StartCoroutine(Fade(false));
        Destroy(gameObject);
    }

}
