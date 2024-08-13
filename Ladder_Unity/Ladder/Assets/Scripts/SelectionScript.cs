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

    void Start()
    {
        // Populate dropdowns with lad names
        List<string> ladNames = new List<string>();
        foreach (GameObject ladPrefab in ladPrefabs)
        {
            ladNames.Add(ladPrefab.GetComponent<Lad>().ladName);
        }
        playerLadDropdown.AddOptions(ladNames);
        enemyLadDropdown.AddOptions(ladNames);

        startBattleButton.onClick.AddListener(OnStartBattle);

        selectionCanvas.SetActive(true);
        battleCanvas.SetActive(false);
    }

    void OnStartBattle()
    {
        // Instantiate selected lads at spawn points
        GameObject playerLad = Instantiate(ladPrefabs[playerLadDropdown.value], playerSpawnPoint.position, Quaternion.identity);
        GameObject enemyLad = Instantiate(ladPrefabs[enemyLadDropdown.value], enemySpawnPoint.position, Quaternion.identity);

        // Pass instantiated lads to GameManager
        FindObjectOfType<GameManager>().InitialiseBattle(playerLad.GetComponent<Lad>(), enemyLad.GetComponent<Lad>());

        selectionCanvas.SetActive(false);
        battleCanvas.SetActive(true);
    }

    public void ResetSelection()
    {
        Debug.Log("Activating selection screen");
        selectionCanvas.SetActive(true);

        Debug.Log("Deactivating battle screen");
        battleCanvas.SetActive(false);
    }
}
