﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Julien12150.FreeSims.Game
{
    public class Human
    {
        const float TIMER = 5;
        float timer = TIMER;

        public bool selected;

        public float posX, posY;
        public int finalPosX, finalPosY;

        public int angle;

        Control control;
        Cursor cursor;
        SpriteBatch spriteBatch;

        Sprite sprites;

        public int Social;
        public int Fun;

        public Activity.Activity activity = null;

        Color pants;
        Color hair;
        Color eyes;
        Color shirt;
        Color shoes;
        Color skin;

        public Human(float posX, float posY, int angle, int Social, int Fun, Control control, Cursor cursor, Sprite sprites, SpriteBatch spriteBatch, Color pants, Color hair, Color eyes, Color shirt, Color shoes, Color skin)
        {
            this.posX = posX;
            this.posY = posY;
            finalPosX = (int)posX;
            finalPosY = (int)posY;

            this.angle = angle;
            this.sprites = sprites;

            this.control = control;
            this.cursor = cursor;
            this.spriteBatch = spriteBatch;

            this.Social = Social;
            this.Fun = Fun;

            this.pants = pants;
            this.hair = hair;
            this.eyes = eyes;
            this.shirt = shirt;
            this.shoes = shoes;
            this.skin = skin;
        }

        private bool FindHuman(Human obj)
        {
            if (obj == this)
                return true;
            return false;
        }

        public void Update(GameTime gameTime)
        {
            Predicate<Human> ph = FindHuman;

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsed;

            if (activity != null)
            {
                activity.Update(gameTime);
                if (activity.type == "Talk")
                {
                    if (timer < 0)
                    {
                        timer = TIMER;
                        Fun--;
                    }
                    if (activity.targetH.activity == null)
                    {
                        activity = null;
                    }
                }
                else if(activity.type == "TVWatch")
                {
                    if (timer < 0)
                    {
                        timer = TIMER;
                        Social--;
                    }
                }
            }
            else
            {
                if (timer < 0)
                {
                    timer = TIMER;
                    Social--;
                    Fun--;
                }
            }

            if (Social > 100)
                Social = 100;
            else if (Social < 0)
                Social = 0;

            if (Fun > 100)
                Fun = 100;
            else if (Fun < 0)
                Fun = 0;

            if (selected)
            {
                if(control.A && control.isControllerMode)
                {
                    finalPosX = (int)cursor.posX;
                    finalPosY = (int)cursor.posY;
                    if (activity != null)
                    {
                        if(activity.type == "Talk")
                            activity.targetH.activity = null;
                        else if (activity.type == "TVWatch")
                        {
                            activity.targetI.humanList.RemoveAll(ph);
                        }
                        activity = null;
                    }
                }
                else if(control.LeftMouseClick && !control.isControllerMode)
                {
                    finalPosX = (int)cursor.posX;
                    finalPosY = (int)cursor.posY;
                    if (activity != null)
                    {
                        if (activity.type == "Talk")
                            activity.targetH.activity = null;
                        else if (activity.type == "TVWatch")
                        {
                            activity.targetI.humanList.RemoveAll(ph);
                        }
                        
                        activity = null;
                    }
                }
            }

            if(posX > finalPosX)
            {
                if(posY > finalPosY)
                {
                    posX--;
                    posY--;
                    angle = 5;
                }
                else if (posY == finalPosY)
                {
                    posX--;
                    angle = 6;
                }
                else if (posY < finalPosY)
                {
                    posX--;
                    posY++;
                    angle = 7;
                }
            }
            else if(posX < finalPosX)
            {
                if (posY > finalPosY)
                {
                    posX++;
                    posY--;
                    angle = 3;
                }
                else if (posY == finalPosY)
                {
                    posX++;
                    angle = 2;
                }
                else if (posY < finalPosY)
                {
                    posX++;
                    posY++;
                    angle = 1;
                }
            }
            else if (posX == finalPosX)
            {
                if (posY > finalPosY)
                {
                    posY--;
                    angle = 4;
                }
                else if (posY < finalPosY)
                {
                    posY++;
                    angle = 0;
                }
            }
        }

        public void Draw(GameTime gameTime, float height)
        {
            //spriteBatch.Draw(sprites.humanSprite, new Rectangle((int)posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2), (int)posY - sprites.humanSprites.mNoColor.Height + 16, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height), new Rectangle((sprites.humanSprites.mNoColor.Width / 8 ) * angle, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height), Color.White, 0, Vector2.Zero, SpriteEffects.None, posY / height);
            spriteBatch.Draw(sprites.humanSprites.mEyes, new Rectangle((int)posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2), (int)posY - sprites.humanSprites.mNoColor.Height + 16, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height), eyes, 0, Vector2.Zero, SpriteEffects.None, posY / height);
            spriteBatch.Draw(sprites.humanSprites.mHair, new Rectangle((int)posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2), (int)posY - sprites.humanSprites.mNoColor.Height + 16, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height), hair, 0, Vector2.Zero, SpriteEffects.None, posY / height);
            spriteBatch.Draw(sprites.humanSprites.mNoColor, new Rectangle((int)posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2), (int)posY - sprites.humanSprites.mNoColor.Height + 16, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height), Color.White, 0, Vector2.Zero, SpriteEffects.None, posY / height);
            spriteBatch.Draw(sprites.humanSprites.mPants, new Rectangle((int)posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2), (int)posY - sprites.humanSprites.mNoColor.Height + 16, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height), pants, 0, Vector2.Zero, SpriteEffects.None, posY / height);
            spriteBatch.Draw(sprites.humanSprites.mShirt, new Rectangle((int)posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2), (int)posY - sprites.humanSprites.mNoColor.Height + 16, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height), shirt, 0, Vector2.Zero, SpriteEffects.None, posY / height);
            spriteBatch.Draw(sprites.humanSprites.mShoes, new Rectangle((int)posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2), (int)posY - sprites.humanSprites.mNoColor.Height + 16, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height), shoes, 0, Vector2.Zero, SpriteEffects.None, posY / height);
            spriteBatch.Draw(sprites.humanSprites.mSkin, new Rectangle((int)posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2), (int)posY - sprites.humanSprites.mNoColor.Height + 16, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height), skin, 0, Vector2.Zero, SpriteEffects.None, posY / height);

            if (activity != null)
            {
                activity.Draw(gameTime, spriteBatch, sprites);
            }
        }
    }
}