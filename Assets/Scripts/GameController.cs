using System.IO;
using Photon.Pun;
using UnityEngine;

public class GameController : MonoBehaviour
{
    void Start()
    {
        CreatePlayer();
    }

    Vector3 GetRandomSpawnPoint()
    {
        SpawnPoint[] spawnPoints = (SpawnPoint[]) GameObject.FindObjectsOfType(typeof(SpawnPoint));
        if (spawnPoints == null || spawnPoints.Length < 1)
        {
            return new Vector3(0, 0, 0);
        }

        SpawnPoint randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        return randomSpawnPoint.transform.position;
    }

    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        GameObject player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"),
            GetRandomSpawnPoint(), Quaternion.identity);
        var characterController = player.GetComponent<CharacterController>();
        var photonView = player.GetComponent<PhotonView>();
        var camera = player.GetComponentInChildren<Camera>();
        var audioListener = player.GetComponentInChildren<AudioListener>();
        if (photonView.IsMine)
        {
            characterController.enabled = true;
            camera.enabled = true;
            audioListener.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}