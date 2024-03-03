using UnityEngine;
using UnityEngine.SceneManagement;

public class MainmenuUIManager : MonoBehaviour
{
    [SerializeField] private RectTransform MainPanel;
    [SerializeField] private RectTransform SettingsPanel;
    //[SerializeField] private RectTransform ControlsPanel;
    //[SerializeField] private RectTransform AudioPanel;

    private ServiceLocator ServiceLocator => ServiceLocator.Instance;

    private AudioManager AudioManager;

    private void Start()
    {
        AudioManager = ServiceLocator.GetService<AudioManager>();
    }

    public void StartGameBttn()
    {
        AudioManager.PlaySFX("Button");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        Time.timeScale = 1;

        AudioManager.PlayMusic("Level");
        asyncLoad.completed += LoadSceneAndSetupCamera;
    }

    private void LoadSceneAndSetupCamera(AsyncOperation asyncLoad)
    {
        ServiceLocator.GetService<CameraManager>().StartCoroutineForCamera();

        Time.timeScale = 1;
    }

    public void SettingsBttn()
    {
        //MainPanel.gameObject.SetActive(false);
        SettingsPanel.gameObject.SetActive(true);

        AudioManager.PlaySFX("Button");
    }

    public void ExitBttn()
    {
        UnityEditor.EditorApplication.isPlaying = false;

        AudioManager.PlaySFX("Button");
        //UnityEditor.EditorApplication.isPlaying = false;
        //in built ver
        Application.Quit();
    }

    //////////////

    public void ReturnToMainBttn()
    {
        //AudioManager.instance.PlaySFX("ButtonClick");
        MainPanel.gameObject.SetActive(true);
        SettingsPanel.gameObject.SetActive(false);

        AudioManager.PlaySFX("Button");
        //AudioPanel.gameObject.SetActive(false);
        //ControlsPanel.gameObject.SetActive(false);
    }
}