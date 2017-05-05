
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public void Start()
    {
        GetComponent<Animation>().Play("Splash");
        StartCoroutine(AnimationDone());
    }

    public IEnumerator AnimationDone()
    {
        yield return new WaitForSeconds(7.0f);
        SceneManager.LoadScene("MainMenu");
    }
}
