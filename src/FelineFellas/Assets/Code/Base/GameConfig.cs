using UnityEngine;

namespace FelineFellas
{
    public interface IGameConfig : IService
    {
        FieldConfig Field { get; }
        CardsConfig Cards { get; }
    }

    [CreateAssetMenu(menuName = "375/FelineFellas/GameConfig", order = 100)]
    public class GameConfig : ScriptableObject, IGameConfig
    {
        [field: SerializeField] public FieldConfig Field { get; private set; }
        [field: SerializeField] public CardsConfig Cards { get; private set; }
    }
}