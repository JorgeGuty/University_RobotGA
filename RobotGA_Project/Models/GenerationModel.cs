﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RobotGA_Project.GASolution;

namespace RobotGA_Project.Models
{
    public class GenerationModel
    {
        [Required] 
        [Display(Name = "ID")] 
        public int Id { get; set; }
        
        [Required] 
        [Display(Name = "Fitness Average")]
        public float FitnessAverage { get; set; }
        
        [Required] 
        [Display(Name = "Worst Fitness")] 
        public int WorstFitness { get; set; }
        
        [Required] 
        [Display(Name = "Best Fitness")] 
        public int BestFitness { get; set; }
        
        [Required] public List<RobotModel> Population { get; set; }
        
        
    }
}