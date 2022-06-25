using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//class to manage players within gameplay scenes
public class PlayerManager : MonoBehaviour
{

    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject playerPrefab;

    //when the instance is created spawn player objects from the list of players in the GameManager
    private void Awake()
    {
        //obtain the GameManager
        var playerConfigs = GameManager.Instance.GetPlayerConfigurations().ToArray();

        for (int i=0; i< playerConfigs.Length; i++)
        {
            int idx = playerConfigs[i].playerIndex;
            var player = Instantiate(playerPrefab, spawnPoints[idx].position, spawnPoints[idx].rotation, gameObject.transform);
            player.GetComponent<PlayerMovement>().Initialize(playerConfigs[i]);
        }
    }

    private void Start()
    {
        
    }
}
