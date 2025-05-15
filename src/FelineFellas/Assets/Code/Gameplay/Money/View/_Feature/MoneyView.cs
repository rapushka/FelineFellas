using TMPro;
using UnityEngine;

namespace FelineFellas
{
    public class MoneyView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textMesh;

        public int Value { set => _textMesh.text = $"${value}"; }
    }
}