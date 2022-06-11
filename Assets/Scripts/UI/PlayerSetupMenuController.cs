using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//class to initialize player controller within the character selection scene
public class PlayerSetupMenuController : MonoBehaviour
{
    private int playerIndex;

    [SerializeField] private TextMeshProUGUI playerNumberText; //Player [1,2,3]
    [SerializeField] private TextMeshProUGUI characterName; // [Samus, Foxxy, ...]
    [SerializeField] private Image characterImg;
    [SerializeField] private Button readyButton;

    // this is not used yet
    [SerializeField] private GameObject readyPanel;
    [SerializeField] private GameObject menuPanel;

    private GameManager gameManager;

    int selectedCharacterIndex = 0;

    //set player Index, obtain GameManager instance and update screen with player index
    public void SetPlayerIndex(int pi)
    {
        playerIndex = pi;
        playerNumberText.SetText("Player " + (pi + 1).ToString());
        selectedCharacterIndex = PlayerPrefs.GetInt("Jugador" + playerIndex.ToString() + "Index");
        gameManager = GameManager.Instance;
        ActualizarPantalla();
    }

    //change image to next character
    public void SiguientePersonaje()
    {
        //cleaner if statement: if idx==0  count-1  else idx--
        //this is the ternary operator, useful for assingment like this one
        // x = (condition) ? value_if_true : value_if_false; 
        selectedCharacterIndex = (selectedCharacterIndex == gameManager.personajes.Count - 1) ? 0 : selectedCharacterIndex + 1;
        ActualizarPantalla();
    }
    //change image to prev character
    public void AnteriorPersonaje()
    {
        //cleaner if statement: if idx==0  count-1  else idx--
        selectedCharacterIndex = (selectedCharacterIndex == 0) ? gameManager.personajes.Count - 1 : selectedCharacterIndex - 1;
        ActualizarPantalla();
    }
    //update the canvas with the current character selected
    private void ActualizarPantalla()
    {
        PlayerPrefs.SetInt("Jugador" +playerIndex.ToString() +"Index", selectedCharacterIndex);
        characterImg.sprite = gameManager.personajes[selectedCharacterIndex].imagen;
        characterName.text = gameManager.personajes[selectedCharacterIndex].nombre;
    }
    //confirm character selection
    public void ChooseCharacter()
    {
        gameManager.setPlayerCharacter(playerIndex, selectedCharacterIndex);
        gameManager.readyPlayer(playerIndex);
        readyButton.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
 
    }

}
