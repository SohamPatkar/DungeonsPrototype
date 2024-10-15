using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenuCanvas;
    public Text _healthtext, _gemText, _gameMessages;
    private PlayerMovement _playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        _playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        _gemText.text = "Gems:" + " " + _playerMovement.Gems;
        _healthtext.text = "Health:" + " " + _playerMovement.Health;
        GemMessageToHeal();
        Pause();
    }
    void GemMessageToHeal()
    {
        if (_playerMovement.Gems > 0 && _playerMovement.Health < 80)
        {
            _gameMessages.text = "Press 'E' to regenerate";
        }
    }
    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            _pauseMenuCanvas.SetActive(true);
        }
    }
    public void PauseMenuBack()
    {
        Time.timeScale = 1;
        _pauseMenuCanvas.SetActive(false);
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        _pauseMenuCanvas.SetActive(true);
    }

    public void PauseMenuExit()
    {
        Application.Quit();
    }
}
