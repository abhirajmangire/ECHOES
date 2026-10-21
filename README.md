# ECHOES — Phase 1 (Foundation)

## Setup
1. Install Unity Hub, then a **Unity 6 LTS** editor with **Android Build Support** (incl. OpenJDK, Android SDK & NDK). Verify the current LTS on unity.com.
2. Unity Hub > New project > **Universal 3D (URP)** template, name it `ECHOES`.
3. Close Unity, copy this repo's `Assets/` contents and `.gitignore` into your project (merge).
4. Package Manager: ensure **Input System** is installed. Project Settings > Player > Active Input Handling = **Input System Package (New)** (editor restarts).
5. Open a new empty scene, create an empty GameObject, add **PrototypeBootstrap**, press Play, save as `Assets/Scenes/Prototype.unity`.

## Controls (Editor)
WASD move, Shift run, hold right mouse to look, E interact (wired later).

## Status
Written but NOT yet compiled or run — no Unity Editor was available when this was generated. Report any console errors and they get fixed first.
