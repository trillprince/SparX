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
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

  
}
