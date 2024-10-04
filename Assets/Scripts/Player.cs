using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public int RandomSeed;

    private NetworkVariable<Color> playerColor = new NetworkVariable<Color>();
    private NetworkVariable<FixedString128Bytes> playerName = new NetworkVariable<FixedString128Bytes>(writePerm : NetworkVariableWritePermission.Owner);
    private TMP_Text tagText;
    public const string username = "weshmangle";

    private void Start()
    {
        Random.InitState(RandomSeed);
        tagText = GetComponentInChildren<TMP_Text>();
    }

    public override void OnNetworkSpawn()
    {
        playerColor.OnValueChanged += OnColorChanged;
        playerName.OnValueChanged += OnNameChanged;

        if(IsLocalPlayer)
        {
            Debug.Log("Spawned ! " + Time.realtimeSinceStartup);
            name += " " + Time.realtimeSinceStartup;
            GetPositionRpc();
        }

        if(IsServer)
        {
            playerColor.Value = Random.ColorHSV();
        }

        OnColorChanged(playerColor.Value, playerColor.Value);

        if(IsOwner)
        {
            playerName.Value = username;
        }
        OnNameChanged(playerName.Value, playerName.Value);
    }

    public override void OnNetworkDespawn()
    {
        playerColor.OnValueChanged -= OnColorChanged;
    }

    public void OnNameChanged(FixedString128Bytes previous, FixedString128Bytes current)
    {
        tagText.text = playerName.Value.ToString();
    }

    public void OnColorChanged(Color previous, Color current)
    {
        Debug.Log($"OnColorChanged {previous} {current}");
        GetComponentInChildren<MeshRenderer>().material.color = current;
    }
    

    [Rpc(SendTo.Server)]
    public void GetPositionRpc()
    {
        var position = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f));
        transform.position = position; 

        SetPositionRpc(position);
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void SetPositionRpc(Vector3 position)
    {
        transform.position = position; 
    }
}