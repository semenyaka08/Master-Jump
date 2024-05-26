using System;
using System.Collections.Generic;
using Master_Jump.Abstractions.Implementations;
using Master_Jump.Models;
using Master_Jump.Models.Interfaces;

namespace Master_Jump.Controllers
{
    public static class EnemyController
    {
        public static readonly List<IBaseUnit> Enemies = new List<IBaseUnit>();
        
        private static void AddEnemy(IBaseUnit enemy)
        {
            Enemies.Add(enemy);
        }
        
        public static void GenerateEnemy(Model model)
        {
            Random random = new Random();
            IBaseUnit enemy = new Enemy(model, new EnemyPhysics(model), random.Next(1,4));
            AddEnemy(enemy);
        }
        
        public static void ClearEnemy()
        {
            Enemies.RemoveAll(p => p.Model.Coordinates.Y >= 540 || (p is Enemy enemy && enemy.IsTouched));
        }
    }
}