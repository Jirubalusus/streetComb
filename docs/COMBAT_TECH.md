# StreetComb — Combat Technical Outline

## High-level architecture
The combat slice should be built as a small set of focused gameplay systems.

### Core gameplay systems
1. InputCaptureSystem
2. GestureRecognitionSystem
3. FighterController
4. CombatStateMachine
5. CombatResolver
6. HealthSystem
7. EnergySystem
8. AIController
9. HUDPresenter
10. TournamentFlowController

## Responsibilities

### InputCaptureSystem
- Reads touch start / move / end
- Stores raw points with timestamps
- Filters accidental noise
- Exposes normalized gesture path data

### GestureRecognitionSystem
- Receives normalized touch path
- Detects taps, double taps, holds, swipes, U gesture, and semicircle
- Returns a high-confidence recognized action or none
- Provides debug metadata for tuning

### FighterController
- Owns fighter-facing gameplay behavior
- Requests actions from gesture results or AI
- Gates actions based on current state and cooldowns
- Sends events to animation and VFX layers

### CombatStateMachine
Basic slice states:
- Idle
- Startup
- ActiveAttack
- Recovery
- Block
- HitStun
- Dash
- Airborne
- Special
- KO

### CombatResolver
- Resolves hits, blocks, guard break, launcher, and damage
- Applies hit stun, knockback, launch state, and meter gain
- Keeps slice combat deterministic and easy to tune

### HealthSystem
- Tracks life
- Broadcasts damage / KO events

### EnergySystem
- Tracks special meter
- Grants meter on hit / block / time-based rules if needed

### AIController
Slice goal:
- Basic movement pressure
- Simple guard behavior
- Occasional anti-air / punish attempt
- Difficulty scalar per fight

### HUDPresenter
- Health bars
- Energy bars
- Gesture recognized feedback
- Combo / hit / block feedback
- Round result panels

### TournamentFlowController
- Character select
- Tutorial prompts
- Fight sequencing
- Upgrade choices
- Result screen

## Suggested combat data model
Use lightweight ScriptableObject-style data assets later for:
- Fighter stats
- Move definitions
- Gesture-to-action mappings
- Upgrade definitions

## First command/action set
- LightAttack
- PressureCombo
- Block
- DashLeft
- DashRight
- Jump
- LowGuard
- Launcher
- SignatureSpecial

## Combat tuning principles
- Readability over simulation depth
- Tight input buffering for touch actions
- Small forgiving windows on gesture completion
- Strong visual confirmation for recognized intent

## Vertical slice implementation order
1. Raw touch capture
2. Gesture recognition debug scene
3. One fighter sandbox scene
4. Enemy dummy and damage resolution
5. Second fighter behavior variant
6. Simple AI
7. Arena + HUD
8. Tournament flow

## Debug tools required early
- On-screen gesture path line
- Recognized gesture label
- Confidence score label
- Input event log
- Fighter current state label

## Main validation question
Can a player perform intended touch actions consistently enough that combat feels skill-based instead of random?
