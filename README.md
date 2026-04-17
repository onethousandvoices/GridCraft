# GridCraft

GridCraft is a Unity 6 grid-based building prototype with touch-first placement, a build catalog, live placement preview, and cell validation on a fixed location. The project is built around VContainer, ScriptableObject-driven data, and a separate grid domain model so placement rules and visuals can evolve without rewriting the core flow.

## What Is Implemented

- Runtime scene bootstrap through `SceneBootstrapper` and `ConstructionInstaller` with modular dependency registration
- Loading of all core data from `Resources/Construction`: catalog, economy, layout, scene, and visuals
- Build catalog with three placeable objects: `House`, `Storage`, and `Turret`
- Drag from catalog into the build area with transition into placement state
- Ghost preview of the selected building with valid and invalid material switching
- Highlight of every cell inside the current footprint before confirming placement
- Building rotation by 90 degrees through placement controls
- Confirm and cancel actions through a dedicated placement UI panel
- Resource wallet with starting balance `1000` and immediate cost deduction after successful placement
- Card affordability refresh based on the current resource balance
- Grid model with three cell blocking states: `Obstacle`, `Forbidden`, and `Occupied`
- Placement validation through independent rules for out of bounds, occupied cells, obstacle cells, forbidden cells, and insufficient resources
- Screen-to-world conversion through projection onto the location plane
- Layout config with an `8 x 10` grid, predefined obstacle cells for props, and a forbidden zone
- Pooling for placed buildings and grid highlights with prewarm at startup
- Safe area support for UI with a dedicated iOS top inset adjustment

## Design Decisions

- The project is split into bootstrap, config, domain, controllers, factories, input, pooling, state, UI, and world. This keeps runtime logic separate from presentation and makes navigation simpler.
- All tunable data lives in `ScriptableObject` assets. Catalog entries, cost, footprint, grid layout, visual materials, and prefabs are configured through assets instead of hardcoded values in controllers.
- The scene is assembled at runtime from `ConstructionSettingsAsset`. `MainCanvasView` and `LocationRootView` are instantiated through `ConstructionSceneBuilder`, and dependencies are registered through VContainer.
- Grid responsibilities are separated. `GridService` handles coordinates, cell size, and world conversion, while `GridMapModel` stores cell state and blocking flags.
- Props do not occupy cells through collider checks or scene traversal. Their blocked cells are declared in the layout asset, so placement is validated against stable data instead of runtime scene scans.
- Placement validation is modeled as a pipeline of `IPlacementRule` implementations. New restrictions can be added without rewriting `PlacementController`.
- Placement state is stored in `BuildModeStateMachine` and `BuildSessionModel`. UI and input controllers react to shared state instead of depending directly on each other.
- Ghosts, placed buildings, and grid highlights all use the same generic `ComponentPool<TKey, TView>`. This reduces `Instantiate` calls during interaction and keeps repeated placements predictable.
- Placement feedback is rendered through shared material swapping and per-cell highlights. The visual state changes without creating new materials per object.
- The current input path is touch-oriented. Active placement pointer updates use `EnhancedTouch`, and location targeting is resolved by projecting touch coordinates onto the grid plane.

## How To Run

1. Open the project in `Unity 6.0.59f2`.
2. Open the scene `Assets/Scenes/SampleScene.unity`.
3. Enter `Play Mode`.
4. In the bottom catalog, start dragging a building card onto the playfield.
5. Release the card over the grid and use `Rotate`, `Confirm`, or `Cancel` when needed.
6. For full validation of the current UX, use touch input because the active placement pointer is updated through `EnhancedTouch`.
