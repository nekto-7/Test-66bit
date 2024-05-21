using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// спавнит монетки
public class Spawner : MonoBehaviour
{  
    [SerializeField] private GameObject coin;
    [SerializeField] private List<Transform> coinsTransforms = new List<Transform>();
    private void Start()
    {
        StartCoroutine(SpawnCoins());
    }
     private IEnumerator SpawnCoins()
    {
        while (true)
        {
            int index = Random.Range(0, coinsTransforms.Count);
            Instantiate(coin, coinsTransforms[index].position, Quaternion.identity);
            yield return new WaitForSeconds(10f);
            
        }
    }
}