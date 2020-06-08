using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isometric
{
    class NKeyboard
    {
        private KeyboardState oldState;
        public KeyboardState State { get; private set; }

        public NKeyboard()
        {
            oldState = State = Keyboard.GetState();
        }

        public void Update()
        {
            oldState = State;
            State = Keyboard.GetState();
        }

        /// <summary>
        /// Check if a key is currently down
        /// </summary>
        public bool IsKeyDown(Keys k)
            => State.IsKeyDown(k);

        /// <summary>
        /// Check if a key is currently up
        /// </summary>
        public bool IsKeyUp(Keys k)
            => State.IsKeyUp(k);

        /// <summary>
        /// Check if a key was pressed this frame
        /// </summary>
        public bool IsKeyPressed(Keys k)
            => State.IsKeyDown(k) && oldState.IsKeyUp(k);

        /// <summary>
        /// Check if a key was released this frame
        /// </summary>
        public bool IsKeyReleased(Keys k)
            => State.IsKeyUp(k) && oldState.IsKeyDown(k);

    }
}
