# Copilot Instructions for RimWorld Mod: Human Milk Harvesting

This document provides detailed instructions and guidelines for using GitHub Copilot while working on the Human Milk Harvesting mod for RimWorld. This mod introduces new gameplay mechanics where human characters can be milked for resources. The instructions cover an overview of the mod, key features, patterns and conventions, XML integration, Harmony patching, and suggestions for Copilot.

## Mod Overview and Purpose

**Mod Name:** Human Milk Harvesting

**Purpose:** 
The mod enhances the gameplay by introducing a system to gather milk from human characters in the game. This adds a unique and controversial resource management aspect to the game, expanding the player's strategies and choices.

## Key Features and Systems

1. **Milkable Human Component:**
   - The `CompMilkableHuman` class allows human characters to behave similarly to animal characters in terms of being a resource for milk.
   
2. **Harvesting Mechanics:**
   - Implemented through the `JobDriver_MilkHuman` class, extending from `JobDriver_GatherHumanBodyResources`, which handles the job of milking human characters.

3. **WorkGiver Classes:**
   - `WorkGiver_MilkHuman` class outlines the instructions for colonists, allowing them to perform the milking job automatically.

4. **Harmony Patches:**
   - `HarmonyPatches` class and `HaulAIUtility_PawnCanAutomaticallyHaulFast_Patch` provide modifications to existing game mechanics to support new features seamlessly.

## Coding Patterns and Conventions

- **Class and Namespace Organization:**
  - Classes are organized by functionality and extend from existing RimWorld and Verse classes for consistency and integration.
  
- **Naming Conventions:**
  - Classes and methods are prefixed with context-specific names (`Comp`, `JobDriver`, `WorkGiver`) to convey their purpose clearly.

- **Inheritance Use:**
  - Abstract base classes like `HumanCompHasGatherableBodyResource` and `JobDriver_GatherHumanBodyResources` are used for shared functionality.

## XML Integration

- The XML files define the new job types and workgiver settings, ensuring integration with the game's existing job system.
- Ensure that all `ThingDefs` and job links are correctly established within XML files to enable appropriate in-game behavior.

## Harmony Patching

- Harmony patches are used to modify existing game functions to accommodate new behavior without altering original game code. This approach ensures compatibility with other mods.
- Utilize `internal static class HarmonyPatches` to house all patch methods. Follow the pattern of prefix and postfix methods as needed.

## Suggestions for Copilot

1. **Code Completion:**
   - Use Copilot to quickly generate boilerplate code for new classes and methods, adhering to existing class structures.
   
2. **Pattern Recognition:**
   - Leverage Copilot to identify and replicate common patterns in job and comp classes.

3. **Refactoring Assistance:**
   - Utilize Copilot suggestions for refactoring purposes to improve code readability and maintainability.

4. **XML Editing:**
   - While Copilot is limited in XML context, it can suggest attribute completions based on existing game definitions.

5. **Harmony Patch Development:**
   - Seek assistance from Copilot to develop Harmony patches, particularly for finding existing methods that need patching.

By following these guidelines and utilizing Copilot effectively, developers can streamline the process of expanding and maintaining the Human Milk Harvesting mod.
