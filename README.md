# Turtle Up Edutainment Game

## Overview  
**Turtle Up** is an edutainment game developed for the Turtle Up recycling center in Kokrobite, Ghana. The game raises awareness about sea turtle conservation through interactive gameplay, combining education and entertainment. Players experience both human and turtle perspectives, learning about the impact of pollution on marine life and the importance of proper waste disposal.

---

## Features

### Two Gameplay Perspectives:
- **Human (Level 1 - Team 2):** Collect and sort trash, navigate obstacles, and help maintain a clean beach environment. Properly dispose of items using a drag-and-drop inventory system and color-coded bins.
- **Turtle (Level 2 - Team 1):** Scavenge for food while avoiding plastic waste and fishing nets, illustrating the challenges sea turtles face in polluted waters. Developed using an underwater-themed asset pack and unique swimming mechanics.

### Character Presets & Customization:
Players can choose from five native turtle species and a variety of human avatars, reflecting the biodiversity of Ghana and promoting player choice and representation.

### Stamina Bar Mechanic:
Level 1 now includes a stamina bar that limits sprinting, encouraging strategic movement and pacing during trash collection.

### Inventory System:
Tracks collected trash and allows for proper disposal at designated bins. Incorrect sorting triggers visual/audio feedback and affects player scoring.

### Educational Integration:
Pop-up learning moments and visuals demonstrate conservation best practices. A tutorial sequence introduces objectives and controls for new players.

### Mobile Compatibility:
Click-to-move functionality ensures a smooth experience on touchscreen devices. WASD and double-click-to-run are supported for desktop play.  
In Level 2, a **hold-to-move** mechanic better reflects the turtle’s swimming behavior.

### Immersive Design Elements:
Includes a day-night cycle, animated details (fire pit, windmill, flowing stream), sound effects, and a mini-map to enhance gameplay engagement.

---

## 🕹️ How to Play

### 🧍🏾 Level 1: Human Perspective  
**Objective:** Collect and sort trash to help keep the beach clean and protect sea life.

**Controls:**
- **WASD / Arrow Keys** – Move your character  
- **Shift (Hold)** – Sprint (drains stamina)
- **Mouse Click / Tap** – Click-to-walk (single-click) or click-to-sprint (double-click)  
- **Drag & Drop** – Move trash from inventory to the correct recycling bin  
- **Settings Button** – Pause the game and access menu options  

---

### 🐢 Level 2: Turtle Perspective  
**Objective:** Navigate the ocean, avoid plastic waste, and scavenge for food while highlighting the impact of pollution on marine life.

**Controls:**
- **WASD / Arrow Keys** – Swim  
- **Shift (Hold)** – Swim faster  
- **Mouse Hold / Tap & Hold** – Swim toward held direction  
- **Settings Button** – Pause the game and access menu options  

---

## 🔧 Installation & Setup

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

## 🚀 Development & Contributions

### Branching & Version Control
- **Main Branch:** Stable version of the game  
- **Feature Branches:** New features or fixes developed separately  
- **Commit Guidelines:** Clear commit messages (e.g., `fix: stamina bar not updating on mobile`)  

### Running the Game
1. Open the project in Unity.  
2. In the Hierarchy, select `Scenes/BeachLevel` (or appropriate scene).  
3. Press **Play** in the Unity editor.  

---

## 🐞 Reporting Issues

Please create an issue on the repository with:
- A clear description  
- Steps to reproduce  
- Screenshots (if applicable)  

---

## 📈 Current Development Progress

### Implemented:
- Two-level gameplay (human and turtle perspectives)  
- Trash collection, inventory tracking, and point-based scoring
- Character preset selection and stamina bar mechanic
- Minimap, day-night cycle, and interactive environment
- Updated main menu and tutorial UI
- Merged scenes into a single Unity project with level switching
- Mobile optimization and WebGL deployment via GitHub Pages

### In Progress:
- Final UI polish and accessibility improvements  
- Deployment testing for iframe integration on TurtleUp.org

### Upcoming:
- Final presentation and project handoff to client  

---

## 🌐 Deployment Notes

We are making the game publicly accessible through GitHub Pages and exploring embedding on the [Turtle Up website](https://turtleup.org).

### Current Hosting:
- **Playable Game (WebGL Build):**  
  https://guilfoyles1.github.io/cps491_TurtleUpGame/

- **Project Homepage:**  
  https://guilfoyles1.github.io/cps491group14_homepage/

- **Turtle Up Website Deployment:**  
  Discussed and tested with Turtle Up’s IT team.

### Notes for Future Developers:
- WebGL build must maintain the `/Build` folder structure.
- Optimize mobile responsiveness through Unity settings.
- Limit features that are unsupported in WebGL (e.g., threading, certain shaders).

---

## 🎨 Asset Credits

Assets were adapted from public packs with full licensing and attribution:

- [Trash & Junk Asset Pack by BTL Games](https://btl-games.itch.io/trash-and-junk-asset-pack)  
- [Sunnyside Tileset by Daniel Diggle](https://danieldiggle.itch.io/sunnyside)  
- [Pixel Kit: Underwater Pack by Noa Calice](https://assetstore.unity.com/packages/2d/environments/pixel-kit-underwater-pack-193051)

Some assets were modified for consistency and localization.

---

## 👥 Team Members

 

### Team 2 (Level 1 – Human Perspective)
 
- **Shayna I. Guilfoyle** – Team Lead – guilfoyles1@udayton.edu  
- **Shani D. Patel** – Backend Development – patels44@udayton.edu  
- **Saif Ullah** – Backend Development – ullahs3@udayton.edu  
- **Zachary R. Spears** – Backend Development – spearsz2@udayton.edu  
- **Lazar Jevtic** – Frontend Design/Development – jevticl1@udayton.edu  

### Team 1(Level 2 – Turtle Perspective)
- **Grant C. Lloyd** – Team Lead – lloydg1@udayton.edu  
- **James S. Jarvis** – Backend Development – jarvisj1@udayton.edu  
- **Kiran J. Khettry** – Backend Development – khettryk1@udayton.edu  

---

## 📄 License

© 2025 Turtle Up and the University of Dayton.

This game was developed by University of Dayton Capstone students in collaboration with Turtle Up, a nonprofit organization. All assets, code, and educational content are considered Turtle Up’s intellectual property.

No part of this project may be reproduced, modified, or distributed without permission.

**For inquiries, contact:** [info@turtleup.org](mailto:info@turtleup.org) or [guilfoyles1@udayton.edu](mailto:guilfoyles1@udayton.edu)

![Turtle Up Asset](TurtleUpAsset.png?raw=true)
