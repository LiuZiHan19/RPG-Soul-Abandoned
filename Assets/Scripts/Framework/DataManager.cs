using System;

public class DataManager : MonoSingleton<DataManager>
{
    private GameDataModel _gameData;
    private PlayerDataModel _playerData;
    private InventoryDataModel _inventoryData;
    private SkillDataModel _skillData;

    public GameDataModel GameData => _gameData;
    public PlayerDataModel PlayerData => _playerData;
    public InventoryDataModel InventoryData => _inventoryData;
    public SkillDataModel SkillData => _skillData;

    protected override void Awake()
    {
        base.Awake();
        _gameData = new GameDataModel();
        _playerData = new PlayerDataModel();
        _inventoryData = new InventoryDataModel();
        _skillData = new SkillDataModel();
    }

    public void Initialize()
    {
        LoadData(E_DataModel.Inventory);
    }

    private void LoadData(E_DataModel dataModel)
    {
        try
        {
            switch (dataModel)
            {
                case E_DataModel.Player:
                    _playerData = JsonMgr.Instance.LoadData<PlayerDataModel>("PlayerData");
                    if (_playerData == null)
                    {
                        _playerData = new PlayerDataModel();
                        Logger.LogWarning(
                            "Player data is null. Created a new instance of PlayerDataModel.");
                    }

                    break;

                case E_DataModel.Skill:
                    _skillData = JsonMgr.Instance.LoadData<SkillDataModel>("SkillData");
                    if (_skillData == null)
                    {
                        _skillData = new SkillDataModel();
                        Logger.LogWarning(
                            "Skill data is null. Created a new instance of SkillDataModel.");
                    }

                    break;

                case E_DataModel.Inventory:
                    _inventoryData = JsonMgr.Instance.LoadData<InventoryDataModel>("InventoryData");
                    if (_inventoryData == null)
                    {
                        _inventoryData = new InventoryDataModel();
                        Logger.LogWarning(
                            "Inventory data is null. Created a new instance of InventoryDataModel.");
                    }

                    break;

                case E_DataModel.GameData:
                    _gameData = JsonMgr.Instance.LoadData<GameDataModel>("GameData");
                    if (_gameData == null)
                    {
                        _gameData = new GameDataModel();
                        Logger.LogWarning(
                            "Game data is null. Created a new instance of GameDataModel.");
                    }

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(dataModel), dataModel, null);
            }
        }
        catch (Exception ex)
        {
            Logger.LogError($"Failed to load {dataModel}: {ex.Message}");
        }
    }

    public void SaveData(E_DataModel dataModel)
    {
        try
        {
            switch (dataModel)
            {
                case E_DataModel.Player:
                    JsonMgr.Instance.SaveData(_playerData, "PlayerData");
                    Logger.Log("Player data saved successfully.");
                    break;

                case E_DataModel.Skill:
                    JsonMgr.Instance.SaveData(_skillData, "SkillData");
                    Logger.Log("Skill data saved successfully.");
                    break;

                case E_DataModel.Inventory:
                    JsonMgr.Instance.SaveData(_inventoryData, "InventoryData");
                    Logger.Log("Inventory data saved successfully.");
                    break;

                case E_DataModel.GameData:
                    JsonMgr.Instance.SaveData(_gameData, "GameData");
                    Logger.Log("Game data saved successfully.");
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(dataModel), dataModel, null);
            }
        }
        catch (Exception ex)
        {
            Logger.LogError($"Failed to save {dataModel}: {ex.Message}");
        }
    }
}