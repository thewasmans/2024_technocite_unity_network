using System;
using Unity.Netcode;
using UnityEngine;

public class PlayerGun : NetworkBehaviour
{
    [SerializeField] public GameObject BulletPrefab;

    private void Update()
    {
        if(!IsLocalPlayer) return;

        if(Input.GetMouseButtonDown(0)) FireRpc();
    }

    [Rpc(SendTo.Server)]
    private void FireRpc()
    {
        var bullet = Instantiate(BulletPrefab, transform.position + transform.forward, transform.rotation);
        bullet.GetComponent<NetworkObject>().Spawn();
    }
}