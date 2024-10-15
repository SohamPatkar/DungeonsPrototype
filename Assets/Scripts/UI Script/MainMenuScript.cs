using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _instructions, _MainMenuCanvas;
    private void Start()
    {
        _instructions.SetActive(false);
    }

    public void OnStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Instructions()
    {
        _instructions.SetActive(true);
        LeanTween.alpha(_instructions.GetComponent<RectTransform>(), 1, 0.2f);
        LeanTween.moveX(_instructions.GetComponent<RectTransform>(), 0, 0.5f).setEase(LeanTweenType.linear);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Back()
    {
        LeanTween.alpha(_instructions.GetComponent<RectTransform>(), 0, 0.2f);
        LeanTween.moveX(_instructions.GetComponent<RectTransform>(), -2000, 0.5f).setEase(LeanTweenType.linear);
    }
}
