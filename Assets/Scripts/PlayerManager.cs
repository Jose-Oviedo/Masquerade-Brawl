using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject playerPrefab;

    private void Awake()
    {
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
        //int indexJugador = PlayerPrefs.GetInt("JugadorIndex");
        //Instantiate(GameManager.Instance.personajes[indexJugador].personajeJugable, transform.position, Quaternion.identity);
    }
}
