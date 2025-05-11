using UnityEngine;

namespace FelineFellas
{
    public interface IGameConfig : IService
    {
        FieldConfig Field { get; }
        CardsConfig Cards { get; }

        UiConfig UI { get; }

        TurnsConfig Turns { get; }

        CameraDirector CameraDirectorPrefab { get; }

        GameModesConfig GameModes { get; }
    }

    [CreateAssetMenu(menuName = "375/FelineFellas/GameConfig", order = 100)]
    public class GameConfig : ScriptableObject, IGameConfig
    {
        [field: NaughtyAttributes.BoxGroup(nameof(Field))]
        [field: SerializeField] public FieldConfig Field { get; private set; }

        [field: NaughtyAttributes.BoxGroup(nameof(Cards))]
        [field: SerializeField] public CardsConfig Cards { get; private set; }

        [field: NaughtyAttributes.BoxGroup(nameof(Turns))]
        [field: SerializeField] public TurnsConfig Turns { get; private set; }

        [field: NaughtyAttributes.BoxGroup(nameof(UI))]
        [field: SerializeField] public UiConfig UI { get; private set; }

        [field: NaughtyAttributes.BoxGroup("Camera")]
        [field: SerializeField] public CameraDirector CameraDirectorPrefab { get; private set; }

        [field: NaughtyAttributes.BoxGroup(nameof(GameModes))]
        [field: SerializeField] public GameModesConfig GameModes { get; private set; }
    }
}