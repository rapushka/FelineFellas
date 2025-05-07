using UnityEngine;

namespace FelineFellas
{
    public class CameraDirector : MonoBehaviour
    {
        [field: SerializeField] public Camera MainCamera { get; private set; }
        [field: SerializeField] public Camera UICamera   { get; private set; }
    }
}