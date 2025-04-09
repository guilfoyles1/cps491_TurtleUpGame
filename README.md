# Turtle Up Edutainment Game

## Overview  
**Turtle Up** is an edutainment game developed for the Turtle Up recycling center in Kokrobite, Ghana. The game is designed to raise awareness about sea turtle conservation through interactive gameplay, combining education and entertainment. Players experience both human and turtle perspectives, learning about the impact of pollution on marine life and the importance of proper waste disposal.

## Features

### Two Gameplay Perspectives:
- **Human (Level 1):** Collect and sort trash, navigate obstacles, and help maintain a clean beach environment. Properly dispose of items using a drag-and-drop inventory system and color-coded bins.
- **Turtle (Level 2):** Scavenge for food while avoiding plastic waste, illustrating the challenges sea turtles face in polluted waters.

### Character Customization:
Players can select from five native turtle species and a variety of human avatars, reflecting the biodiversity of Ghana and allowing for representation and player choice.

### Inventory System:
Tracks collected trash and allows for proper disposal at designated bins. Improper sorting will eventually trigger feedback to help guide player learning *(in progress)*.

### Educational Integration:
Pop-up learning moments and visuals demonstrate conservation best practices. A tutorial sequence introduces objectives and controls for new players.

### Mobile Compatibility:
Click-to-move functionality ensures a smooth experience on touchscreen devices. WASD and double-click-to-run are also supported for desktop play.

### Immersive Design Elements:
Includes a day-night cycle, animated objects (fire pit, windmill, flowing stream), sound effects, and a mini-map to enhance gameplay and player engagement.

---

##  How to Play

The game is divided into two levels, each offering a unique perspective and learning objective:

### 🧍🏾 Level 1: Human Perspective  
**Objective:** Collect and sort trash to help keep the beach clean and protect sea life.

**Controls:**
- **WASD / Arrow Keys** – Move your character  
- **Shift (Hold)** – Sprint  
- **Mouse Click / Tap** – Click-to-walk (single-click) or click-to-sprint (double-click)  
- **Drag & Drop** – Move trash from your inventory to the appropriate recycling bin  
- **Settings Button** – Pause the game and access volume/menu options  

---

### 🐢 Level 2: Turtle Perspective  
**Objective:** Navigate the ocean, avoid plastic waste, and scavenge for food while highlighting the impact of pollution on sea turtles.

**Controls:**
- **WASD / Arrow Keys** – Swim  
- **Shift (Hold)** – Swim faster  
- **Mouse Click / Tap** – Swim to selected spot (single-click) or swim quickly (double-click)  
- **Settings Button** – Pause the game and access volume/menu options  

---

## Installation & Setup

### Prerequisites
Ensure you have the following installed:
- **Unity 2022.3+** (LTS recommended)  
- **Git** (for version control)  
- **Text Editor** (VS Code, Rider, or preferred IDE)  

### Cloning the Repository
```bash
git clone https://github.com/guilfoyles1/cps491group14.git
cd cps491group14
```

### Opening in Unity
1. Open Unity Hub.  
2. Click **Add Project** and select the cloned repository.  
3. Ensure your Unity version matches the project’s required version.  
4. Open the project and allow it to load dependencies.  

---

## Development & Contributions

### Branching & Version Control
- **Main Branch:** Stable version of the game.  
- **Feature Branches:** Develop new features or fixes separately before merging.  
- **Commit Guidelines:** Use clear commit messages (e.g., `fix: resolved collision bug in level 1`)  

### Running the Game
1. Open the project in Unity.  
2. In the Hierarchy, select `Scenes/BeachLevel` (or appropriate scene).  
3. Press **Play** in the Unity editor.  

---

## Reporting Issues

Please create an issue on the repository with:
- A clear description  
- Steps to reproduce  
- Screenshots (if applicable)  

---

## Current Development Progress

### Implemented:
- Player movement (WASD, arrow keys, click-to-move, double-click-to-run)  
- Trash collection and inventory tracking  
- Minimap, camera follow system, pause/settings menu  
- Day-night cycle and animated environmental elements  

### In Progress:
- Character customization  
- UI refinements  
- Recycling/trash bin interaction logic with sorting feedback  
- Tutorial screen for new players  

### Upcoming:
- Educational overlays and pop-ups for conservation messaging  
- Scoring system and Level 2 (Turtle POV) integration  
- Music replacement for licensing compliance  
- Public deployment *(awaiting client guidance on hosting platform)*  

---

## Team Members
- **Shani D. Patel** – Backend Development – patels44@udayton.edu  
- **Saif Ullah** – Backend Development – ullahs3@udayton.edu  
- **Shayna I. Guilfoyle** – Team Lead – guilfoyles1@udayton.edu  
- **Lazar Jevtic** – Frontend Design/Development – jevticl1@udayton.edu  
- **Zachary R. Spears** – Backend Development – spearsz2@udayton.edu  

---

## License

© 2025 Turtle Up and the University of Dayton.

This game was developed by University of Dayton Capstone students in collaboration with Turtle Up, a nonprofit organization. All game assets, code, and educational content were created specifically for Turtle Up’s use and are considered their intellectual property.

No part of this project may be reproduced, modified, or distributed without express permission from Turtle Up.

**For inquiries, contact:** [info@turtleup.org](mailto:info@turtleup.org) or [guilfoyles1@udayton.edu](mailto:guilfoyles1@udayton.edu) (Team Lead)

![Turtle Up Asset](TurtleUpAsset.png?raw=true)
