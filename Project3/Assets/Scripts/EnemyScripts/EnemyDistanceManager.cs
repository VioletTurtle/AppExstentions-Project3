using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistanceManager : MonoBehaviour
{
    public FriendAI fAI;
    GameObject player;
    GameObject[] enemys;
    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        fAI.SetCurrentEnemy(enemys[0]);
        Debug.Log("Enemys found: "+(enemys.Length-1));
    }

    // Update is called once per frame
    private void Update()
    {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemys.Length == 1)
        {
            EndGame();
        }
        float distance = Mathf.Infinity;
        GameObject close = null;
        foreach (GameObject current in enemys)
        {
            float distanceEnemy = Vector3.Distance(player.transform.position, current.transform.position);
            if(distanceEnemy < distance)
            {
                distance = distanceEnemy;
                close = current;
            }
        }
        fAI.SetCurrentEnemy(close);
    }

    private void EndGame()
    {

    }
}
