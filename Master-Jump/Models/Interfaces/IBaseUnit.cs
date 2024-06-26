using System.Drawing;
using Master_Jump.Abstractions;
using Master_Jump.Abstractions.Implementations;

namespace Master_Jump.Models.Interfaces
{
    public interface IBaseUnit
    {
        Image Sprite { get; set; }
        
        Physics Physics { get; set; }
        
        Model Model { get; set; }
        
        void DrawUnit(Graphics graphics);
    }
}