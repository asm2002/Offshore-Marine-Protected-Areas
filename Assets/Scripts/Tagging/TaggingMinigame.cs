using DG.Tweening;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using DG.Tweening;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TaggingMinigame : MonoBehaviour
{

    private enum GameState
    {
        tutorial,
        game,
        end
    }

    [SerializeField] private GameObject m_player;
    [SerializeField] private GameObject m_tagPrefab;
    [SerializeField] private GameObject m_sharkPrefab;
    [SerializeField] private Transform m_sharkSpawnPoint;
    [SerializeField] private Transform m_tagSpawnPoint;
    [SerializeField] private Canvas grabPrompt;
    [SerializeField] private Canvas placePrompt;
    [SerializeField] private GameState curState = GameState.tutorial;
    [SerializeField] private TMP_Text countDownText;
    [SerializeField] private string startText = "Tag the sharks!";
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text text;
    [SerializeField] private float countdownTime = 3;
    [SerializeField] private float gameTime = 60;


    private Transform m_tagPlacedArea;
    private GameObject m_shark;
    private TaggingShark m_sharkScript;
    private GameObject m_currentTag;
    private float gameTimeElapsed;

    private XRGrabInteractable m_tag;
    private bool firstTag;

    private void OnEnable()
    {
        CreateShark();
    }

    private void Start()
    {
        firstTag = true;
        ResetTag();
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
        m_tag = m_currentTag.GetComponentInChildren<XRGrabInteractable>();
        m_tag.selectEntered.AddListener(swapActiveLabel);
        m_tag.selectExited.AddListener(swapActiveLabel);
    }

    private void PlaceTag()
    {
        m_tag.selectEntered.RemoveListener(swapActiveLabel);
        m_tag.selectExited.RemoveListener(swapActiveLabel);
        m_currentTag.GetComponent<XRGrabInteractable>().enabled = false;
        m_currentTag.GetComponent<Rigidbody>().isKinematic = true;
        m_currentTag.transform.position = m_tagPlacedArea.position;

        if (curState == GameState.tutorial)
        {
            ResetTag();
        }
        
        DestroyShark();
        CreateShark();
    }

    private void CreateShark()
    {
        m_shark = Instantiate(m_sharkPrefab, m_sharkSpawnPoint.position, m_sharkSpawnPoint.rotation);
        m_tagPlacedArea = m_sharkPrefab.transform.GetChild(0);
        m_sharkScript = m_shark.GetComponent<TaggingShark>();

        m_sharkScript.TagPlaced += PlaceTag;
    }


    private void DestroyShark()
    {
        m_sharkScript.TagPlaced -= PlaceTag;
        Destroy(m_shark);
    }

    private void swapActiveLabel(SelectEnterEventArgs args)
    {

        if (curState == GameState.tutorial)
        {
            if (grabPrompt.gameObject.activeSelf)
            {
                grabPrompt.gameObject.SetActive(false);
                placePrompt.gameObject.SetActive(true);
            }
            else
            {
                grabPrompt.gameObject.SetActive(true);
                placePrompt.gameObject.SetActive(false);
            }
        }
    }

    private void swapActiveLabel(SelectExitEventArgs args)
    {
        if (grabPrompt.gameObject.activeSelf)
        {
            grabPrompt.gameObject.SetActive(false);
            placePrompt.gameObject.SetActive(true);
        }
        else
        {
            grabPrompt.gameObject.SetActive(true);
            placePrompt.gameObject.SetActive(false);
        }
    }

    private IEnumerator StartGame()
    {
        countDownText.SetText(startText);
        yield return new WaitForSeconds(5);
        countDownText.SetText("3");
        yield return new WaitForSeconds(1);
        countDownText.SetText("2");
        yield return new WaitForSeconds(1);
        countDownText.SetText("1");
        yield return new WaitForSeconds(1);

    }

}
