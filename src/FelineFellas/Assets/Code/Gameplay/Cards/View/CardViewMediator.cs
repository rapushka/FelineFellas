using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public class CardViewMediator : MonoBehaviour
    {
        [SerializeField] private GameObject _unitView;

        public void Initialize(Entity<GameScope> card)
        {
            _unitView.SetActive(card.Is<UnitCard>());
        }
    }
}