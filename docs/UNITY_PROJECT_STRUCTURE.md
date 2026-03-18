# StreetComb — Unity Project Structure

## Recommended editor baseline
- Unity 6 LTS line (detected locally: 6000.3.7f1)
- Mobile-first configuration
- Start from 3D core template, then keep rendering lean

## Current blocker
Unity batch project creation is blocked locally because the editor license is not activated in batch mode.

Detected error:
- `No valid Unity Editor license found. Please activate your license.`

## Project root
`unity/StreetComb`

## Scene plan
### Boot
- Minimal bootstrap scene
- Initializes persistent services
- Loads title/menu scene

### Title
- Main menu
- Start run
- Options placeholder

### CharacterSelect
- Choose between Raze and Iron Monk
- Simple fighter preview

### Arena_Fight
- Main combat scene for slice
- Reused for all 3 tournament fights

### Results
- Victory / defeat / summary

## Top-level folder structure
```text
Assets/
  Art/
    Characters/
      Raze/
      IronMonk/
    Environments/
      Arena_Street/
    UI/
    VFX/
    Materials/
  Audio/
    SFX/
    Music/
  Prefabs/
    Characters/
    Combat/
    UI/
    Environment/
  Scenes/
    Boot.unity
    Title.unity
    CharacterSelect.unity
    Arena_Fight.unity
    Results.unity
  Scripts/
    Core/
    Input/
    Combat/
    Fighters/
    AI/
    UI/
    Flow/
    Data/
    Debug/
  ScriptableObjects/
    Fighters/
    Moves/
    Upgrades/
  Settings/
  ThirdParty/
```

## Script folder intent

### Core
- Game bootstrap
- Service registration
- Persistent managers only when actually needed

### Input
- Touch capture
- Gesture normalization
- Gesture recognition
- Gesture debugging

### Combat
- Combat state machine
- Hit resolution
- Damage and launch logic
- Meter logic

### Fighters
- Fighter controller base
- Raze controller/config
- Iron Monk controller/config
- Animation event bridges

### AI
- Slice enemy logic
- Difficulty presets

### UI
- HUD
- Gesture feedback widget
- Results UI
- Upgrade choice UI

### Flow
- Character select flow
- Tournament sequencing
- Scene loading

### Data
- Move definitions
- Fighter stats
- Upgrade definitions

### Debug
- Gesture overlay
- State labels
- Combat event log

## First scripts to create
1. `TouchCapture.cs`
2. `GestureSample.cs`
3. `GestureRecognizer.cs`
4. `GestureDebugOverlay.cs`
5. `FighterController.cs`
6. `CombatStateMachine.cs`
7. `CombatResolver.cs`
8. `HealthComponent.cs`
9. `EnergyComponent.cs`
10. `ArenaFlowController.cs`

## Early prototyping approach
Build in this order:
1. Gesture sandbox scene
2. One fighter + dummy target
3. Damage and hit reactions
4. Second fighter variant
5. Basic enemy AI
6. HUD + tournament loop

## Rendering recommendation
Keep rendering simple for the slice:
- Strong silhouettes
- Limited post-processing
- Readable contrast
- Lightweight effects instead of heavy realism

## Mobile priorities
- 60 FPS target on decent Android device
- Clear touch zones and UI readability
- Immediate gesture feedback
- Low visual clutter

## Definition of 'implementation-ready'
Once Unity license/editor access is confirmed, bootstrap the project and create the script skeletons above immediately.
