using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Julien12150.FreeSims
{
    public class Control
    {
        int width, height;
        public Control(bool isControllerMode, int width, int height)
        {
            this.isControllerMode = isControllerMode;
            this.width = width;
            this.height = height;
        }

        public bool isControllerMode = false;

        public bool DPadRight
        {
            get
            {
                if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed)
                {
                    isControllerMode = true;
                    return true;
                }
                else return false;
            }
        }
        public bool DPadUp
        {
            get
            {
                if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed)
                {
                    isControllerMode = true;
                    return true;
                }
                else return false;
            }
        }
        public bool DPadLeft
        {
            get
            {
                if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed)
                {
                    isControllerMode = true;
                    return true;
                }
                else return false;
            }
        }
        public bool DPadDown
        {
            get
            {
                if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed)
                {
                    isControllerMode = true;
                    return true;
                }
                else return false;
            }
        }

        public float LeftStickX
        {
            get
            {
                return GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X;
            }
        }
        public float LeftStickY
        {
            get
            {
                return GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y;
            }
        }
        public float RightStickX
        {
            get
            {
                return GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X;
            }
        }
        public float RightStickY
        {
            get
            {
                return GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y;
            }
        }

        public bool LeftStick
        {
            get
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.LeftStick == ButtonState.Pressed)
                {
                    isControllerMode = true;
                    return true;
                }
                else
                    return false;
            }
        }
        public bool RightStick
        {
            get
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.RightStick == ButtonState.Pressed)
                {
                    isControllerMode = true;
                    return true;
                }
                else
                    return false;
            }
        }

        public bool A
        {
            get
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
                {
                    isControllerMode = true;
                    return true;
                }
                else return false;
            }
        }
        public bool B
        {
            get
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed)
                {
                    isControllerMode = true;
                    return true;
                }
                else return false;
            }
        }
        public bool X
        {
            get
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
                {
                    isControllerMode = true;
                    return true;
                }
                else return false;
            }
        }
        public bool Y
        {
            get
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed)
                {
                    isControllerMode = true;
                    return true;
                }
                else
                    return false;
            }
        }

        public bool Home
        {
            get
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.BigButton == ButtonState.Pressed)
                {
                    isControllerMode = true;
                    return true;
                }
                else
                    return false;
            }
        }
        public bool Start
        {
            get
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
                {
                    isControllerMode = true;
                    return true;
                }
                else
                    return false;
            }
        }
        public bool Back
        {
            get
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                {
                    isControllerMode = true;
                    return true;
                }
                else
                    return false;
            }
        }

        public bool LB
        {
            get
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.LeftShoulder == ButtonState.Pressed)
                {
                    isControllerMode = true;
                    return true;
                }
                else
                    return false;
            }
        }
        public bool RB
        {
            get
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.RightShoulder == ButtonState.Pressed)
                {
                    isControllerMode = true;
                    return true;
                }
                else
                    return false;
            }
        }
        public float L
        {
            get
            {
                return GamePad.GetState(PlayerIndex.One).Triggers.Left;
            }
        }
        public float R
        {
            get
            {
                return GamePad.GetState(PlayerIndex.One).Triggers.Right;
            }
        }

        public bool LeftMouseClick
        {
            get
            {
                if (Mouse.GetState().X > 0 && Mouse.GetState().X < width && Mouse.GetState().Y > 0 && Mouse.GetState().Y < height)
                { 
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        //isControllerMode = false;
                        return true;
                    }
                }
                return false;
            }
        }
        public bool RightMouseClick
        {
            get
            {
                if (Mouse.GetState().X > 0 && Mouse.GetState().X < width && Mouse.GetState().Y > 0 && Mouse.GetState().Y < height)
                {
                    if (Mouse.GetState().RightButton == ButtonState.Pressed)
                    {
                        //isControllerMode = false;
                        return true;
                    }
                }
                return false;
            }
        }
        public bool MiddleMouseClick
        {
            get
            {
                if (Mouse.GetState().X > 0 && Mouse.GetState().X < width && Mouse.GetState().Y > 0 && Mouse.GetState().Y < height)
                {
                    if (Mouse.GetState().MiddleButton == ButtonState.Pressed)
                    {
                        //isControllerMode = false;
                        return true;
                    }
                }
                return false;
            }
        }

        
        public bool Right
        {
            get
            {
                if (isControllerMode)
                {
                    if (DPadRight)
                        return true;
                }
                else
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Right))
                        return true;
                }
                return false;
            }
        }
        public bool Left
        {
            get
            {
                if (isControllerMode)
                {
                    if (DPadLeft)
                        return true;
                }
                else
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Left))
                        return true;
                }
                return false;
            }
        }
        public bool OtherRight
        {
            get
            {
                if (isControllerMode)
                {
                    if (RB)
                        return true;
                }
                else
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.PageDown))
                        return true;
                }
                return false;
            }
        }
        public bool OtherLeft
        {
            get
            {
                if (isControllerMode)
                {
                    if (LB)
                        return true;
                }
                else
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.PageUp))
                        return true;
                }
                return false;
            }
        }
        public bool Up
        {
            get
            {
                if (isControllerMode)
                {
                    if (DPadUp)
                        return true;
                }
                else
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Up))
                        return true;
                }
                return false;
            }
        }
        public bool Down
        {
            get
            {
                if (isControllerMode)
                {
                    if (DPadDown)
                        return true;
                }
                else
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Down))
                        return true;
                }
                return false;
            }
        }

        public bool Enter
        {
            get
            {
                if (isControllerMode)
                {
                    if (A)
                        return true;
                }
                else
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        return true;
                }
                return false;
            }
        }
        public bool GoBack
        {
            get
            {
                if (isControllerMode)
                {
                    if (Start)
                        return true;
                }
                else
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                        return true;
                }
                return false;
            }
        }
    }
}
