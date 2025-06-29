using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameObject playButton;

    void Start()
    {
        gameManager.enabled = false;
        playerController.enabled = false;
        playerMovement.enabled = false;
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        gameManager.enabled = true;
        playerController.enabled = true;
        playerMovement.enabled = true;
        playButton.SetActive(false);
    }

}
