using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TaggingMinigame : MonoBehaviour
{

    [SerializeField] private GameObject m_player;
    [SerializeField] private GameObject m_tagPrefab;
    [SerializeField] private GameObject m_sharkPrefab;
    [SerializeField] private Transform m_sharkSpawnPoint;
    [SerializeField] private Transform m_tagSpawnPoint;
    [SerializeField] private Canvas grabPrompt;
    [SerializeField] private Canvas placePrompt;


    private Transform m_tagPlacedArea;
    private GameObject m_shark;
    private TaggingShark m_sharkScript;
    private GameObject m_currentTag;

    private XRGrabInteractable m_tag;
    private bool firstTag;

    private void OnEnable()
    {
        CreateShark();
    }

    private void Start()
    {
        firstTag = true;
    }

    private void OnDisable()
    {
        m_sharkScript.TagPlaced -= PlaceTag;
        m_tag.selectEntered.RemoveListener(swapActiveLabel);
    }

    private void Update()
    {
        if (Vector3.Distance(m_player.transform.position, m_currentTag.transform.position) > 10)
        {
            ResetTag();
        }
    }

    private void ResetTag()
    {
        Destroy(m_currentTag);
        m_currentTag = Instantiate(m_tagPrefab, m_tagSpawnPoint.position, m_tagSpawnPoint.rotation);
        m_tag = m_currentTag.GetComponent<XRGrabInteractable>();
        m_tag.selectEntered.AddListener(swapActiveLabel);
    }

    private void PlaceTag()
    {
        m_tag.selectEntered.RemoveListener(swapActiveLabel);
        m_currentTag.GetComponent<XRGrabInteractable>().enabled = false;
        m_currentTag.GetComponent<Rigidbody>().isKinematic = true;
        m_currentTag.transform.position = m_tagPlacedArea.position;
        
        DestroyShark();
        ResetTag();
        CreateShark();
    }

    private void CreateShark()
    {
        m_shark = Instantiate(m_sharkPrefab, m_sharkSpawnPoint.position, m_sharkSpawnPoint.rotation);
        m_tagPlacedArea = m_sharkPrefab.transform.GetChild(0);
        m_sharkScript = m_shark.GetComponent<TaggingShark>();
        m_currentTag = Instantiate(m_tagPrefab, m_tagSpawnPoint.position, m_tagSpawnPoint.rotation);

        m_sharkScript.TagPlaced += PlaceTag;
    }


    private void DestroyShark()
    {
        Destroy(m_shark);
        m_sharkScript.TagPlaced -= PlaceTag;
    }

    private void swapActiveLabel(SelectEnterEventArgs args)
    {

    }

}
