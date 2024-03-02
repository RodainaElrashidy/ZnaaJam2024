using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoxManager : MonoBehaviour
{
    [SerializeField] private List<LevelBox> _boxRotationList;
    [SerializeField] private GameObject _player;
    [SerializeField] internal GameEvents levelCompleted;

    private LevelBox _levelBox;

    private int _levelBoxIndex = 0;
    private Coroutine currentPlayerScale;

    private ServiceLocator _serviceLocator;

    private void Awake()
    {
        _serviceLocator = ServiceLocator.Instance;
        _serviceLocator.RegisterService(this);
    }

    private void Start() => SetLevelBox();

    private void OnEnable() => levelCompleted.GameAction += SetLevelCompleted;

    private void OnDisable() => levelCompleted.GameAction -= SetLevelCompleted;

    internal void SetLevelBox()
    {
        if (_levelBoxIndex >= _boxRotationList.Count)
            _levelBoxIndex = 0;

        _levelBox = _boxRotationList[_levelBoxIndex];

        _levelBoxIndex++;

        if (currentPlayerScale != null)
            StopCoroutine(currentPlayerScale);
    }

    internal LevelBox GetBoxRotation() => _levelBox;

    private void SetLevelCompleted()
    {
        _levelBox.Coolider2D.enabled = false;

        _levelBox.StartCoroutine(_levelBox.DisbaleObstcals());

        SetLevelBox();
        currentPlayerScale = StartCoroutine(SetPlayerScale());

        _serviceLocator.GetService<PlayerJumping>().SetJumpForce = _levelBox.PlayerJumpForce;
    }

    private IEnumerator SetPlayerScale()
    {
        Vector3 targetScale = new(_levelBox.PlayerScale, _levelBox.PlayerScale);

        while (Vector3.Distance(_player.transform.localScale, targetScale) > 0.01f)
        {
            _player.transform.localScale = Vector3.Lerp(_player.transform.localScale, targetScale, Time.deltaTime);
            yield return null;
        }

        _player.transform.localScale = targetScale;
    }
}