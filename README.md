# Martian Robot

## Overview
This project is a .NET implementation of the classic Martian Robot challenge. It reads a world definition and a sequence of robot instructions, simulates each robot in turn, and produces the final position and orientation for every robot while respecting the world boundaries.

## Problem Statement
The challenge defines a rectangular world containing a set of robots. Each robot starts with a position and heading, then receives a series of instructions:

- L: rotate left
- R: rotate right
- F: move forward one grid space

Rules:
- The world is defined by its upper-right coordinates.
- Robots are processed sequentially.
- A robot must never leave the world.
- If a robot attempts to move beyond the boundary from a position/direction without a scent, the robot is lost and leaves a scent at its last valid position. A subsequent robot attempting the same move from that scented position ignores the move.
- The final output reports each robot's final position and heading.

## Solution Approach
The implementation keeps the domain model simple and the responsibilities explicit. Each component has a clear job, which makes the solution easier to reason about and easier to validate through tests.

Input
  ↓
InputParser
  ↓
World + Robot
  ↓
RobotSimulator
  ↓
Output

- InputParser converts the raw input into structured domain objects.
- World represents the environment and validates whether a proposed position is legal.
- Robot owns its current state and handles rotation and forward movement.
- RobotSimulator coordinates the instruction pipeline and manages the interaction between the robot and the world.

### Architecture Overview

```text
MartianRobot/
├── Domain/
│   ├── Robot
│   ├── Position
│   ├── Orientation
│   └── World
├── Simulator/
│   └── RobotSimulator
├── Parsing/
│   └── InputParser
└── Program.cs

MartianRobot.Tests/
├── Domain/
├── Simulator/
├── Parsing/
└── ...
```

The design intentionally separates concerns: the robot is responsible for its own state and movement logic, while the world owns the rules for valid movement. That split keeps the domain model clean and prevents the robot from becoming tightly coupled to environmental constraints.

## Design Decisions
### Why separate Robot and World?
A robot should know where it is and how it turns, but it should not need to understand the rules of the world it is moving in. Keeping those responsibilities separate reduces coupling and makes both parts easier to test in isolation.

### Why process robots sequentially?
The problem defines the robot sequence explicitly, and the simulation follows that order. Each robot completes its instructions before the next robot is processed.

### Why use an enum for orientation?
Using an enum ensures only valid headings can exist internally. It provides a type-safe representation of the robot's direction and removes the possibility of invalid orientation values creeping into the model.

### Why use a switch for movement and rotation?
The instruction set is small and fixed. A switch-based implementation is straightforward, readable, and appropriate for the current problem size without adding unnecessary abstraction.

## Assumptions
- The input format follows the challenge specification.
- The world is defined by the supplied upper-right coordinates.
- Instructions are limited to L, R, and F.
- Robot simulation is performed in the order they appear in the input.
- A movement that would cause a robot to fall off the grid is ignored only if a scent exists at the robot's current position and orientation; otherwise, the robot is marked as LOST and leaves a scent.

## Testing
The solution is covered with xUnit and Shouldly, and the tests are focused on real behaviour rather than implementation details. The key areas covered include:

- robot movement
- rotation and orientation wrapping
- world boundary handling
- input parsing
- multiple robot scenarios
- end-to-end challenge examples
- invalid input handling where applicable

## Technology Choices
| Technology | Reason |
| --- | --- |
| .NET / C# | A strong fit for modelling the domain, keeping the code type-safe, and implementing the simulation clearly |
| xUnit | A reliable and widely used test framework for .NET |
| Shouldly | Produces readable assertions that make tests easier to understand |

## Running the Application
First, restore dependencies, build the solution, and run the test suite:

```bash
dotnet restore
dotnet build
dotnet test
```

### Executing the Simulation

You can stream an input file into the CLI application using your preferred terminal environment:


Command Prompt (CMD) / Linux / macOS:
```DOS
dotnet run < input.txt
```

PowerShell:
```PowerShell
Get-Content input.txt | dotnet run
```

Alternative (Direct Piping):
```DOS
type input.txt | dotnet run
```


Example input format:
This project reads the input file, processes each robot's instructions, and outputs the final position and orientation for each robot in the format required by the challenge.

```text
5 3
1 1 E
RFRFRFRF
3 2 N
FRRFLLFFRRFLL
0 3 W
LLFFFLFLFL
```

## Possible Improvements
The current solution intentionally keeps the model compact because the problem space is relatively small and well-defined. If this were extended to support additional commands, obstacles, collisions, or concurrent robot movement, I would likely introduce a more formal command abstraction or a movement strategy layer.

For the current requirements, the simpler design is the right trade-off: it keeps the code readable, maintainable, and easy to validate without introducing unnecessary complexity.
