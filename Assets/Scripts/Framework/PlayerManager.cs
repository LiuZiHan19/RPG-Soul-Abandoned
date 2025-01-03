public class PlayerManager : MonoSingleton<PlayerManager>
{
    public Player player { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        player = FindObjectOfType<Player>();
    }
}