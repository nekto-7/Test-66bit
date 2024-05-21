using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
// дублировать плохо но хочу сделать быстрее
public class StartPoints : MonoBehaviour
{
    public AudioClip source;
    public AudioSource audioPlayer;
    private Progress Prog = Progress.Inst;
    [SerializeField] private float time = 60f;
    [SerializeField] private TextMeshProUGUI textTimer;
    [SerializeField] private List<Transform> transforms = new List<Transform>();
    [SerializeField] private GameObject Foods;
    [SerializeField] private GameObject ScoreObject;
    [SerializeField] private bool flag;
    [SerializeField] private GameObject Scene;
    [SerializeField] private GameObject CarCollider;
    [SerializeField] private GameObject textTutorial;
    [SerializeField] private GameObject Trener;

        private void Awake()
    {
        Prog.FaindText();
        Prog.UpdateText();
    }
    private void Budy(int songIndex, int score)
    {
        Prog.coins += score;
        Prog.PlaySongs(songIndex, audioPlayer, source);
        if (Prog.record < Prog.coins)
        {
            Prog.record = Prog.coins;
            Prog.Save();
        }
        Prog.UpdateText();
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "food" )
        {
            Budy(1, 0);
            Destroy(col.gameObject);
            SpawnPurpose();
            StartCoroutine(StartTimer());
            flag = false;
            
        }

        if (col.gameObject.tag == "point" )
        {
            Budy(0, 0);
            Destroy(col.gameObject);
            Destroy(this.gameObject);
            Scene.SetActive(true);
            CarCollider.SetActive(true);
            Trener.SetActive(false);
            textTutorial.SetActive(false);
        }
    }

    private void SpawnPurpose()
    {
        int index = Random.Range(0, transforms.Count);
        Instantiate(ScoreObject, transforms[index].position, Quaternion.identity);
    }

    private IEnumerator StartTimer()
    {
        float currentTime = time;

        while (currentTime > 0)
        {
            if (textTimer != null)
            {
                textTimer.text = "timer: " + currentTime.ToString("F2");
            }
            yield return new WaitForSeconds(1f);
            currentTime--;

            if (flag)
            {
                yield break;
            }
            
        }
        SceneManager.LoadScene(1);
    }
}
