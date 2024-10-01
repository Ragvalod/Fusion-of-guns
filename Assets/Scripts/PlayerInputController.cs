using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YG;

public class PlayerInputController : SoundsManeger
{
    [SerializeField] private Transform _leftBorder;
    [SerializeField] private Transform _rightBorder;
    [SerializeField] private TextMeshProUGUI _poitsText;
    [SerializeField] private TextMeshProUGUI _poitsRecordText;
    [SerializeField] private GameObject[] _spawnObgects;
    [SerializeField] private GameObject[] _spawnObgectsPreview;
    [SerializeField] private GameObject[] _spawnModifaer;
    [SerializeField] private GameObject _OffCursorObject;
    [SerializeField] private Transform _spawnPostion;
    [SerializeField] private bool _isSpawned;
    [SerializeField] private float _canSpawning;
    [SerializeField] private Camera _camera;
    [SerializeField] private Vector2 _cursorPosition;
    private int randomShape;
    private bool cursorIsOff;
    GameObject cursorPrefab;
    private const string POINTS_SAVER = "pointsSaver";
    [SerializeField] private GameObject _cursorPrefab;
    private int points;
    private bool isExposive;
    private bool isErasible;
    private GameObject curentSpawnObgect;

    private void Start()
    {

        if (_OffCursorObject != null)
        {
            // Добавляем коллайдер, если его нет
            if (_OffCursorObject.GetComponent<Collider>() == null)
            {
                _OffCursorObject.AddComponent<BoxCollider>();
            }
        }

        cursorIsOff = false;
        YandexGame.FullscreenShow();
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Confined;
        randomShape = Random.Range(0, _spawnObgects.Length);
        curentSpawnObgect = _spawnObgects[randomShape];
        ShowGAmeObget();
        isErasible = false;
        isExposive = false;
        points = 0;
        _poitsRecordText.text = PlayerPrefs.GetInt(POINTS_SAVER).ToString();
        _poitsText.text = "0";
    }



    private void OnEnable()
    {
        GameOver.onClichedToRestart += SaveCounPoints;
    }

    private void OnDisable()
    {
        GameOver.onClichedToRestart -= SaveCounPoints;
    }

    private void Update()
    {
       

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (Time.timeScale != 0 && !cursorIsOff)
            InputPlayer();
        if (cursorPrefab != null)
        {
            cursorPrefab.transform.position = _spawnPostion.position;
        }
    }

    private void InputPlayer()
    {
        Cursor.lockState = CursorLockMode.Confined;
        _spawnPostion.position = new Vector2(Mathf.Clamp(_camera.ScreenToWorldPoint(Input.mousePosition).x, _leftBorder.position.x, _rightBorder.position.x), _spawnPostion.position.y);
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(InstantiateConnect());
        }
    }

    private IEnumerator InstantiateConnect()
    {
        if (_isSpawned)
        {
            _isSpawned = false;
            PlaySound(0);
            Instantiate(curentSpawnObgect, _spawnPostion.position, Quaternion.identity);
            Destroy(cursorPrefab);
            randomShape = Random.Range(0, _spawnObgects.Length);
            curentSpawnObgect = _spawnObgects[randomShape];
            yield return new WaitForSeconds(_canSpawning);
            ShowGAmeObget();
            _isSpawned = true;
        }
    }

    public void OnClickToModifaer(int type)
    {
        switch (type)
        {
            case 0:
                _isSpawned = false;
                isErasible = true;
                curentSpawnObgect = _spawnModifaer[0];
                break;
            case 1:
                _isSpawned = false;
                isExposive = true;
                curentSpawnObgect = _spawnModifaer[1];
                break;
            default:
                break;
        }
        _isSpawned = true;
    }

    public void AddPoints(int points)
    {
        this.points += points;
        _poitsText.text = this.points.ToString();
    }

    public void SetIsSpawned(bool isChecked)
    {
        if (isChecked)
            _isSpawned = false;
        else
            _isSpawned = true;
    }

    //Метод спавна фигуры для предпросмотра 
    private void ShowGAmeObget()
    {
        if (cursorPrefab != null)
        {
            Destroy(cursorPrefab);
        }
        GameObject eee = _spawnObgectsPreview[randomShape];
        cursorPrefab = Instantiate(eee, _cursorPrefab.transform.position, Quaternion.identity);
    }

    //Метод сохранения очков
    public void SaveCounPoints()
    {
        int tempPointsCount = PlayerPrefs.GetInt(POINTS_SAVER);
        if (tempPointsCount < points)
        {
            PlayerPrefs.SetInt(POINTS_SAVER, points);
            YandexGame.NewLeaderboardScores("score", points);
            Debug.Log("Сохранились!");
        }
    }
       
}
