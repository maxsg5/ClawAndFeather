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
- [x] ~~Character Controller~~
- [x] ~~Way To Control Animations~~
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
- [x] ~~Hold Space for Pause Menu~~
- [x] ~~HUD (Score Tally and Lives)~~
- [x] ~~Progress Bar~~
- [x] ~~Animation to Show How Long Button Has Been Pressed for~~
- [x] ~~Tap Space for Changing Selected Button~~
- [x] ~~Hold Space to Select Button~~

### Gameplay Mechanics
- [x] ~~Press Space to Change Bird Direction~~
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

## How to Contribute
1. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
2. Make your Changes
3. **TEST YOUR CHANGES** (run the game and make sure everything works)
4. Add your changes (`git add .`)
5. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
6. Push to the Branch (`git push origin feature/AmazingFeature`)
7. Open a Pull Request
8. Request a review from the project maintainers
9.  Make sure your code is up to the project's [coding standards](#coding-standards).

## Coding Standards
- Use  `camelCase` for public and local variables.
- Use `_camelCase` for private variables.
- Use `PascalCase` for class, property, and method names.
- Use `ALL_CAPS` for constants.
- Only one `return` statement per method.
### Example class
<!-- make this a drop down expandable element -->
<details>
<summary>Click to expand!</summary>

```csharp
public class Player
{
    // Constants (using ALL_CAPS_WITH_UNDERSCORES)
    private const int MAX_HEALTH_INCREMENT = 100;

    // Private fields (using underscores and CamelCase)
    private int _currentHealth;
    private int _maxHealth;

    // Public property (using PascalCase for methods and properties)
    public string Name { get; set; }

    // Constructor (using PascalCase)
    public Player(string name, int initialHealth)
    {
        Name = name;
        _maxHealth = initialHealth;
        _currentHealth = initialHealth;
    }

    // Method to display player info (using PascalCase)
    public void DisplayHealth()
    {
        Console.WriteLine($"{Name} has {_currentHealth} out of {_maxHealth} health points remaining.");
    }

    // Method to apply damage (using PascalCase)
    public void ApplyDamage(int damageAmount)
    {
        if (damageAmount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(damageAmount), "Damage amount cannot be negative.");
        }

        _currentHealth -= damageAmount;
        _currentHealth = Math.Max(_currentHealth, 0);
        Console.WriteLine($"{Name} took {damageAmount} points of damage.");
    }

    // Method to heal the player (using PascalCase)
    public void Heal(int healAmount)
    {
        if (healAmount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(healAmount), "Heal amount cannot be negative.");
        }

        _currentHealth += healAmount;
        _currentHealth = Math.Min(_currentHealth, _maxHealth);
        Console.WriteLine($"{Name} has been healed by {healAmount} points.");
    }

    // Method to increase max health (using PascalCase)
    public void IncreaseMaxHealth()
    {
        _maxHealth += MAX_HEALTH_INCREMENT;
        Console.WriteLine($"{Name}'s maximum health has been increased to {_maxHealth}.");
    }
}
```
</details>
