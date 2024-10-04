using Unity.Netcode;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    public float Speed;


    private void Start()
    {
        if(!IsOwner) return;
        
        Destroy(gameObject, 3.0f);
    }

    private void Update()
    {
        if(!IsOwner) return;

        transform.position = transform.forward * Speed * Time.deltaTime;    
    }    
}