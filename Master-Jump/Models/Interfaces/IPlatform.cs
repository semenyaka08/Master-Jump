using System.Drawing;

namespace Master_Jump.Models.Interfaces
{
    public interface IPlatform
    {
        Image Sprite { get; set; }

        Model Model { get; set; }

        bool IsTouched { get; set; }

        void DrawPlatform(Graphics graphics);
    }
}