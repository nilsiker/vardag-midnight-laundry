# Vardag: Midnight Laundry

[![License](https://img.shields.io/badge/license-MIT%2FApache--2.0-informational)](COPYRIGHT.md)

_You awake on your couch to your alarm clock ringing, the TV light slowly washing away your torpor._

_It's midnight and you remember you've booked the late night laundry slot._

## Description

_Vardag: Midnight Laundry_ is a short horror game. It is the first episode of the Vardag horror series, an episodic game anthology featuring everyday scenarios with horror twists.

The game is designed to be finished in a single short sitting (max 1 hour).

## Built with

This game is built using the Godot engine, utilizing free assets and some custom made.

- Godot 4.4
- Blender 3.4
- Audacity
- GIMP

## License

_Vardag_ is dual-licensed under the MIT license and the Apache 2.0 license.

## Acknowledgements

This game has been developed and designed using various resources listed below.

- ambientCG - Public Domain Resources for Physically Based Rendering (https://ambientcg.com)

<details>
    <summary><h2>Design (SPOILERS AHEAD)</h2></summary>

🛑 DO NOT READ FURTHER IF YOU PLAN TO PLAY VARDAG 🛑

### Act 1 (Introduction)

The first 

```mermaid
flowchart
        subgraph r1[Objectives]
            subgraph Optional
                o1o1[Check balcony]
            end
            r1o1[Pick up basket]
        end
        1[Wake up]-->r1
        r1-->3[Leave apartment]
        3-->4[Walk down stairs]
        4-->5[Enter laundry room]
        5-->6[Walk up stairs]
        6-->7[Enter apartment]
```

### Act 2 (Crescendo)

```mermaid 
flowchart
    subgraph r[Requisites]
        r1[Gather clothes]
        r2[Take nap]
        r3[Wake up]
        r2-->r3
    end
    r-->8[Pick up basket]
    8-->9[Leave apartment]
```

### Act 3 (Finale)

</details>
