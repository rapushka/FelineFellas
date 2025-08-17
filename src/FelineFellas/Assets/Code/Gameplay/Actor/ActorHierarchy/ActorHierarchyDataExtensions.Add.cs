using Entitas.Generic;

namespace FelineFellas
{
    public static partial class ActorHierarchyDataExtensions
    {
        /// Create new only on Actor. and for all other entities - pass them this Data by reference
        public static Entity<GameScope> AddHierarchyFromActor(
            this Entity<GameScope> actor,
            Entity<GameScope> stage = null,
            Entity<GameScope> lead = null,
            Entity<GameScope> deck = null
        )
            => actor.AddHierarchy(Create(stage, actor, lead, deck));

        private static ActorHierarchyData Create(
            Entity<GameScope> stage = null,
            Entity<GameScope> actor = null,
            Entity<GameScope> lead = null,
            Entity<GameScope> deck = null
        ) => new() { StageID = stage?.ID(), ActorID = actor?.ID(), LeadID = lead?.ID(), DeckID = deck?.ID() };

        private static Entity<GameScope> AddHierarchy(this Entity<GameScope> entity, ActorHierarchyData data)
            => entity.Add<ActorHierarchy, ActorHierarchyData>(data);
    }
}