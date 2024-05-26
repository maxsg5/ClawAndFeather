# Claw & Feather

## Table of Contents
- [Claw \& Feather](#claw--feather)
  - [Table of Contents](#table-of-contents)
  - [ToDo List](#todo-list)
    - [General Stuff](#general-stuff)
    - [Calibration](#calibration)
    - [UI Mechanics](#ui-mechanics)
    - [Gameplay Mechanics](#gameplay-mechanics)
  - [Game Summary](#game-summary)
  - [Game Genre](#game-genre)
  - [Target Audience](#target-audience)
  - [Game Mechanics](#game-mechanics)
  - [Story/Theme](#storytheme)
  - [Visual Style](#visual-style)
  - [Audio Style](#audio-style)
  - [Game Flow](#game-flow)
  - [Key Features](#key-features)
  - [Getting Started](#getting-started)
    - [Prerequisites](#prerequisites)
    - [Installation](#installation)

## ToDo List
### General Stuff
- [ ] Character Controller
- [ ] Way To Control Animations
- [ ] Collision Detection
- [ ] Obstacles
- [x] ~~Setup Input~~

### Calibration
- [ ] Main Menu
- [ ] Specific Mechanics
- [ ] Pickups
- [ ] Accuracy Score
- [ ] Keys Hit To Beat
- [ ] Rhythm

### UI Mechanics
- [ ] Hold Space for Pause Menu
- [ ] HUD (Score Tally and Lives)
- [ ] Selected Button Changes Based on Beat
- [ ] Progress Bar
- [ ] Animation to Show How Long Button Has Been Pressed for
- [ ] Tap Space for Changing Selected Button
- [ ] Hold Space to Select Button

### Gameplay Mechanics
- [ ] Press Space to Change Bird Direction
- [ ] Hazards Come Out on Tempo

## Game Summary
A rhythm-based autoscroller where players control a bird navigating through various obstacles while keeping in sync with the beat. The game features a cat and bird duo, with thematic levels and engaging rhythmic mechanics.

## Game Genre
Rhythm, Autoscroller

## Target Audience
Casual gamers, rhythm game enthusiasts, mobile gamers

## Game Mechanics
- **Movement:** The bird moves automatically in a pre-set path. Players must press the space bar to change the bird's direction and maintain rhythm.
- **Rhythm Mechanics:** The bird flaps to the beat, with varying flap intensities (e.g., little flap, big flap). Players must keep the beat to avoid losing grip.
- **Obstacles:** Different obstacles in each level, requiring players to time their movements to avoid them.
  - **Level 1:** Tree branches, baseballs, frisbees, wind turbine propellers
  - **Level 2:** Storm clouds, planes, geese
  - **Level 3:** UFOs, cat aliens, astronauts, shooting stars/meteors, Sputnik

## Story/Theme
The game follows a bird and a cat who, after crashing into the ISS, must navigate through various levels to reach their destination. Each level has a unique theme, with space elements introduced in the third level.

## Visual Style
Colorful, cartoonish art style with vibrant backgrounds and dynamic animations. The game will feature a playful and whimsical aesthetic, with characters bopping to the beat.

## Audio Style
Upbeat and rhythmic soundtrack that drives the gameplay. Sound effects will be synced with player actions and obstacles, enhancing the rhythmic experience.

## Game Flow
- **Start Screen:** Title screen with options to start the game, view high scores, and access settings.
- **Main Game:** Players navigate through levels by keeping in sync with the beat and avoiding obstacles.
- **End Game:** The game ends when the player completes all levels or loses all lives. The final screen displays the player's score and accuracy.

## Key Features
- Rhythmic gameplay with engaging beat synchronization
- Varied obstacles and themes across multiple levels
- Simple, one-button control scheme
- Charming characters and animations

## Getting Started
To get a local copy up and running follow these simple steps.

### Prerequisites
- Unity 2022.3.30f1 or higher
- Git

### Installation
1. Clone the repo
   ```sh
   git clone https://github.com/maxsg5/ClawAndFeather.git
