# StreetComb — Gesture System

## Design goals
The gesture system must be:
- Fast
- Forgiving
- Readable
- Learnable
- Mobile-first

The player should feel in control, not like they are drawing random shapes and hoping.

## Input philosophy
Use a layered system:

### Layer 1 — immediate controls
- Tap -> light attack
- Double tap -> pressure combo / follow-up attack
- Hold -> block / charge stance
- Swipe left/right -> dash / step
- Swipe up -> jump / evasive rise
- Swipe down -> low guard / crouch evade

### Layer 2 — specials
- U gesture -> launcher / rising attack
- Semicircle -> signature special move

## Why this structure works
- New players can play almost instantly
- Advanced players get depth from timing and specials
- Recognition can stay robust because the gesture vocabulary stays small

## Recognition rules
Gesture recognition should favor intent over geometric perfection.

### General rules
- Minimum distance threshold before a gesture counts
- Short accidental drags should be ignored
- Gestures must complete within a time window
- UI must show what the system recognized
- If uncertain between two gestures, prefer the simpler one

### Suggested detection priority
1. Hold
2. Tap / double tap
3. Swipe directional
4. U gesture
5. Semicircle

This avoids complex gestures stealing simple commands.

## Gesture definitions

### Tap
- Single quick touch
- No meaningful movement
- Triggers fast attack

### Double tap
- Two quick taps in time window
- Triggers pressure string or fast follow-up

### Hold
- Finger down beyond threshold time
- Little movement allowed
- Block by default, charge when appropriate

### Swipe left / right
- Clear horizontal movement
- Small vertical deviation allowed
- Dash or reposition

### Swipe up
- Clear upward movement
- Jump or evasive rise

### Swipe down
- Clear downward movement
- Low guard / crouch evade / brace

### U gesture
- Down then curve up
- Can be mirrored by facing direction if needed
- Signature launcher / uppercut family gesture

### Semicircle
- Arc gesture with enough travel and curvature
- Used for each fighter's signature special

## Anti-frustration rules
- Never require perfect circles or beautiful handwriting
- Avoid diagonal ambiguity whenever possible
- Show a small recognized-gesture icon near the fighter or HUD
- During early builds, include debug overlays to inspect paths
- Prefer 'no special' over the wrong special

## Character interpretation
Different fighters can share the same gesture family, but the result must feel different.

### Raze
- U -> fast electric uppercut
- Semicircle -> burst rush combo

### Iron Monk
- U -> heavy rising strike
- Semicircle -> crushing guard break special

## What to prototype first
1. Tap
2. Hold
3. Swipe left/right
4. Swipe up
5. U gesture
6. Semicircle

Swipe down and double tap can come shortly after if needed.

## Success criteria
The system is good enough for slice validation if:
- Players understand it quickly
- Recognition feels consistent
- Fights stay readable on phone screens
- Complex gestures feel rewarding, not unreliable
