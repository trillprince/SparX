using System.IO;
using Photon.Pun;
using UnityEngine;

public class GameController : MonoBehaviour
{
    void Start()
    {
        CreatePlayer();
    }

    /*public override void OnJoinedRoom()
    {
    }*/

    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        GameObject player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), new Vector3(925, 229, 195), Quaternion.identity);
        //GameObject player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), new Vector3(0,0,0), Quaternion.identity);
        var characterController = player.GetComponent<CharacterController>();
        var photonView = player.GetComponent<PhotonView>();
        var camera = player.GetComponentInChildren<Camera>();
        if (photonView.IsMine)
        {
            characterController.enabled = true;
            camera.enabled = true;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

  
}
