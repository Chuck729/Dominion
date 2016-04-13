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
        /// <summary>
        /// Number of frames.
        /// </summary>
        int AnimationFrames { get; }

        /// <summary>
        /// The current frame.
        /// </summary>
        int AnimationFrame { get; set; }

        /// <summary>
        /// True if the current frame equals the number of frames.
        /// </summary>
        bool Expanded { get; }

        /// <summary>
        /// True if the current frame equals 0.
        /// </summary>
        bool Collapsed { get; }

        /// <summary>
        /// Decides how the current animation frame should be adjusted.
        /// </summary>
        void AdjustAnimationFrame();
    }
}
