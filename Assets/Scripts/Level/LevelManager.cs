using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private PlayerController _player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HeightCheck();
    }

    public void OnLevelStart()
    {
        _player = GameManager.Instance.Player;
        _startPosition = _player.transform.position;
        _startRotation = _player.transform.rotation;
    }

    public void OnLevelComplete()
    {

    }

    private void HeightCheck()
    {
        if (_player.transform.position.y < 0)
        {
            ResetPlayer();
        }
    }

    private void ResetPlayer()
    {
        _player.transform.position = _startPosition;
    }
}
