using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Selectable initialSelected; // UI element đầu tiên được chọn

    public GameObject generalSetting;
    public GameObject controlsSetting;
    public GameObject gamepadPanel;
    public GameObject keyboardPanel;
    // Start is called before the first frame update

    public void Choi()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    public void Caidat()
    {
    }
    public void Thoat()
    {
        Application.Quit();

    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Menu()
    {
        SceneManager.LoadScene(3);
    }
    public void ChoilaiBoss1()
    {
        SceneManager.LoadScene(11);
    }
    public void ChoilaiBoss2()
    {
        SceneManager.LoadScene(15);
    }
    public void GeneralSetting()
    {
        generalSetting.SetActive(true);
        controlsSetting.SetActive(false);
    }

    public void ControlsSetting()
    {
        generalSetting.SetActive(false);
        controlsSetting.SetActive(true);
    }

    public void GamepadPanel()
    {
        keyboardPanel.SetActive(false);
        gamepadPanel.SetActive(true);
    }
    public void KeyboardPanel()
    {
        keyboardPanel.SetActive(true);
        gamepadPanel.SetActive(false);
    }

}
