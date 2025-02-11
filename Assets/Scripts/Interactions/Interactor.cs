using Fusion;
using UnityEngine;

public class Interactor : NetworkBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private LayerMask _grabbableMask;
    [SerializeField] private LayerMask _buttonMask;

    private bool _isMouseButtonDown = false;
    private bool _isMouseButtonUp = false;

    private GrabbableObject _grabbableObject = null;
    private SwitchButton _switchButton = null;
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            _isMouseButtonDown = true;
        if(Input.GetMouseButtonUp(0))
            _isMouseButtonUp = true;
    }
    public override void Spawned()
    {
        _playerCamera = Camera.main;
    }
    public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority) return;

        if (_isMouseButtonDown)
        {
            RaycastHit raycastHit;
            float interactDistance = 20f;

            if (Runner.GetPhysicsScene().Raycast(_playerCamera.transform.position, _playerCamera.transform.forward, out raycastHit, interactDistance))
            {
                if (_grabbableObject == null)
                {
                    if (raycastHit.transform.TryGetComponent(out _grabbableObject))
                    {
                        if (_grabbableObject.isGrabbable)
                        {
                            if (!_grabbableObject.HasStateAuthority)
                                _grabbableObject.GetComponent<NetworkObject>().RequestStateAuthority();
                            _grabbableObject.Grab(Utils.GetGrabPoint());
                        }
                    }
                }
                if (raycastHit.transform.TryGetComponent(out _switchButton))
                {
                    if (!_switchButton.HasStateAuthority)
                        _switchButton.GetComponent<NetworkObject>().RequestStateAuthority();
                    _switchButton.ToggleSwitchActive();
                }
            }
            _isMouseButtonDown = false;
        }
        if (_isMouseButtonUp)
        {
            if (_grabbableObject != null)
            {
                _grabbableObject.Drop();
                _grabbableObject = null;
            }
            _isMouseButtonUp = false;
        }
    }
}
