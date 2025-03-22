using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainHub : MonoBehaviour
{
    public float loadSceneWaitTime = 5f;
    
    bool changingScene = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeScene(int sceneIndex)
    {
        if (!changingScene)
        {
            changingScene = true;
            StartCoroutine(loadScene(sceneIndex));
        }
    }

    IEnumerator loadScene(int index)
    {
        yield return new WaitForSeconds(loadSceneWaitTime);
        SceneManager.LoadScene(index);
    }


}
