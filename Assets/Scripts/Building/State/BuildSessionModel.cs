using System;
using GridCraft.Construction.Runtime;
using UnityEngine;

namespace GridCraft.Construction
{
    public sealed class BuildSessionModel
    {
        public event Action Changed;

        public bool HasDefinition { get; private set; }

        public bool HasPreview { get; private set; }

        public BuildingDefinitionAsset CurrentDefinition { get; private set; }

        public PlacementPreview CurrentPreview { get; private set; }

        public Vector2 LastScreenPosition { get; private set; }

        public int RotationQuarterTurns { get; private set; }

        public void Begin(BuildingDefinitionAsset definition, Vector2 screenPosition)
        {
            HasDefinition = true;
            HasPreview = false;
            CurrentDefinition = definition;
            CurrentPreview = default;
            LastScreenPosition = screenPosition;
            RotationQuarterTurns = 0;
            Changed?.Invoke();
        }

        public void Cancel()
        {
            if (!HasDefinition && !HasPreview && RotationQuarterTurns == 0) return;

            HasDefinition = false;
            HasPreview = false;
            CurrentDefinition = null;
            CurrentPreview = default;
            LastScreenPosition = default;
            RotationQuarterTurns = 0;
            Changed?.Invoke();
        }

        public void ClearPreview()
        {
            if (!HasPreview) return;

            HasPreview = false;
            CurrentPreview = default;
            Changed?.Invoke();
        }

        public void RotateClockwise()
        {
            RotationQuarterTurns = RotationQuarterTurns + 1 & 3;
            Changed?.Invoke();
        }

        public void SetLastScreenPosition(Vector2 screenPosition) => LastScreenPosition = screenPosition;

        public void SetPreview(PlacementPreview preview)
        {
            if (HasPreview && CurrentPreview == preview) return;

            HasPreview = true;
            CurrentPreview = preview;
            Changed?.Invoke();
        }
    }
}