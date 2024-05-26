using System.Collections.Generic;
using System.Drawing;
using Master_Jump.Abstractions;
using Master_Jump.Abstractions.Implementations;
using Master_Jump.Models.Interfaces;
using Master_Jump.Properties;

namespace Master_Jump.Models
{
    public class EnemyUnit : IBaseUnit
    {
        private readonly Dictionary<int, Image> _enemies = new Dictionary<int, Image>
        {
            {1, Resources.enemy1r},
            {2,Resources.enemy3r},
            {3,Resources.monster_2}
        };
        
        public Image Sprite { get; set; } 
        
        public Physics Physics { get; set; }
        
        public Model Model { get; set; }
        public void DrawUnit(Graphics graphics)
        {
            graphics.DrawImage(Sprite, Model.Coordinates.X, Model.Coordinates.Y, Model.Size.Width, Model.Size.Height);
        }
        public EnemyUnit(Model model, PlayerPhysics physics, int number)
        {
            Sprite = _enemies[number];
            
            Model = model;

            Physics = physics;
        }
    }
}