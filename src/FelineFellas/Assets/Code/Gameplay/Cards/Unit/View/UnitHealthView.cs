using Entitas.Generic;
using TMPro;
using UnityEngine;

namespace FelineFellas
{
    public class UnitHealthView
        : BaseListener<GameScope>,
          IRegistrableListener<GameScope, Health>,
          IRegistrableListener<GameScope, MaxHealth>
    {
        [SerializeField] private SpriteProgressBar _progressBar;
        [SerializeField] private TMP_Text _text;

        private Entity<GameScope> _entity;

        public override void Register(Entity<GameScope> entity)
        {
            _entity = entity;
            _entity.Retain(this);

            _entity.AddListener<Health>(this);
            _entity.AddListener<MaxHealth>(this);

            if (_entity.Has<Health>() && _entity.Has<MaxHealth>())
                UpdateValue(_entity);
        }

        public void OnValueChanged(Entity<GameScope> entity, Health component)    => UpdateValue(entity);
        public void OnValueChanged(Entity<GameScope> entity, MaxHealth component) => UpdateValue(entity);

        private void UpdateValue(Entity<GameScope> entity)
        {
            if (_entity != entity)
                return;

            var currentHP = entity.Get<Health>().Value;
            var maxHP = entity.Get<MaxHealth>().Value;

            _progressBar.SetValue((float)currentHP / maxHP);

            if (_text != null)
                _text.text = $"{currentHP}/{maxHP}";
        }

        public override void Unregister()
        {
            _entity.RemoveListener<Health>(this);
            _entity.RemoveListener<MaxHealth>(this);

            _entity.Release(this);
            _entity = null;
        }
    }
}