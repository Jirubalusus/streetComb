# StreetComb — Immediate next steps

## What can start right now
- Define exact gesture list
- Define gesture recognition rules and tolerances
- Define first combat loop and state machine
- Define vertical slice feature scope
- Prepare repo/project structure plan

## What is blocked until access/setup exists
- Creating GitHub repo under jirubalusus
- Unity project creation in final target location
- Automated Android builds
- Google Play Internal Testing upload pipeline

## Recommended build order
1. Gesture system design
2. Combat state model
3. Fighter move list for Raze and Iron Monk
4. Vertical slice GDD
5. Unity project bootstrap
6. Android build pipeline
7. Google Play internal testing automation

## Required external setup later
- GitHub access for jirubalusus repo creation/push
- Google Play service account JSON
- Android keystore

## First playable target
A mobile Android build where:
- Player selects Raze or Iron Monk
- Performs tap, hold, swipes, U gesture, and semicircle
- Fights 3 AI opponents in one short run
- Sees clear feedback for recognized gestures
