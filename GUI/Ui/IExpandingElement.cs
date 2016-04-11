using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Ui
{
    /// <summary>
    /// The template to define a standard way Ui elements can feature an epanding visual element.
    /// </summary>
    public interface IExpandingElement
    {
        int AnimationFrames { get; }
        int AnimationFrame { get; set; }
        bool Expanded { get; }
        bool Collapsed { get; }
        void AdjustAnimationFrame();
    }
}
