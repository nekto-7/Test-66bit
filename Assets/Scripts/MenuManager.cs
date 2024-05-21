using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject panelOption;
    [SerializeField] private Slider volumeSlider; 
    private bool statusOption;
    void Start()
    {
        // Загружаем сохраненное значение громкости 
        // Иначе используем значение по умолчанию
        float savedVolume = PlayerPrefs.GetFloat("Songs", 0.5f);

        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(SetSong);
        }
        SetVolume(savedVolume);
    }
    private void Update()
    {
        // открывем настройки esc
        if(Input.GetKey(KeyCode.Escape) && statusOption)
        {
            Close_Options();
            statusOption = false;
        }
    }
    // для кнопок
    public void Start_Game()
    {
        SceneManager.LoadScene(1);

    }
    public void Exit_Game()
    {
        Application.Quit();
    }
    public void Open_Options()
    {
        panelOption.SetActive(true);
        statusOption = true;
    }
    public void Close_Options()
    {
        panelOption.SetActive(false);
        statusOption = false;
    }
    // для настройки звука
    private void SetSong(float value)
    {
        SetVolume(value);
        PlayerPrefs.SetFloat("Songs", value);
    }
    private void SetVolume(float value)
    {
        foreach (var audioSource in Progress.Inst.audioMassive)
        {
            if (audioSource != null)
            {
                audioSource.volume = value;
            }
        }
    }
}
