using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    public static SceneTransitioner Instance;

    [Header("Animator del Fade")]
    public Animator black;

    private bool isTransitioning = false;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        if (!isTransitioning)
            StartCoroutine(Transition(sceneName));
    }

    public void LoadScene(int index)
    {
        if (!isTransitioning)
            StartCoroutine(Transition(index));
    }

    public void ReloadScene()
    {
        if (!isTransitioning)
            StartCoroutine(Transition(SceneManager.GetActiveScene().buildIndex));
    }

    private IEnumerator Transition(string sceneName)
    {
        isTransitioning = true;

        black.SetTrigger("Out");
        yield return new WaitForSeconds(1.5f); // duración del fade

        yield return SceneManager.LoadSceneAsync(sceneName);

        black.SetTrigger("In");
        yield return new WaitForSeconds(1.5f);

        isTransitioning = false;
    }

    private IEnumerator Transition(int index)
    {
        isTransitioning = true;

        black.SetTrigger("Out");
        yield return new WaitForSeconds(1.5f);

        yield return SceneManager.LoadSceneAsync(index);

        black.SetTrigger("In");
        yield return new WaitForSeconds(1.5f);

        isTransitioning = false;
    }
}
