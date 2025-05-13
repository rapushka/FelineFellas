using Entitas.Generic;
using TMPro;
using UnityEngine;

namespace FelineFellas
{
    public class UnitStrengthView : BaseListener<GameScope, Strength>
    {
        [SerializeField] private TMP_Text _textMesh;

        public override void OnValueChanged(Entity<GameScope> entity, Strength component)
        {
            _textMesh.text = component.Value.ToString();
        }
    }
}