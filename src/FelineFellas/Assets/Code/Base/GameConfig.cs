using UnityEngine;

namespace FelineFellas
{
    public interface IGameConfig : IService
    {
        FieldConfig Field { get; }
    }

    [CreateAssetMenu(menuName = "375/DeckScaler/GameConfig", order = 100)]
    public class GameConfig : ScriptableObject, IGameConfig
    {
        [field: SerializeField] public FieldConfig Field { get; private set; }
    }
}