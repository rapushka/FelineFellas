using Entitas.Generic;
using TMPro;
using UnityEngine;

namespace FelineFellas
{
    public class CardTitleView : BaseListener<GameScope, CardTitle>
    {
        [SerializeField] private TMP_Text _textMesh;

        public override void OnValueChanged(Entity<GameScope> entity, CardTitle component)
        {
            _textMesh.text = component.Value;
        }
    }
}