# 🚀 Space Scavenger Simulator

A Unity-based space scavenging game where players pilot a spaceship to collect space debris using a tractor beam system.

> 🇷🇺 [Русская версия](README_RU.md) | 🇬🇧 [English version](README.md)

## 📖 Overview

Space Scavenger Simulator is a physics-based space exploration game where you control a spaceship equipped with a tractor beam. Your mission is to collect space debris (trash) scattered across the environment and deliver it to your base for processing.

## ✨ Features

### Current Implementation
- 🎮 **Player Spaceship Control**: Physics-based movement with forward/backward thrust and rotation
- 🔮 **Tractor Beam System**: Target and capture space debris within range
- 🏠 **Base Collection System**: Deliver collected items to the base for processing
- ⚙️ **Physics-Based Gameplay**: Realistic physics interactions with rigidbodies and joints

### 🎯 Controls
- **WASD / Arrow Keys**: Move forward/backward and turn
- **Space**: Activate tractor beam to capture targeted debris
- **G**: Release currently held object

## 🔧 Technical Details

### Unity Version
- Unity 2022.3 LTS or later (recommended)

### Key Packages
- Universal Render Pipeline (URP) 17.0.4
- Input System 1.14.0
- AI Navigation 2.0.7

### Project Structure
```
Assets/
├── Scripts/
│   ├── Player/
│   │   ├── PlayerController.cs    # Spaceship movement and physics
│   │   └── TractorBeam.cs         # Tractor beam targeting and capture
│   └── Base/
│       └── BaseCollector.cs       # Base collection point logic
└── Scenes/
    └── SampleScene.unity          # Main game scene
```

## 🚀 Getting Started

### Prerequisites
- Unity Editor 2022.3 LTS or newer
- Git (for version control)

### Installation
1. Clone the repository
2. Open the project in Unity Editor
3. Open `Assets/Scenes/SampleScene.unity`
4. Press Play to start the game

### Setup Requirements
- Ensure objects tagged as "Trash" are present in the scene
- Configure the base collector trigger collider
- Set up the player's tractor beam hold point transform

## 📋 MVP & Development

For detailed MVP requirements and development roadmap, see [MVP.md](MVP.md).

## ⚠️ Known Issues
- 🐛 Debug messages in BaseCollector.cs are in Russian (need localization)
- 👁️ No visual feedback for tractor beam range
- 🎨 Missing UI elements for game state

## 🤝 Contributing

This is a personal project, but suggestions and feedback are welcome! 💡

## 📄 License

See LICENSE file for details.

## 🙏 Credits

Developed using Unity Engine and Universal Render Pipeline.
