using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Points : MonoBehaviour
{    
    public AudioClip source;
    public AudioSource audioPlayer;
    private Progress Prog = Progress.Inst;
    [SerializeField] private float time = 60f;
    [SerializeField] private TextMeshProUGUI textTimer;
    [SerializeField] private List<Transform> foodTransforms = new List<Transform>();
    [SerializeField] private List<Transform> pointTransforms = new List<Transform>();
    [SerializeField] private GameObject Foods;
    [SerializeField] private GameObject ScoreObject;
    private bool flagTimer;
    private bool flagCollider;
    private void Awake()
    {
        Prog.FaindText();
        Prog.UpdateText();
    }
    private void Start()
    {
        SpawnFood();
    }
    // метод отвечающий за начисление очков и воспроизведение звука
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

        if (col.gameObject.tag == "food" && !flagCollider)
        {
            Debug.Log("Spawn Point");// была проблема что детектил объект дважды
            Destroy(col.gameObject);
            SpawnPurpose();
            StartCoroutine(StartTimer());
            flagTimer = false;
            flagCollider = true;
            Budy(1, 0);
        }

        if (col.gameObject.tag == "point" && flagCollider)
        {
            Debug.Log("Spawn Food");
            Destroy(col.gameObject);
            StopCoroutine(StartTimer());
            SpawnFood();
            flagTimer = true;
            flagCollider = false;
            Budy(0, 10);
        }
        if (col.gameObject.tag == "coins" )
        {
            Budy(0, 1);
            Destroy(col.gameObject);
        }
    }

    private void SpawnPurpose()
    {
        int index = Random.Range(0, pointTransforms.Count);
        Instantiate(ScoreObject, pointTransforms[index].position, Quaternion.identity);
    }

    private void SpawnFood()
    {
        int index = Random.Range(0, foodTransforms.Count);
        Instantiate(Foods, foodTransforms[index].position, Quaternion.identity);
    }
    // куратина таймера за которое мы должны закончить доставку
    private IEnumerator StartTimer()
    {
        float currentTime = time;

        while (currentTime > 0)
        {
            if (textTimer != null)
            {
                textTimer.text = "timer: " + currentTime.ToString();
            }
            yield return new WaitForSeconds(1f);
            currentTime--;

            if (flagTimer)
            {
                yield break;
            }
        }
        SceneManager.LoadScene(1);
    }
}
