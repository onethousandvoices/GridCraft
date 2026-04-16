using System;

namespace GridCraft.Construction
{
    public enum BuildMode : byte
    {
        Idle = 0,
        CatalogDrag = 1,
        Placement = 2
    }

    public sealed class BuildModeStateMachine
    {
        public event Action Changed;

        private BuildMode _mode;

        public bool AcceptsPlacementPointer => _mode == BuildMode.Placement;

        public bool ShowsPlacementControls => _mode != BuildMode.Idle;

        public bool AllowsPlacementRotation => _mode == BuildMode.Placement;

        public bool IsPlacementActive => _mode == BuildMode.Placement;

        public void EnterCatalogDrag() => SetMode(BuildMode.CatalogDrag);

        public void EnterPlacement() => SetMode(BuildMode.Placement);

        public void Cancel() => SetMode(BuildMode.Idle);

        private void SetMode(BuildMode mode)
        {
            if (_mode == mode) return;

            _mode = mode;
            Changed?.Invoke();
        }
    }
}