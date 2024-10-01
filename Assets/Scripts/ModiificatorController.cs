using TMPro;
using UnityEngine;

public class ModiificatorController : MonoBehaviour
{
    [SerializeField] private GameObject _playerInputController;

    private void OnMouseEnter()
    {
        Debug.Log("Mouse OFF");
        _playerInputController.GetComponent<PlayerInputController>().SetIsSpawned(true);
    }

    private void OnMouseExit()
    {
        Debug.Log("Mouse ON");
        _playerInputController.GetComponent<PlayerInputController>().SetIsSpawned(false);
    }
}
