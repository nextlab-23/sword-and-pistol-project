using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyBtnScript : MonoBehaviour
{
    public void StartLobby()
    {
        SceneLoader.instance.LoadScene("LobbyScene");
    }
}
