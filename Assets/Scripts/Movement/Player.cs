using Fusion;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Net.NetworkInformation;

public class Player : NetworkBehaviour
{
    public float PlayerSpeed = 2f;

    private CharacterController _controller;

    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            //Set local player's layer to not render the model
            Utils.SetRenderLayerInChildren(GetComponent<Transform>(), LayerMask.NameToLayer("LocalPlayer"));

            Utils.SetFirstPersonCamera(GetComponent<Transform>());

            _controller = GetComponent<CharacterController>();
        }
    }
   public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority) return;

        Vector3 moveDirection = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        Vector3 move = moveDirection * Runner.DeltaTime * PlayerSpeed;
        _controller.Move(move);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = moveDirection * Runner.DeltaTime * PlayerSpeed;
        }
    }
}
