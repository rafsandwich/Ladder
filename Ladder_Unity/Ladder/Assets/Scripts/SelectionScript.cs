using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SelectionScript : MonoBehaviour
{
    public TMP_Dropdown playerLadDropdown;
    public TMP_Dropdown enemyLadDropdown;
    public Button startBattleButton;

    public GameObject[] ladPrefabs;
    public Transform playerSpawnPoint;
    public Transform enemySpawnPoint;

    public GameObject selectionCanvas;
    public GameObject battleCanvas;

    public Button[] move1Buttons;
    public Button[] move2Buttons;

    private GameObject playerLad;
    private GameObject enemyLad;
    


    void Start()
    {
        battleCanvas.SetActive(false);

        // Populate dropdowns with lad names
        List<string> ladNames = new List<string>();
        foreach (GameObject ladPrefab in ladPrefabs)
        {
            ladNames.Add(ladPrefab.GetComponent<Lad>().ladName);
        }
        playerLadDropdown.AddOptions(ladNames);
        enemyLadDropdown.AddOptions(ladNames);

        startBattleButton.onClick.AddListener(OnStartBattle);

        DisableAllMoveButtons();

        selectionCanvas.SetActive(true);
    }

    void OnStartBattle()
    {
        // Instantiate selected lads at spawn points
        playerLad = Instantiate(ladPrefabs[playerLadDropdown.value], playerSpawnPoint.position, Quaternion.identity);
        enemyLad = Instantiate(ladPrefabs[enemyLadDropdown.value], enemySpawnPoint.position, Quaternion.identity);

        EnableUserMoveButtons(playerLadDropdown.value);

        // Pass instantiated lads to GameManager
        FindObjectOfType<GameManager>().InitialiseBattle(playerLad.GetComponent<Lad>(), enemyLad.GetComponent<Lad>(), playerLadDropdown.value);

        selectionCanvas.SetActive(false);
        battleCanvas.SetActive(true);
    }

    void EnableUserMoveButtons(int ladIndex)
    {
        DisableAllMoveButtons();

        move1Buttons[ladIndex].gameObject.SetActive(true);
        move2Buttons[ladIndex].gameObject.SetActive(true);

        // Destroy previous move buttons if any exist
        //if (userMoveButton1 != null) Destroy(userMoveButton1);
        //if (userMoveButton2 != null) Destroy(userMoveButton2);

        // Instantiate new move buttons based on the selected Lad
        //userMoveButton1 = Instantiate(moveButtonPrefabs[ladIndex * 2], moveButtonParentPos);
        //userMoveButton2 = Instantiate(moveButtonPrefabs[ladIndex * 2 + 1], moveButtonParentPos);
    }

    public void ResetSelection()
    {
        Debug.Log("Activating selection screen");
        selectionCanvas.SetActive(true);

        Debug.Log("Deactivating battle screen");
        battleCanvas.SetActive(false);

        //if (playerLad != null) Destroy(playerLad);
        //if (enemyLad != null) Destroy(enemyLad);

        //DisableAllMoveButtons();
    }

    void DisableAllMoveButtons()
    {
        foreach (var button in move1Buttons)
        {
            button.gameObject.SetActive(false);
        }

        foreach (var button in move2Buttons)
        {
            button.gameObject.SetActive(false);
        }

    }
}
