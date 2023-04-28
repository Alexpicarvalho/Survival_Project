using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{

    [Header("Requirements")]
    private Player_Stats _playerStats;
    private PlayerMovement _playerMovement;

    [Header("Internal References")]
    [SerializeField] TextMeshProUGUI _hpText;
    [SerializeField] TextMeshProUGUI _staminaText;
    [SerializeField] TextMeshProUGUI _currentSpeedDebug;

    private void Awake()
    {
        _playerStats = GetComponentInParent<Player_Stats>();
        _playerMovement = GetComponentInParent<PlayerMovement>();   
    }

    private void Update()
    {
        _hpText.text = string.Format(
            "{0}/{1}", _playerStats._currentHp, _playerStats._maxHp);

        _staminaText.text = string.Format(
            "{0}/{1}", _playerStats._currentStamina, _playerStats._maxStamina);

        _currentSpeedDebug.text = _playerMovement.CalculateSpeed().ToString();
    }
}
