﻿using System;

namespace RobotGA_Project.GASolution
{
    public abstract class Constants
    {
        /*
         * Abstract class to stock all constant values throughout the project.
         * All constant quantities open for changes.
         */

        public static readonly int ChromosomeSize = 8; // Size measured in bits quantity

        public static readonly int ChromosomeQuantity = 3;

        public static readonly int GenotypeMinvalue = 0;
        public static readonly int GenotypeMaxValue = (int)Math.Pow(2,ChromosomeSize);

        public static readonly int CompleteChromosomeSize = ChromosomeSize * ChromosomeQuantity;
        
        public static readonly int PopulationSize = 80;
        
        public static readonly int MutationProbability = 8;
        
        public static readonly int BasicEnergyConsumption = 1;
        public static readonly int BasicCost = 1;

        public static readonly int TRIES_TOTAL = 75;
        
        public static readonly int GEN_ZERO_PARENT_ID = -1;
        public static readonly (int, int, int) HEAT_INITIAL_COLOR = (255, 139, 61);

        //public static readonly (int, int, int) GENERATIONAL_HEAT_INITIAL_COLOR = (255,139,142);

        public static readonly (int, int, int) GENERATIONAL_HEAT_INITIAL_COLOR = HEAT_INITIAL_COLOR;
        public static readonly int GENERATIONAL_R_DECREMENT_RATIO = 2;
        public static readonly int GENERATIONAL_G_DECREMENT_RATIO = 20;
        public static readonly int GENERATIONAL_B_DECREMENT_RATIO = 130;
        
        public static readonly int R_DECREMENT_RATIO = 4 * GENERATIONAL_R_DECREMENT_RATIO;
        public static readonly int G_DECREMENT_RATIO = 4 * GENERATIONAL_G_DECREMENT_RATIO;
        public static readonly int B_DECREMENT_RATIO = 4 * GENERATIONAL_B_DECREMENT_RATIO;
        public static readonly int MOVES_DISPLAY_COLUMNCOUNT = 5;

        // Map Constants
        
        public static readonly int MapDimensions = 20; // Height and width of the squared map
        public static readonly (int, int) StartIndex = (MapDimensions - 1, 0); // Starting position (X,Y)
        public static readonly (int, int) GoalIndex = (0, MapDimensions - 1); // Goal position (X,Y)
        
        // Terrain Constants

        public static readonly int PassableTerrainTypeQuantity = 3;
        public static readonly int TerrainEnergyConsumptionIncrement = 1;
        
        // Terrain types
        
        public static readonly Terrain BlockedTerrain = 
            new Terrain(Int32.MaxValue, Int32.MaxValue, "M");
        
        public static readonly Terrain EasyTerrain = 
            new Terrain(1,BasicEnergyConsumption, "A");
        
        public static readonly Terrain MediumTerrain = 
            new Terrain(2, BasicEnergyConsumption + TerrainEnergyConsumptionIncrement, "B");
        
        public static readonly Terrain DifficultTerrain = 
            new Terrain(3,BasicEnergyConsumption + (2 * TerrainEnergyConsumptionIncrement), "C");
        
        // Software Constants

        public static int BehaviorTraitQuantity = 4;
        public static int SoftwareChromosomeSize = ChromosomeSize * BehaviorTraitQuantity;
        
        // Engine Constants

        public static readonly int EngineTypeQuantity = 3;
        public static readonly int EngineCostIncrement = 5;
        
        public static readonly int EngineMaxCost = (EngineTypeQuantity * EngineCostIncrement) + BasicCost;
        
        // Engine types
        
        public static readonly Engine SmallEngine  = new Engine(EasyTerrain, BasicCost);
        public static readonly Engine MediumEngine = new Engine(MediumTerrain, BasicCost + EngineCostIncrement);
        public static readonly Engine BigEngine    = new Engine(DifficultTerrain, BasicCost + 2 * EngineCostIncrement);
        
        
        // Camera Constants
        
        public static readonly int CameraTypeQuantity = 3;
        public static readonly int BasicRange = 1;
        public static readonly int CameraEnergyConsumptionIncrement = 1;
        public static readonly int CameraCostIncrement = 1;
        
        public static readonly int CameraMaxCost = (CameraTypeQuantity * CameraCostIncrement) + BasicCost;
        
        // Camera types
        
        //  Starts in zero consumption because basic model doesn't consume additional energy
        public static readonly Camera SmallCamera =
            new Camera(BasicRange, 0, BasicCost); 
        
        public static readonly Camera MediumCamera = 
            new Camera(BasicRange + 1, 
                       CameraEnergyConsumptionIncrement, 
                       BasicCost + CameraCostIncrement);
        
        public static readonly Camera BigCamera = 
            new Camera(BasicRange + 2, 
                       2 * CameraEnergyConsumptionIncrement, 
                       BasicCost + 2 * CameraCostIncrement);
        
        
        // Battery Constants
        
        public static readonly int BatteryTypeQuantity = 3;
        public static readonly int BasicEnergy = 50;
        public static readonly int EnergyIncrement = 10;
        public static readonly int BatteryCostIncrement = 4;
        
        public static readonly int BatteryMaxCost = (BatteryTypeQuantity * BatteryCostIncrement) + BasicCost;
        
        // Battery types
        
        public static readonly Battery CommonBattery = 
            new Battery(BasicEnergy, BasicCost);  
        
        public static readonly Battery MediumBattery = 
            new Battery(BasicEnergy + EnergyIncrement, BasicCost + BatteryCostIncrement);
        
        public static readonly Battery SuperBattery  = 
            new Battery(BasicEnergy +  (2 * EnergyIncrement), BasicCost + 2 * BatteryCostIncrement);

        
        // Fitness Constants

        public static readonly int FIRST_PRIORITY_VALUE = 40;
        public static readonly int SECOND_PRIORITY_VALUE = 10;
        public static readonly int THIRD_PRIORITY_VALUE = 5;
        public static readonly int FOURTH_PRIORITY_VALUE = 2;
        public static readonly int LAST_PRIORITY_VALUE = 1;

        public static readonly int FitnessCriteriaQuantity = 5;

        public static readonly int WinBonus = 25;

        public static readonly int MaxFinalDistancePossible =
            (int)MathematicalOperations.DistanceBetweenPoints(StartIndex, GoalIndex);

        public static readonly int MaxEnergyPossible = (BatteryTypeQuantity - 1 ) * EnergyIncrement + BasicEnergy;

        public static readonly int MaxNonRepeatedStepsPossible = MaxEnergyPossible / 2;
        
        public static readonly int MaxEnergyPerStepPossible = 
            (PassableTerrainTypeQuantity - 1) * TerrainEnergyConsumptionIncrement + BasicEnergyConsumption + 
            (CameraTypeQuantity - 1) * CameraEnergyConsumptionIncrement;
        
        public static readonly int MaxCostPossible = BatteryMaxCost + CameraMaxCost + EngineMaxCost;
        
    }
}