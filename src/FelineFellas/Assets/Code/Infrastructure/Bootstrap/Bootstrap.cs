using UnityEngine;

namespace FelineFellas
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private GameConfig _gameConfig;

        private void Awake()
        {
            Game.Instance.RegisterServices(_gameConfig);
            Game.Instance.Run();
        }
    }
}