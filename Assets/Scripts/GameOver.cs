using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private float _penatlyTime;
    [SerializeField] private float _time;
    [SerializeField] private GameObject _gameOwerPanel;
    [SerializeField] private TextMeshProUGUI _timerText;

    bool _stay;
    public static Action onClichedToRestart;

    void Start()
    {
        _stay = false;
        _time = _penatlyTime;
        _timerText.text = _time.ToString();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "obg")
        {
            _time -= Time.fixedDeltaTime;
            _timerText.text = _time.ToString();
            _stay = true;
            if (_time <= 0)
                SetGameOwer();
        }
        else
            _stay = false;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "obg" && !_stay)
        {
            _time = _penatlyTime;
            _timerText.text = _time.ToString();
        }
    }

    private void SetGameOwer()
    {
        _gameOwerPanel.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartButtonController()
    {
        if (onClichedToRestart != null)
            onClichedToRestart.Invoke();
        SceneManager.LoadScene(0);
    }
}
