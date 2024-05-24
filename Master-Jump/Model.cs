using System.Drawing;

namespace Master_Jump
{
    public class Model
    {
        public PointF Coordinates;
        public SizeF Size;

        public Model(PointF coordinates, SizeF size)
        {
            Coordinates = coordinates;
            Size = size;
        }
    }
}