﻿using System;
using System.Collections.Generic;

namespace RobotGA_Project.GASolution
{
    public class Generation
    {
        /*
         * Class that stores and manages the reproduction, mutation and fitness of a population.
         */
        
        public int Id { get; set; }
        
        public List<Robot> Population { get; set; }
        public float FitnessAverage { get; set; }
        
        public Robot BestRun { get; set; }
        public Robot WorstRun { get; set; }

        public Generation(List<Robot> pLastGeneration, int pId)
        {
            /*
             *  Initializes a population by breeding and mutating the last generation.
             */
            
            Id = pId;
            Population = Breed(pLastGeneration);
            GenerationalMutation();
            FitGeneration();
        }

        public Generation(int pId)
        {
            /*
             *  Initializes a random generation.
             */
            
            Id = pId;
            
            Population = new List<Robot>();
            for (int size = 0; size < Constants.PopulationSize; size++)
            {
                Population.Add(new Robot(size,Id));
            }
            FitGeneration();
        }
        
        private List<Robot> Breed(List<Robot> pLastGeneration)
        {
            List<Robot> newGeneration = new List<Robot>();
            
            //GeneticOperations.SetGenerationReproductionProbabilities(pLastGeneration);

            while (newGeneration.Count < Constants.PopulationSize)
            {
                Robot parentA = SelectMatingPartner(pLastGeneration);
                Robot parentB = SelectMatingPartner(pLastGeneration);

                var (child1, child2) = ReproduceIndividuals(parentA, parentB, newGeneration.Count);

                newGeneration.Add(child1);
                newGeneration.Add(child2);
            }

            return newGeneration;
        }

        private void FitGeneration()
        {
            var fitnessList = new List<int>();
            foreach (var robot in Population)
            {
                robot.CalculateFitness();
                fitnessList.Add(robot.Fitness);
                try
                {
                    if (robot.Fitness > BestRun.Fitness) BestRun = robot;
                    if (robot.Fitness < WorstRun.Fitness) WorstRun = robot;
                }
                catch (Exception e)
                {
                    BestRun = robot;
                    WorstRun = robot;
                }
            }
            GeneticOperations.SetGenerationReproductionProbabilities(Population);
            FitnessAverage = MathematicalOperations.Average(fitnessList, fitnessList.Count);
        }

        private void GenerationalMutation()
        {
            /*
             *  Mutates a bit from random individuals from the population.
             *  Number of mutations dictated in Constants.cs
             */
            int individualIndex;
            int bitIndex;
            string completeChromosome;
            int softwareOrHardwareMutate;

            for (int times = 0; times < Constants.MutationProbability; times++)
            {
                individualIndex = 
                    MathematicalOperations.RandomIntegerInRange(0, Constants.PopulationSize);
                softwareOrHardwareMutate = MathematicalOperations.RandomIntegerInRange(0, 2);
                if (softwareOrHardwareMutate == 1)
                {
                    bitIndex = 
                        MathematicalOperations.RandomIntegerInRange(0, Constants.CompleteChromosomeSize);
                    completeChromosome = Population[individualIndex].Hardware.CompleteChromosome;
                    completeChromosome =
                        GeneticOperations.Mutate(bitIndex, completeChromosome);
                    Population[individualIndex].Hardware.Mutate(completeChromosome);
                }
                else
                {
                    bitIndex = 
                        MathematicalOperations.RandomIntegerInRange(0, Constants.SoftwareChromosomeSize);
                    completeChromosome = Population[individualIndex].Software.CompleteChromosome;
                    GeneticOperations.Mutate(bitIndex, completeChromosome);
                    Population[individualIndex].Software.Mutate(completeChromosome);
                }
            }
        }
        
        private Robot SelectMatingPartner(List<Robot> pGeneration)
        {
            var random0To1Number = MathematicalOperations.Random0To1Float();
            float accumulatedProbability = 0;
            var selectedIndex = -1;
            
            for (var index = 0; index < pGeneration.Count; index++)
            {
                accumulatedProbability += pGeneration[index].ReproductionProbability;
                if (!(random0To1Number <= accumulatedProbability)) continue;
                selectedIndex = index;
                break;
            }
            return pGeneration[selectedIndex];
        }

        private (Robot, Robot) ReproduceIndividuals(Robot pParentA, Robot pParentB, int pId)
        {
            int hardwarePartitionIndex =
                MathematicalOperations.RandomIntegerInRange(1, Constants.CompleteChromosomeSize);
            int softwarePartitionIndex = 
                MathematicalOperations.RandomIntegerInRange(1, Constants.SoftwareChromosomeSize);
            
            Robot child1 = new Robot(pParentA, pParentB, hardwarePartitionIndex, softwarePartitionIndex, pId, Id);
            Robot child2 = new Robot(pParentB, pParentA, hardwarePartitionIndex, softwarePartitionIndex,pId + 1,Id);

            return (child1, child2);
        }
    }
}