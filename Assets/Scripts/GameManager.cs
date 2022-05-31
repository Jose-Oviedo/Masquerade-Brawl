using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Personajes> personajes;
    public MenuSeleccionPersonaje UICanvas;

    private List<PlayerConfiguration> playerConfigs;
    private int MaxPlayers = 4;

    private bool allPlayersReady = false;
    private bool sceneChanged = false;

    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            GameManager.Instance = this;
            DontDestroyOnLoad(this.gameObject);
            playerConfigs = new List<PlayerConfiguration>();
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public List<PlayerConfiguration> GetPlayerConfigurations()
    {
        return playerConfigs;
    }

    public void HandlePlayerJoined(PlayerInput pi)
    {
        if (playerConfigs.Count == MaxPlayers)
        {
            return;
        }
        
        if (!playerConfigs.Any(p => p.playerIndex == pi.playerIndex))
        {
            pi.transform.SetParent(transform);
            pi.SwitchCurrentActionMap("UI");
            playerConfigs.Add(new PlayerConfiguration(pi));
        }
        if (allPlayersReady && !sceneChanged)
        {
            UICanvas.UnSetTimer();
        }
        allPlayersReady = false;
    }

    public void setPlayerCharacter(int playerIndex, int characterindex)
    {
        playerConfigs[playerIndex].characterIndex = characterindex;
    }

    public void readyPlayer(int index)
    {  
        playerConfigs[index].isReady = true;
        if (playerConfigs.All(p => p.isReady == true))
        {
            allPlayersReady = true;
            //TODOOOOOOOO
            UICanvas.SetTimer();
        }
    }
    
    private void SetActionMaps()
    {
        foreach(var config in playerConfigs)
        {
            config.input.SwitchCurrentActionMap("Player");
        }
    }

    public void SceneChanged()
    {
        sceneChanged = true;
        SetActionMaps();
    }
}

public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        playerIndex = pi.playerIndex;
        input = pi;
    }
    public PlayerInput input { get; set; }
    public int playerIndex { get; set; }
    public bool isReady { get; set; }
    public int characterIndex { get; set; }

}