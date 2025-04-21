using UnityEngine;

namespace FelineFellas
{
    public interface IGameConfig : IService { }

    [CreateAssetMenu(menuName = "375/DeckScaler/GameConfig", order = 100)]
    public class GameConfig : ScriptableObject, IGameConfig { }
}