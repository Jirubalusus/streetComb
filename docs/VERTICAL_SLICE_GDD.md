# StreetComb — Vertical Slice GDD

## Goal
Validate whether gesture-based 1v1 combat can feel responsive, fun, and commercially promising on mobile.

## Slice content
- 2 fighters: Raze and Iron Monk
- 1 arena
- 3-fight tournament run
- Basic AI
- Basic HUD
- Core gesture input
- Win/lose flow
- Small between-fight upgrade choice

## Player flow
1. Title screen
2. Character select
3. Short tutorial prompt
4. Fight 1
5. Choose 1 of 2 upgrades
6. Fight 2
7. Choose 1 of 2 upgrades
8. Fight 3 / mini-boss feel
9. Victory or defeat summary

## Core systems
### 1. Gesture input
The heart of the game. Must be responsive and visible.

### 2. Combat state machine
Basic states:
- Idle
- Attack
- Block
- Hit stun
- Dash
- Jump / airborne
- Special
- KO

### 3. Fighter kits
Two contrasting fighters to prove style variety.

### 4. AI
Simple but enough to pressure, defend, and punish obvious mistakes.

### 5. Progression lite
Simple run modifiers between fights to add replayability.

## Fighter 1 — Raze
- Fast rushdown fighter
- Easy to understand
- Strong dash pressure
- Electric visual feedback

Move intent:
- Tap: quick jab
- Double tap: pressure string
- Hold: brief block / charged burst
- Swipe left/right: fast dash
- Swipe up: quick evasive rise
- U: fast uppercut
- Semicircle: burst combo special

## Fighter 2 — Iron Monk
- Heavier, technical fighter
- Strong defense and punish tools
- Slower but more damaging

Move intent:
- Tap: palm strike
- Double tap: heavy double hit
- Hold: strong guard / charge impact
- Swipe left/right: slower step
- Swipe up: committed rise
- U: heavy launcher
- Semicircle: crushing special / guard break

## Arena
Single clean stylized urban arena.

Requirements:
- Strong silhouette separation from fighters
- No noisy background clutter
- Good readability on mobile

## HUD
- Player health bar
- Enemy health bar
- Energy / special meter
- Recognized gesture feedback
- Pause
- Victory / defeat panels

## Upgrade examples
Between fights, choose one:
- +10% light attack damage
- Better guard stability
- Longer dash
- Faster special gain
- Extra launcher damage

## Visual feedback requirements
- Finger trail while drawing
- Confirmed gesture icon or text
- Hit sparks
- Light screen shake on heavier impacts
- Brief hit stop on strong attacks
- Distinct VFX per fighter

## Audio goals
- Responsive hit sounds
- Small UI feedback sounds
- Distinct special cues per fighter

## Technical priority order
1. Touch capture
2. Gesture recognition
3. Basic combat loop
4. HUD feedback
5. AI
6. Tournament flow
7. Polish

## Kill criteria
If the slice fails at these, stop expanding content and iterate the core:
- Inputs feel unreliable
- Player cannot understand what move was recognized
- Combat feels floaty or weak
- Fighters do not feel meaningfully different
