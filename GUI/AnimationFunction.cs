using System;

namespace GUI
{
  
    public static class AnimationFunction
    {
        /// <summary>
        /// Linear animation function
        /// </summary>
        /// <param name="time">The current time or the current frame.</param>
        /// <param name="start">The start point of the animation.</param>
        /// <param name="change">The total distance that should be animated across.</param>
        /// <param name="duration">The duration of the animation or the number of frames.</param>
        /// <returns>The position along the animation.</returns>
        public static float Linear(float time, float start, float change, float duration)
        {
            time /= duration;
            return start + change * time;
        }

        /// <summary>
        /// Formula aquired from http://gizma.com/easing/#quint2
        /// </summary>
        /// <param name="time">The current time or the current frame.</param>
        /// <param name="start">The start point of the animation.</param>
        /// <param name="change">The total distance that should be animated across.</param>
        /// <param name="duration">The duration of the animation or the number of frames.</param>
        /// <returns>The position along the animation.</returns>
        public static float EaseOutCirc(float time, float start, float change, float duration)
        {
            time /= duration;
            time--;
            return (float) (change*Math.Sqrt(1 - time*time) + start);
        }


        /// <summary>
        /// Formula aquired from http://gizma.com/easing/#quint2
        /// </summary>
        /// <param name="time">The current time or the current frame.</param>
        /// <param name="start">The start point of the animation.</param>
        /// <param name="change">The total distance that should be animated across.</param>
        /// <param name="duration">The duration of the animation or the number of frames.</param>
        /// <returns>The position along the animation.</returns>
        public static float EaseInOutCirc(float time, float start, float change, float duration)
        {
            time /= duration / 2;
            if (time < 1) return (float) (-change / 2 * (Math.Sqrt(1 - time * time) - 1) + start);
            time -= 2;
            return (float) (change / 2 * (Math.Sqrt(1 - time * time) + 1) + start);
        }
    }
}
