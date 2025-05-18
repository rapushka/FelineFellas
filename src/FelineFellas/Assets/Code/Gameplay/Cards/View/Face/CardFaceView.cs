using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public class CardFaceView : BaseListener<GameScope, CardFace>
    {
        [SerializeField] private GameObject _faceUp;
        [SerializeField] private GameObject _faceDown;

        private Vector3 _initScale;

        public override void Register(Entity<GameScope> entity)
        {
            base.Register(entity);

            _initScale = _faceUp.transform.localScale;
        }

        public override void OnValueChanged(Entity<GameScope> entity, CardFace component)
        {
            _faceUp.transform.localScale = Vector3.zero;
            _faceDown.transform.localScale = Vector3.zero;

            var currentFace = component.Value is Face.FaceUp ? _faceUp
                : component.Value is Face.FaceDown           ? _faceDown
                                                               : throw new("Unknown Face");

            currentFace.transform.localScale = _initScale;
        }
    }
}