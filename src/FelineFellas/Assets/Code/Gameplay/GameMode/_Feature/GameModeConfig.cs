using UnityEngine;

namespace FelineFellas
{
    public interface IGameMode
    {
        bool DiscardHandOnEndTurn { get; }
    }

    [CreateAssetMenu(menuName = "375/FelineFellas/Game Mode", order = 100)]
    public class GameModeConfig : ScriptableObject, IGameMode
    {
        [field: SerializeField] public string Name { get; private set; }

        [field: SerializeField] public bool DiscardHandOnEndTurn { get; private set; }
    }
}