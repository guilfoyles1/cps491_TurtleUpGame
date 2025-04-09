# Turtle Up Edutainment Game

## Overview  
**Turtle Up** is an edutainment game developed for the Turtle Up recycling center in Kokrobite, Ghana. The game is designed to raise awareness about sea turtle conservation through interactive gameplay, combining education and entertainment. Players experience both human and turtle perspectives, learning about the impact of pollution on marine life and the importance of proper waste disposal.

---

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

## 🕹️ How to Play

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
- **Main Branch:** Stable version of the game.  
- **Feature Branches:** Develop new features or fixes separately before merging.  
- **Commit Guidelines:** Use clear commit messages (e.g., `fix: resolved collision bug in level 1`)  

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
- Player movement (WASD, arrow keys, click-to-move, double-click-to-run)  
- Trash collection and inventory tracking  
- Minimap, camera follow system, pause/settings menu  
- Day-night cycle and animated environmental elements  
- Updated main menu with new visuals, logo, and parallax design  
- Trash bin logic with item tagging and sorting response  
- Slot icon feedback for correct/incorrect sorting  
- Initial deployment to GitHub Pages (WebGL)

### In Progress:
- UI polish and visual feedback improvements  
- Timer mechanic and simplified scoring logic  
- Cross-team scene integration for Level 1 and Level 2  
- Tutorial menu and pop-up overlays  
- Character customization refinement

### Upcoming:
- Final trash sorting logic and educational overlays  
- Client review and testing  
- Public deployment *(via GitHub Pages and/or iframe on TurtleUp.org)*  
- Finalizing documentation for Capstone handoff  

---

## 🌐 Deployment Notes

We are currently exploring how to make the game accessible through the [Turtle Up website](https://turtleup.org). Our goal is to ensure the game is playable on both desktop and mobile devices while keeping it secure and easy to update.

### Current Hosting Options:
- **GitHub Pages (WebGL Build):**  
  You can view the current WebGL build here:  
  [https://spearsz2.github.io/CPS491Group14self/](https://spearsz2.github.io/CPS491Group14self/)  
  This uses Unity’s built-in WebGL exporter and is hosted via GitHub Pages.

- **Turtle Up Website (iframe):**  
  We are in contact with the Turtle Up IT department (Mike) to assess the feasibility of embedding the game via iframe on the organization’s site.  
  - **Pros:** Safer and easier to maintain  
  - **Cons:** May cause issues with mobile compatibility  
  Further testing and collaboration is ongoing.

### Notes for Future Developers:
- WebGL builds must be placed in a `/Build` folder and linked to an `index.html`.
- Some Unity features may not work in WebGL (e.g., certain shaders or intensive physics).
- Use Unity’s compression and resolution settings to optimize for mobile.
- If not using iframe, take care to sandbox the game to avoid any potential conflicts with the hosting website.

---

## 🎨 Asset Credits

We used public asset packs to build and customize visuals for this project. Below are the original sources:

- [Trash & Junk Asset Pack by BTL Games](https://btl-games.itch.io/trash-and-junk-asset-pack)  
- [Sunnyside Tileset by Daniel Diggle](https://danieldiggle.itch.io/sunnyside)  

Assets were slightly modified for style consistency, optimization, and localization.

---

## 👥 Team Members

- **Shani D. Patel** – Backend Development – patels44@udayton.edu  
- **Saif Ullah** – Backend Development – ullahs3@udayton.edu  
- **Shayna I. Guilfoyle** – Team Lead – guilfoyles1@udayton.edu  
- **Lazar Jevtic** – Frontend Design/Development – jevticl1@udayton.edu  
- **Zachary R. Spears** – Backend Development – spearsz2@udayton.edu  

---

## 📄 License

© 2025 Turtle Up and the University of Dayton.

This game was developed by University of Dayton Capstone students in collaboration with Turtle Up, a nonprofit organization. All game assets, code, and educational content were created specifically for Turtle Up’s use and are considered their intellectual property.

No part of this project may be reproduced, modified, or distributed without express permission from Turtle Up.

**For inquiries, contact:** [info@turtleup.org](mailto:info@turtleup.org) or [guilfoyles1@udayton.edu](mailto:guilfoyles1@udayton.edu)

![Turtle Up Asset](TurtleUpAsset.png?raw=true)
