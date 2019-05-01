using System.Windows.Forms;
using GameFactory.SDK;

namespace MockGame01
{
    public class RealGameOne : IGame
    {
        public string Title { get; private set; }

        public RealGameOne()
        {
            Title = "Real Game #1";
        }
        public void Play()
        {
            var msg = string.Format("Playing {0}!!!", Title);
            MessageBox.Show(msg, Title, MessageBoxButtons.OK);
        }
    }
}
