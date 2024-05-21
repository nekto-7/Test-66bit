using UnityEngine;
using TMPro;
public class Progress : MonoBehaviour 
{
    public int coins = 0;
    public int record = 0;
    public bool SongWorked = true;
    public AudioSource[] audioMassive;
    public TextMeshProUGUI RecordText, ScoreText;
    private GameObject[] G1, G2;
    public static Progress Inst;
    private void Awake()
    {
        FaindText();
        if (Inst != null && Inst != this)
        {
            Destroy(gameObject);
            return;
        }
        Inst = this;
        DontDestroyOnLoad(gameObject);
        UotputSave();
        Save();
    }

    public void FaindText()
    {
        G1 = GameObject.FindGameObjectsWithTag("Records");
        if (G1.Length > 0 && G1[0] != null)
        {
            RecordText = G1[0].GetComponent<TextMeshProUGUI>();
        }
        G2 = GameObject.FindGameObjectsWithTag("Score");
        if (G2.Length > 0 && G2[0] != null)
        {
            ScoreText= G2[0].GetComponent<TextMeshProUGUI>();
        }

        UpdateText();
    }
    public void UpdateText()
    {
        UotputSave();
        if (ScoreText != null)
        {
            ScoreText.text = "Score: " + coins.ToString();
        }
        if (RecordText != null)
        {
            RecordText.text = "Record: " + PlayerPrefs.GetInt("Record").ToString();
        }
    }
    public void Save()
    {
        PlayerPrefs.SetInt("Record", record);
    }
    public void UotputSave()
    {
        if (PlayerPrefs.HasKey("Record"))
        {
            record = PlayerPrefs.GetInt("Record");
        }
    }
    public void PlaySongs(int index, AudioSource audioPlayer, AudioClip source)
    {
        audioPlayer = audioMassive[index];
        source = audioMassive[index].clip;
        audioPlayer.PlayOneShot(source);
    }
}
