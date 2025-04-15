using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static System.TimeZoneInfo;
using UnityEngine.SceneManagement;

public class XRMenuButtonManager : MonoBehaviour
{
    static private XRMenuButtonManager instance;
    private XRMenuButton menuControls;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            menuControls = new XRMenuButton();
            menuControls.Enable();

            menuControls.XRActions.Menu.performed += reset;
        }
        else
        {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad(gameObject);

        
    }

    private void OnApplicationQuit()
    {
        menuControls.XRActions.Menu.performed -= reset;

        menuControls.Disable();
    }

    void reset(InputAction.CallbackContext context)
    {
        StartCoroutine(loadScene());
    }

    IEnumerator loadScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);
    }

}