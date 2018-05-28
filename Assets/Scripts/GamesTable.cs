using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class GamesTable : MonoBehaviour
{
    [XmlRoot("Game")]
    public class Game
    {
        [XmlAttribute]
        private string gameName;
        private int standatrGameRooms;
        private int goldGameRooms;
        private int currentGoldCoints;
        private int standartGameGoldCosts;
        private int goldGameGoldCosts;
        private string[] standartRoomsNames;
        private string[] goldRoomsNames;
        private int[] standartRoomPlayers;
        private int[] goldRoomPlayers;
        private int maxRoomPlayers;
        public string GameName { get { return gameName; } set {gameName=value; } }
        public int StandatrGameRooms { get { return standatrGameRooms; } set { standatrGameRooms = value; } }
        public int GoldGameRooms { get { return goldGameRooms; } set { goldGameRooms = value; } }
        public int CurrentGoldCoints { get { return currentGoldCoints; } set { currentGoldCoints = value; } }
        public int StandartGameGoldCosts { get { return standartGameGoldCosts; } set { standartGameGoldCosts = value; } }
        public int GoldGameGoldCosts { get { return goldGameGoldCosts; } set { goldGameGoldCosts = value; } }
        public string[] StandartRoomsNames { get { return standartRoomsNames; } set { standartRoomsNames = value; } }
        public string[] GoldRoomsNames { get { return goldRoomsNames; } set { goldRoomsNames = value; } }
        public int[] StandartRoomPlayers { get { return standartRoomPlayers; } set { standartRoomPlayers = value; } }
        public int[] GoldRoomPlayers { get { return goldRoomPlayers; } set { goldRoomPlayers = value; } }
        public int MaxRoomPlayers { get { return maxRoomPlayers; } set { maxRoomPlayers = value; } }

        public Game()
        { }
        public Game(string name,int rooms,int goldrooms,int coints)
        {
            gameName = name;
            standatrGameRooms = rooms;
            goldGameRooms = goldrooms;
            currentGoldCoints = coints;
            standartGameGoldCosts = 300;
            goldGameGoldCosts = 500;
            standartRoomsNames= new string[standatrGameRooms];
            goldRoomsNames = new string[goldGameRooms];
            standartRoomPlayers = new int[standatrGameRooms];
            goldRoomPlayers = new int[goldGameRooms];
            for (int i = 0; i <= standatrGameRooms-1; i++)
            {
                standartRoomsNames[i] = "Standart Server" +" "+ (i + 1);
            }
            for (int i = 0; i <= goldGameRooms- 1; i++)
            {
                goldRoomsNames[i] = "Gold Server" + " " + (i + 1);
            }
            for (int i = 0; i <= standatrGameRooms - 1; i++)
            {
                standartRoomPlayers[i] = UnityEngine.Random.Range(0, 100);
            }
            for (int i = 0; i <= goldGameRooms - 1; i++)
            {
                goldRoomPlayers[i] = UnityEngine.Random.Range(0, 100);
            }
            maxRoomPlayers = 100;
        }
    }
	void Start ()
    {
        Game starcraft = new Game("StarCraft", 2, 2, 400);
        Game warcraft = new Game("WarCraft", 2, 3, 250);
        Game tanki = new Game("Tanki", 3, 2, 180);
        Game alphaprotokol= new Game("AlphaProtocol", 3, 4, 520);
        Game finansist = new Game("Finansist", 4, 2, 499);
        Game reunion = new Game("Reunion", 3, 3, 120);
        List <Game> Games = new List<Game>() {starcraft,warcraft,tanki,alphaprotokol,finansist,reunion };
        Serializer(Games);
    }
    readonly XmlSerializer serializer = new XmlSerializer(typeof(Game));
    private void Serializer(List<Game> names)
    {
        foreach (var item in names)
        {
            FileStream stream = new FileStream(item.GameName+".xml", FileMode.Create, FileAccess.Write, FileShare.Read);
            serializer.Serialize(stream, item);
            Debug.Log("Create file -"+ item.GameName+".xml");
            stream.Close();
        }
    }
}
