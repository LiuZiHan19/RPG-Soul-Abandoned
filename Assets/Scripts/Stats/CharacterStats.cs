using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CharacterStats : MonoBehaviour
{
    public Action OnHealthChanged { get; set; }

    [SerializeField] private int _currentHealth;
    
    [Header("Major Stats")] 
    [SerializeField]
    public Stat agility; // 1 point increase evasion by 1% and critical.chance by 1%
    public Stat intelligence; // 1 point increase magic damage by 1 and magic resistance by 3
    public Stat strength; // 1 point increase damage by 1 and critical.power by 1%
    public Stat vitality; // 1 point increase health by 3 or 5 points
    
    [Header("Offensive Stats")] 
    public Stat damage;
    public Stat criticalPower; // default 150%
    public Stat criticalChance;

    [Header("Defensive Stats")]
    public Stat maxHealth;
    public Stat evasion;
    public Stat armor;
    public Stat magicResistance;

    [Header("Magic Stats")] 
    [SerializeField] private bool _isChilled; // reduce armor by 20%
    [SerializeField] private bool _isIgnited; // does damage over time
    [SerializeField] private bool _isShocked; // reduce accuracy by 20%

    [SerializeField] private Stat _igniteDuration;
    public Stat igniteDamage;
    [SerializeField] private float _igniteTimer;
    [SerializeField] private float _igniteDamageCooldown;
    [SerializeField] private float _igniteDamageTimer;

    [SerializeField] private Stat _iceDuration;
    public Stat iceDamage;
    [SerializeField] private float _iceTimer;
    [SerializeField] private float _frostIntensity;

    [SerializeField] private Stat _thunderDuration;
    public Stat thunderDamage;
    [SerializeField] private float _thunderTimer;

    private CharacterStats _applyStats;
    private EntityFx _entityFX;
    private Entity _entity;

    protected virtual void Start()
    {
        _igniteDamageCooldown = .25f;
        criticalPower.SetDefaultValue(150);
        _currentHealth = maxHealth.GetValue();
        _entityFX = GetComponent<EntityFx>();
        _entity = GetComponent<Entity>();
    }

    private void Update()
    {
        _igniteTimer -= Time.deltaTime;
        _igniteDamageTimer -= Time.deltaTime;
        _iceTimer -= Time.deltaTime;
        _thunderTimer -= Time.deltaTime;

        if (_igniteTimer < 0 && _isIgnited)
        {
            _isIgnited = false;
            _entityFX.RemoveElementFx();
        }

        if (_iceTimer < 0 && _isChilled)
        {
            _isChilled = false;
            _entityFX.RemoveElementFx();
            _entity.RemoveIce();
        }

        if (_thunderTimer < 0 && _isShocked)
        {
            _isShocked = false;
            _entityFX.RemoveElementFx();
        }

        if (_isIgnited && _igniteDamageTimer < 0)
        {
            _igniteDamageTimer = _igniteDamageCooldown;
            TakeDamage(Mathf.RoundToInt(_applyStats.igniteDamage.GetValue()));
        }
    }

    public float GetCurHealthPercentage()
    {
        int maxHp = maxHealth.GetValue() + vitality.GetValue() * 3;
        float percentage = (float)_currentHealth / maxHp;
        return percentage;
    }

    public void DoDamage(CharacterStats targetStats)
    {
        // 1. Evasion
        if (CanEvasion(targetStats)) return;

        // 2. Total damage
        int totalDamage = strength.GetValue() + damage.GetValue();

        // 3. Critical Damage
        if (CanCritical()) totalDamage = CheckCriticalDamage(totalDamage);

        // 4. Armor
        totalDamage = CheckArmor(targetStats, totalDamage);

        // 5. Take damage
        targetStats.TakeDamage(totalDamage);
    }

    public void DoMagicDamage(CharacterStats targetStats)
    {
        targetStats._applyStats = this;

        int igniteDamage = this.igniteDamage.GetValue();
        int chillDamage = iceDamage.GetValue();
        int lightingDamage = thunderDamage.GetValue();

        // Avoid infinite loops
        if (Mathf.Max(igniteDamage, chillDamage, lightingDamage) <= 0) return;

        int totalMagicDamage = igniteDamage + chillDamage + lightingDamage + intelligence.GetValue();
        totalMagicDamage = CheckMagicResistance(targetStats, totalMagicDamage);
        targetStats.TakeDamage(totalMagicDamage);

        // Randomly apply an element 
        while (true)
        {
            if (Random.value < 0.5f && igniteDamage > 0)
            {
                targetStats.ApplyIgnite(_igniteDuration.GetValue());
                return;
            }

            if (Random.value < 0.5f && chillDamage > 0)
            {
                targetStats.ApplyIce(_iceDuration.GetValue());

                return;
            }

            if (Random.value < 0.5f && lightingDamage > 0)
            {
                targetStats.ApplyThunder(_thunderDuration.GetValue());
                targetStats.GetComponent<Entity>().Knockback(_entity.FacingDir, _entity.thunderStrikeForce);
                return;
            }
        }
    }

    protected virtual void TakeDamage(int damage)
    {
        Debug.Log(gameObject.name + " is takedmage " + damage);
        OnHealthChanged?.Invoke();
        _currentHealth -= damage;
        if (_currentHealth <= 0) Die();
    }

    protected virtual void Die()
    {
        Debug.Log(gameObject.name + " is dead.");
    }

    private bool CanEvasion(CharacterStats targetStats)
    {
        int totalEvasion = targetStats.evasion.GetValue() + targetStats.agility.GetValue();

        if (_isShocked) totalEvasion += 20;

        if (Random.Range(0, 100) < totalEvasion) return true;

        return false;
    }

    private bool CanCritical()
    {
        int totalCriticalChance = criticalChance.GetValue() + agility.GetValue();
        if (Random.Range(0, 100) <= totalCriticalChance)
            return true;
        return false;
    }

    private int CheckArmor(CharacterStats targetStats, int totalDamage)
    {
        if (targetStats._isChilled)
            totalDamage -= Mathf.RoundToInt(targetStats.armor.GetValue() * .8f);
        else
            totalDamage -= targetStats.armor.GetValue();
        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }

    private int CheckCriticalDamage(int damage)
    {
        float totalCriticalPower = (criticalPower.GetValue() + strength.GetValue()) * .01f;
        float criticalDamage = damage * totalCriticalPower;
        return Mathf.RoundToInt(criticalDamage);
    }

    private int CheckMagicResistance(CharacterStats targetStats, int totalMagicDamage)
    {
        totalMagicDamage -= targetStats.magicResistance.GetValue() + targetStats.intelligence.GetValue() * 3;
        totalMagicDamage = Mathf.Clamp(totalMagicDamage, 0, int.MaxValue);
        return totalMagicDamage;
    }

    private void ApplyIgnite(float duration)
    {
        _entityFX.ApplyIgnite();
        _igniteTimer = duration;
        _isIgnited = true;
        _entity.ApplyIgnite();
    }

    private void ApplyIce(float duration)
    {
        _entityFX.ApplyIce();
        _iceTimer = duration;
        _isChilled = true;
        _entity.ApplyIce();
    }

    private void ApplyThunder(float duration)
    {
        _entityFX.ApplyThunder();
        _thunderTimer = duration;
        _isShocked = true;
        _entity.ApplyThunder();
    }
}