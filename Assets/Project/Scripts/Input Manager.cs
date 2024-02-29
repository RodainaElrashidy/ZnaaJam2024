using UnityEngine;

public class InputManager : MonoBehaviour
{
    private GameInputSystem _playerInput;
    private GameInputSystem.PlayerActions _playerActions;

    private ServiceLocator _serviceLocator;

    private void Awake()
    {
        _serviceLocator = ServiceLocator.Instance;
        _serviceLocator.RegisterService(this);

        _playerInput = new GameInputSystem();
        _playerActions = _playerInput.Player;

        _playerActions.BoxRotate.performed += _ => _serviceLocator.GetService<BoxRotationManager>().GetBoxRotation().FlipRotation();
        _playerActions.Jump.performed += context => _serviceLocator.GetService<PlayerJumping>().HandleJump(context.ReadValue<float>() > 0);
    }

    private void OnEnable() => _playerInput.Enable();

    private void OnDisable() => _playerInput.Disable();
}