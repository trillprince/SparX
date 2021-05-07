using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class LobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject cancellButton;

    public int roomSize = 20;

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        startButton.SetActive(true);
    }

    public void OnStartButtonClick()
    {
        startButton.SetActive(false);
        cancellButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnCancellButtonClick()
    {
        cancellButton.SetActive(false);
        startButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }


    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join a random room. Making a new one...");
        CreateRoom();
    }

    private void CreateRoom()
    {
        int randomRoomNumber = Random.Range(0, 10000);
        RoomOptions roomOptions = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = (byte) roomSize
        };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOptions);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room... trying again");
        CreateRoom();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}