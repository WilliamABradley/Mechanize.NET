using System.Drawing;

namespace Mechanize.Forms.Controls
{
    public interface IFormClickable
    {
        void Click(Point Coordinates);

        bool Clicked { get; }
    }
}