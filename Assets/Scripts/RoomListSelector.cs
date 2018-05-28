using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomListSelector : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject standartGamePanelPrafab;
    [SerializeField] private GameObject goldGamePanelPrafab;
    [SerializeField] private GameObject joinRoomPanelPrefab;
    Dropdown gameSelector;
    string currentChosenGame;
    List<string> gameNames = null;
    GamesTable.Game activeGame = new GamesTable.Game();
    readonly XmlSerializer serializer = new XmlSerializer(typeof(GamesTable.Game));
    public GameObject canvas;
    GameObject gamePanell;
    List<GameObject> PanellList=new List<GameObject>();
    #endregion

    void Start ()
    {
        CreateGameList();
	}

    private void CreateGameList()
    {
        gameSelector = canvas.GetComponentInChildren<Dropdown>();
        gameSelector.ClearOptions();
        //Debug.Log(gameSelector);
        gameNames = new List<string>() {"StarCraft", "WarCraft", "Tanki", "AlphaProtocol","Finansist","Reunion" };
        gameNames.Sort();
        gameNames.Insert(0, "Chose Game!");
        gameSelector.AddOptions(gameNames);
    }
    public void DropdownIndexCghanged(int index)
    {
        
        currentChosenGame = gameNames[index];
        if (PanellList.Count > 0)
        {
            for (int i = 0; i < PanellList.Count; i++)
            {
                Destroy(PanellList[i]);
            }
        }
        PanellList.Clear();
        activeGame.GameName = currentChosenGame;
        DeserializeActiveGame();
        CreateStandartGame();
        CreateGoldGame();
    }
    private void DeserializeActiveGame()
    {
        FileStream stream = new FileStream(activeGame.GameName + ".xml", FileMode.Open, FileAccess.Read, FileShare.Read);
        activeGame = serializer.Deserialize(stream) as GamesTable.Game;
        stream.Close();
        Debug.Log(activeGame.GameName + " is current chosen");
        
    }
    private void CreateStandartGame()
    {
        var standartGame = Instantiate<GameObject>(standartGamePanelPrafab);
        PanellList.Add(standartGame);
        Text _coins = standartGame.GetComponentInChildren<Text>();
        var _progressBar = standartGame.transform.Find("ProgressBar");
        var _progressText = _progressBar.transform.Find("ProgressText");
        Text _progress = _progressText.GetComponentInChildren<Text>();
        Slider currentGold = standartGame.GetComponentInChildren<Slider>();
        _coins.text = activeGame.CurrentGoldCoints.ToString();
        currentGold.minValue = 0;
        currentGold.maxValue = activeGame.StandartGameGoldCosts;
        currentGold.value = activeGame.CurrentGoldCoints;
        _progress.text = _coins.text + "/" + currentGold.maxValue.ToString();
        standartGame.transform.SetParent(canvas.transform,false);
        CreateStandartRoom();
    }
    private void CreateStandartRoom()
    {
        for (int i = 0; i <= activeGame.StandatrGameRooms-1; i++)
        {
            var standartroom = Instantiate(joinRoomPanelPrefab);
            PanellList.Add(standartroom);
            var playerCountBar = standartroom.transform.Find("PlayerCountText");
            Text playerCountText = playerCountBar.GetComponentInChildren<Text>();
            var roomNameBar= standartroom.transform.Find("RoomNameText");
            Text roomNameText = roomNameBar.GetComponentInChildren<Text>();
            Button joinButton = standartroom.GetComponentInChildren<Button>();
            playerCountText.text = activeGame.StandartRoomPlayers[i].ToString();
            roomNameText.text = activeGame.StandartRoomsNames[i].ToString();
            if(activeGame.StandartRoomPlayers[i]==activeGame.MaxRoomPlayers)
            {
                var buttonActive = joinButton.GetComponent<Button>();
                buttonActive.interactable = false;
                var joinButtonText = joinButton.GetComponentInChildren<Text>();
                joinButtonText.text = "Full";
            }
            joinButton.onClick.AddListener((UnityEngine.Events.UnityAction)this.JoinToGame);
            standartroom.transform.SetParent(canvas.transform, false);
        }
    }
    private void CreateGoldGame()
    {
        var goldGame = Instantiate<GameObject>(goldGamePanelPrafab);
        PanellList.Add(goldGame);
        Text _coins = goldGame.GetComponentInChildren<Text>();
        var _progressBar = goldGame.transform.Find("ProgressBar");
        var _progressText = _progressBar.transform.Find("ProgressText");
        Text _progress = _progressText.GetComponentInChildren<Text>();
        Slider currentGold = goldGame.GetComponentInChildren<Slider>();
        _coins.text = activeGame.CurrentGoldCoints.ToString();
        currentGold.minValue = 0;
        currentGold.maxValue = activeGame.GoldGameGoldCosts;
        currentGold.value = activeGame.CurrentGoldCoints;
        _progress.text = _coins.text + "/" + currentGold.maxValue.ToString();
        goldGame.transform.SetParent(canvas.transform, false);
        CreateGoldRoom();
    }
    private void CreateGoldRoom()
    {
        for (int i = 0; i <= activeGame.GoldGameRooms - 1; i++)
        {
            var goldroom = Instantiate(joinRoomPanelPrefab);
            PanellList.Add(goldroom);
            var playerCountBar = goldroom.transform.Find("PlayerCountText");
            Text playerCountText = playerCountBar.GetComponentInChildren<Text>();
            var roomNameBar = goldroom.transform.Find("RoomNameText");
            Text roomNameText = roomNameBar.GetComponentInChildren<Text>();
            Button joinButton = goldroom.GetComponentInChildren<Button>();
            playerCountText.text = activeGame.GoldRoomPlayers[i].ToString();
            roomNameText.text = activeGame.GoldRoomsNames[i].ToString();
            if (activeGame.GoldRoomPlayers[i] == activeGame.MaxRoomPlayers)
            {
                var buttonActive = joinButton.GetComponent<Button>();
                buttonActive.interactable = false;
                var joinButtonText = joinButton.GetComponentInChildren<Text>();
                joinButtonText.text = "Full";
            }
            joinButton.onClick.AddListener((UnityEngine.Events.UnityAction)this.JoinToGame);
            goldroom.transform.SetParent(canvas.transform, false);
        }
    }
    public void JoinToGame()
    {
        Debug.Log("JOIN TO GAME");
        SceneManager.LoadScene("JoinRoom", LoadSceneMode.Single);
    }
}
