using Unity.Netcode;
using UnityEngine;

public class ManagerViewer : MonoBehaviour
{
    public void StartHost() => NetworkManager.Singleton.StartHost();

    public void StartServer() => NetworkManager.Singleton.StartServer();

    public void StartClient() => NetworkManager.Singleton.StartClient();

    public void Stop() => NetworkManager.Singleton.Shutdown();
}
