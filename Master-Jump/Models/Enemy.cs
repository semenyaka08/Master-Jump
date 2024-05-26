using System.Collections.Generic;
using System.Drawing;
using Master_Jump.Abstractions;
using Master_Jump.Models.Interfaces;
using Master_Jump.Properties;

namespace Master_Jump.Models
{
    public class Enemy : IBaseUnit
    {
        private readonly Dictionary<int, Image> _enemies = new Dictionary<int, Image>
        {
            {1, Resources.enemy1r},
            {2, Resources.enemy3r},
            {3, Resources.monster_2}
        };

        public bool IsTouched { get; set; }

        public Image Sprite { get; set; } 
        
        public Physics Physics { get; set; }
        
        public Model Model { get; set; }
        
        public void DrawUnit(Graphics graphics)
        {
            graphics.DrawImage(Sprite, Model.Coordinates.X, Model.Coordinates.Y, Model.Size.Width, Model.Size.Height);
        }
        public Enemy(Model model, Physics physics, int number)
        {
            IsTouched = false;
            
            Sprite = _enemies[number];
            
            Model = model;

            Physics = physics;
        }
    }
}