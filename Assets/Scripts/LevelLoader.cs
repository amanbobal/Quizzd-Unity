using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    
    public Animator animator;

    public GameObject QuitCanvas;

    public float Time = 1f;

    private void Awake()
    {
        Application.targetFrameRate = 30;

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            animator.enabled = false;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitCanvas.SetActive(true);
        }
    }
    public void LoadNextScene()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public IEnumerator Loadanim()
    {
        animator.SetBool("Start", true);

        yield return new WaitForSeconds(Time);

        animator.SetBool("Start", false);
    }
    IEnumerator LoadScene(int sceneIndex)
    {
        animator.enabled = true;

        animator.SetBool("Start", true);

        yield return new WaitForSeconds(Time);

        SceneManager.LoadSceneAsync(sceneIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
