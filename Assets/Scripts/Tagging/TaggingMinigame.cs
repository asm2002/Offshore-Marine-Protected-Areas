using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TaggingMinigame : MonoBehaviour
{

    private enum GameState
    {
        tutorial,
        timer,
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
    [SerializeField] private TMP_Text sharkCounterText;
    [SerializeField] private float countdownTime = 3;
    [SerializeField] private float gameTime = 60;
    [SerializeField] private GameObject davitModel;
    [SerializeField] private GameObject davitPivotPoint;
    [SerializeField] private GameObject boat;

    [Header("Narration")]
    [SerializeField] private Subtitles subtitles;
    [SerializeField] private Narration place;
    private bool placed = false;
    [SerializeField] private Narration minigameIntro;
    [SerializeField] private Narration outro1;
    [SerializeField] private Narration outro2;
    [SerializeField] private Narration outro3;


    private Transform m_tagPlacedArea;
    private GameObject m_shark;
    private TaggingShark m_sharkScript;
    private GameObject m_currentTag;
    private float gameTimer;

    private XRGrabInteractable m_tag;
    private Coroutine turning;

    private void OnEnable()
    {
        CreateShark();
    }

    private void Start()
    {
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
        if (turning == null)
        {
            m_tag.selectEntered.RemoveListener(swapActiveLabel);
            m_tag.selectExited.RemoveListener(swapActiveLabel);
            m_currentTag.GetComponent<XRGrabInteractable>().enabled = false;
            m_currentTag.GetComponent<Rigidbody>().isKinematic = true;
            m_currentTag.transform.position = m_tagPlacedArea.position;

            turning = StartCoroutine(RotateShark());
        }
    }

    private void CreateShark()
    {
        m_shark = Instantiate(m_sharkPrefab, m_sharkSpawnPoint.position, m_sharkSpawnPoint.rotation, boat.transform);
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
                if (placed == false)
                {
                    subtitles.enqueueNarration(place);
                    placed = true;
                }
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
        else if (placePrompt.gameObject.activeSelf)
        {
            grabPrompt.gameObject.SetActive(true);
            placePrompt.gameObject.SetActive(false);
        }
        else
        {
            grabPrompt.gameObject.SetActive(false);
            placePrompt.gameObject.SetActive(false);
        }
    }

    private IEnumerator RotateShark()
    {
        m_shark.transform.DOMoveY(21, 0.5f);
        m_currentTag.transform.DOMoveY(21, 0.5f);
        if (curState == GameState.tutorial)
        {
            subtitles.enqueueNarration(minigameIntro);
            grabPrompt.gameObject.SetActive(false);
            placePrompt.gameObject.SetActive(false);
        }
        Debug.Log("right");
        while (davitModel.transform.eulerAngles.y != 90)
        {
            davitModel.transform.RotateAround(davitPivotPoint.transform.position, new Vector3(0, 1, 0), Time.deltaTime * 20);
            m_shark.transform.RotateAround(davitPivotPoint.transform.position, new Vector3(0, 1, 0), Time.deltaTime * 20);
            m_sharkSpawnPoint.transform.RotateAround(davitPivotPoint.transform.position, new Vector3(0, 1, 0), Time.deltaTime * 20);
            m_currentTag.transform.RotateAround(davitPivotPoint.transform.position, new Vector3(0, 1, 0), Time.deltaTime * 20);
            if (davitModel.transform.eulerAngles.y > 90)
            {
                davitModel.transform.eulerAngles = new Vector3(0, 90, 0);
                m_shark.transform.eulerAngles = new Vector3(0, 90, 0);
                m_sharkSpawnPoint.transform.eulerAngles = new Vector3(0, 90, 0);
                break;
            }
            yield return null;
        }
        m_shark.transform.DOMoveY(17, 1f);
        m_currentTag.transform.DOMoveY(17, 1f);
        yield return new WaitForSeconds(1);
        DestroyShark();
        Coroutine gameStarting;
        if (curState == GameState.tutorial)
        {
            Debug.Log("start");
            gameStarting = StartCoroutine(StartGame());
            yield return gameStarting;
        }
        else if (curState == GameState.game)
        {
            Debug.Log("counter");
            sharkCounterText.SetText((++StaticData.sharksTagged).ToString());
            ResetTag();
            CreateShark();
        }
        if (curState != GameState.end) m_shark.transform.DOMoveY(21, 0.5f);
        Debug.Log("left");
        while (davitModel.transform.eulerAngles.y != 0)
        {
            //Debug.Log("Here");
            davitModel.transform.RotateAround(davitPivotPoint.transform.position, new Vector3(0, 1, 0), -(Time.deltaTime * 20));
            if (curState != GameState.end) m_shark.transform.RotateAround(davitPivotPoint.transform.position, new Vector3(0, 1, 0), -(Time.deltaTime * 20));
            m_sharkSpawnPoint.transform.RotateAround(davitPivotPoint.transform.position, new Vector3(0, 1, 0), -Time.deltaTime * 20);
            if (davitModel.transform.eulerAngles.y > 359)
            {
                Debug.Log("at the end");
                davitModel.transform.eulerAngles = new Vector3(0, 0, 0);
                if (curState != GameState.end) m_shark.transform.eulerAngles = new Vector3(0, 0, 0);
                m_sharkSpawnPoint.transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            }
            yield return null;
        }
        if (curState != GameState.end) m_shark.transform.DOMoveY(19.956f, 0.5f);
        turning = null;

    }

    private IEnumerator StartGame()
    {
        curState = GameState.timer;
        countDownText.SetText(startText);
        yield return new WaitForSeconds(3);
        countDownText.SetText("3");
        yield return new WaitForSeconds(1);
        countDownText.SetText("2");
        yield return new WaitForSeconds(1);
        countDownText.SetText("1");
        yield return new WaitForSeconds(1);
        countDownText.SetText("");
        curState = GameState.game;
        ResetTag();
        DestroyShark();
        CreateShark();
        StartCoroutine(GameTimer());
    }

    private IEnumerator GameTimer()
    {
        timerText.rectTransform.DOMoveX(200, 1);
        StaticData.sharksTagged = 0;
        sharkCounterText.rectTransform.DOMoveX(200, 1);
        sharkCounterText.SetText("0");
        gameTimer = 60;
        while (gameTimer >= 0)
        {
            timerText.text = gameTimer.ToString("00");
            gameTimer--;
            yield return new WaitForSeconds(1);
        }
        timerText.rectTransform.DOMoveX(-200, 1);
        m_currentTag.GetComponent<XRGrabInteractable>().enabled = false;
        curState = GameState.end;
        StartCoroutine(EndOfGame());
    }

    private IEnumerator EndOfGame()
    {
        subtitles.enqueueNarration(outro1);
        subtitles.enqueueNarration(outro2);
        subtitles.enqueueNarration(outro3);
        yield return null;
    }

}
