using System;
using UnityEngine;

namespace FelineFellas
{
    [Serializable]
    public class UiConfig
    {
        [field: SerializeField] public Canvas CanvasPrefab { get; private set; }

        [field: SerializeField] public MainMenuPage MainMenuPrefab     { get; private set; }
        [field: SerializeField] public GameplayHUD  HUDPrefab          { get; private set; }
        [field: SerializeField] public GameOverPage GameOverPagePrefab { get; private set; }
    }
}