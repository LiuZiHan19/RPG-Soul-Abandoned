public class SkillManager : MonoSingleton<SkillManager>
{
    public Skill_CounterAttack CounterAttack { get; private set; }
    public Skill_Dash Dash { get; private set; }
    public Skill_Clone Clone { get; private set; }
    public Skill_ThrowSword ThrowSword { get; private set; }
    public Skill_MagicOrb MagicOrb { get; private set; }
    public Skill_EnergyOrb EnergyOrb { get; private set; }
    public Skill_ElectricOrb ElectricOrb { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        CounterAttack = GetComponent<Skill_CounterAttack>();
        Dash = GetComponent<Skill_Dash>();
        Clone = GetComponent<Skill_Clone>();
        ThrowSword = GetComponent<Skill_ThrowSword>();
        MagicOrb = GetComponent<Skill_MagicOrb>();
        ElectricOrb = GetComponent<Skill_ElectricOrb>();
        EnergyOrb = GetComponent<Skill_EnergyOrb>();
    }
}