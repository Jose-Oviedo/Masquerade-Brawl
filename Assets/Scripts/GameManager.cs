using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

//class to manage events, from character selection to instancing players in the map
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Personajes> personajes;
    public MenuSeleccionPersonaje UICanvas;

    private List<PlayerConfiguration> playerConfigs;
    private int MaxPlayers = 4;

    private bool allPlayersReady = false;
    private bool sceneChanged = false;
    private bool leavingMenu = true;
    private int playersDead = 0;
    //on creation make it singleton
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

        //if it does not exist already add it to the list
        if (!playerConfigs.Any(p => p.playerIndex == pi.playerIndex))
        {
            pi.transform.SetParent(transform);
            pi.SwitchCurrentActionMap("UI");
            playerConfigs.Add(new PlayerConfiguration(pi));
        }

        //if all players were ready but another joined (max4) unset timer to change scene
        if (allPlayersReady && !sceneChanged)
        {
            UICanvas.UnSetTimer();
            allPlayersReady = false;
        }
    }

    //set character selection for player[playerIndex]
    public void setPlayerCharacter(int playerIndex, int characterindex)
    {
        playerConfigs[playerIndex].characterIndex = characterindex;
    }

    //set player as ready, if all ready start timer to begin game
    public void readyPlayer(int index)
    {  
        playerConfigs[index].isReady = true;
        if (playerConfigs.All(p => p.isReady == true))
        {
            allPlayersReady = true;
            UICanvas.SetTimer();
            leavingMenu = false;
        }
    }
    public void playerDied(int index)
    {
        Debug.Log(index + "died");
        //it was already dead
        if (playerConfigs[index].isReady)
        {
            Debug.Log(index + "was already dead");
            return;
        }

        playersDead++;
        if (playersDead == playerConfigs.Count-1)
        {
            SceneChanged();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    
    //change PlayerInput action map for all current players
    private void SetActionMaps()
    {
        foreach(var config in playerConfigs)
        {
            config.input.SwitchCurrentActionMap("Player");
        }
    }

    private void ClearPlayerReady()
    {
        foreach (var config in playerConfigs)
        {
            config.isReady=false;
        }
    }
    //when scene changes from character selection to gameplay change action map (to control the player)
    public void SceneChanged()
    {
        if (leavingMenu)
        {
            GetComponent<PlayerInputManager>().DisableJoining();
            SetActionMaps();
            sceneChanged = true;
        }
        ClearPlayerReady();
        playersDead = 0;
    }
}

//class that holds the config for a player
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