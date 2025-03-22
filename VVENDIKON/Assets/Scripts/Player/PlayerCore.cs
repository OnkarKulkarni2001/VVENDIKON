using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;

public class PlayerCore : MonoBehaviour, IHittable
{
    [Header("Stats")]
    [SerializeField] private int _currentHealth = 6;
    [SerializeField] private int _currentCoins = 0;

    [Header("Invincibility")]
    [SerializeField] private float _invincibilityTime = 1f;
    private bool _isInvincible = false;

    [Header("Extra")]
    [SerializeField] private float _durationOfHitFreezeFrame = 0.1f;

    [Header("References")]
    [SerializeField] private InventorySystem inventorySystem;

    public event Action<int> onHealthChanged;
    public event Action<int> onCoinsChanged;
    public event Action onDeath;
    public UnityEvent OnTakeDamage;

    public int Health => _currentHealth;
    public int Coins => _currentCoins;

    private void Awake()
    {
        if (inventorySystem == null)
        {
            inventorySystem = GetComponent<InventorySystem>();
            if (inventorySystem == null)
            {
                UnityEngine.Debug.LogError("InventorySystem not found on PlayerCore!");
            }
        }
    }

    private void Start()
    {
        onHealthChanged?.Invoke(_currentHealth);
    }

    private void Update()
    {
        if (inventorySystem == null) return;

        if (Input.GetKeyDown(KeyCode.Alpha1)) inventorySystem.SelectSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) inventorySystem.SelectSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) inventorySystem.SelectSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) inventorySystem.SelectSlot(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) inventorySystem.SelectSlot(4);

        if (Input.GetKeyDown(KeyCode.Mouse0)) inventorySystem.UseCurrentTool();
        if (Input.GetKeyDown(KeyCode.T)) inventorySystem.ThrowItem();
    }

    public void Respawn()
    {
    }

    private void ModifyHealth(int amount)
    {
        _currentHealth = Mathf.Max(0, _currentHealth + amount);
        onHealthChanged?.Invoke(_currentHealth);

        if (_currentHealth <= 0)
            StartCoroutine(WaitAndHandleDeath());
    }

    IEnumerator WaitAndHandleDeath()
    {
        yield return new WaitForSeconds(0.3f);
        HandleDeath();
    }

    public void ModifyCoins(int amount)
    {
        _currentCoins = Mathf.Max(0, _currentCoins + amount);
        onCoinsChanged?.Invoke(amount);
    }

    public void GiveCoins(int amount)
    {
        ModifyCoins(amount);
    }

    public bool TryBuying(int price)
    {
        if (_currentCoins < price) return false;

        ModifyCoins(-price);
        return true;
    }

    void HandleDeath()
    {
        onDeath?.Invoke();
    }

    public void TakeDamage()
    {
        if (_isInvincible) return;

        OnTakeDamage?.Invoke();

        ModifyHealth(-1);

        // Add freeze-frame effect
        StartCoroutine(FreezeFrameCoroutine());

        StartCoroutine(InvincibilityCoroutine());
    }

    private IEnumerator FreezeFrameCoroutine()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(_durationOfHitFreezeFrame);
        Time.timeScale = 1f;
    }

    private IEnumerator InvincibilityCoroutine()
    {
        _isInvincible = true;
        yield return new WaitForSeconds(_invincibilityTime);
        _isInvincible = false;
    }

    public void TakeHit(HitData hitData)
    {
        TakeDamage();
    }
}