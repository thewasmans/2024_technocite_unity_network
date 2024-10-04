using Unity.Netcode;
using UnityEngine;

public class PlayerMotion : NetworkBehaviour
{
    public float Speed = 5.0f;
    void Start()
    {
        
    }

    void Update()
    {
        if(!IsLocalPlayer) return;
        
        var direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        transform.position += direction * Speed * Time.deltaTime;
    }
}
