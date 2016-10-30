using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Julien12150.FreeSims.Game
{
    public class Human
    {
        const float TIMER = 5;
        float timer = TIMER;

        const float BLINKTIMER = 125;
        float blinktimer = BLINKTIMER;

        public bool selected;

        public float posX, posY;
        public float posZ;
        public int finalPosX, finalPosY;

        public int angle;

        Control control;
        Cursor cursor;
        SpriteBatch spriteBatch;

        Sprite sprites;

        public int Social;
        public int Fun;
        public int Hunger;

        public bool cannotDie;
        public bool blinking = false;

        public Activity.Activity activity = null;

        Random blinkNum;

        public string name;
        public bool female;
        public Color pants;
        public Color hair;
        public int hairStyle;
        public Color eyes;
        public Color shirt;
        public Color shoes;
        public Color skin;

        public Texture2D shadow;

        public Human(float posX, float posY, float posZ, int angle, int Social, int Fun, int Hunger, bool cannotDie, Control control, Cursor cursor, Sprite sprites, SpriteBatch spriteBatch, string name, bool female, Color pants, Color hair, int hairStyle, Color eyes, Color shirt, Color shoes, Color skin, int num)
        {
            this.posX = posX;
            this.posY = posY;
            this.posZ = posZ;
            finalPosX = (int)posX;
            finalPosY = (int)posY;

            this.cannotDie = cannotDie;

            this.angle = angle;
            this.sprites = sprites;

            this.control = control;
            this.cursor = cursor;
            this.spriteBatch = spriteBatch;

            this.Social = Social;
            this.Fun = Fun;
            this.Hunger = Hunger;

            this.name = name;
            this.female = female;
            this.pants = pants;
            this.hair = hair;
            this.hairStyle = hairStyle;
            this.eyes = eyes;
            this.shirt = shirt;
            this.shoes = shoes;
            this.skin = skin;

            blinkNum = new Random(((int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds - int.MaxValue) + num);
        }

        private bool FindHuman(Human obj)
        {
            if (obj == this)
                return true;
            return false;
        }

        public void Update(GameTime gameTime)
        {

            int bn = blinkNum.Next(int.MinValue, int.MaxValue);
            if (!blinking)
            {
                if (bn < (int.MinValue + 50000000))
                {
                    blinking = true;
                }
            }
            else
            {
                if(blinktimer < 0)
                {
                    blinking = false;
                    blinktimer = BLINKTIMER;
                }
                blinktimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

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
                        Fun--;
                        Hunger--;
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
                        Social--;
                        Hunger--;
                    }
                }
                else if(activity.type == "Eat")
                {
                    if( timer < 0)
                    {
                        Hunger = 100;
                        activity = null;
                    }
                }
                else
                {
                    if (timer < 0)
                    {
                        Social--;
                        Fun--;
                        Hunger--;
                    }
                }

                if (timer < 0)
                    timer = TIMER;
            }
            else
            {
                if (timer < 0)
                {
                    timer = TIMER;
                    Social--;
                    Hunger--;
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

            if (Hunger > 100)
                Hunger = 100;
            else if (Hunger < 0)
                Hunger = 0;

            if (selected)
            {
                if(control.A && control.isControllerMode)
                {
                    finalPosX = (int)cursor.posX;
                    finalPosY = (int)cursor.posY;
                    if (activity != null)
                    {
                        if (activity.type == "Talk")
                            activity.targetH.activity = null;
                        else if (activity.type == "TVWatch" || activity.type == "Eat")
                            activity.targetI.humanList.RemoveAll(ph);
                        else if (activity.type == "SitChair")
                            activity.targetI.humanList.Remove(this);
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
                        else if (activity.type == "TVWatch" || activity.type == "Eat")
                            activity.targetI.humanList.RemoveAll(ph);
                        else if (activity.type == "SitChair")
                            activity.targetI.humanList.Remove(this);

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

        public void Draw(GameTime gameTime, float height, GraphicsDevice gd)
        {
            if(shadow == null)
                shadow = Shadow.GenerateShadow((int)(((double)sprites.humanSprites.mNoColor.Width / 8) / 1.5), (sprites.humanSprites.mNoColor.Width / 8), gd);
            //spriteBatch.Draw(sprites.humanSprite, new Rectangle((int)posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2), (int)posY - sprites.humanSprites.mNoColor.Height + 16, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height), new Rectangle((sprites.humanSprites.mNoColor.Width / 8 ) * angle, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height), Color.White, 0, Vector2.Zero, SpriteEffects.None, posY / height);
            spriteBatch.Draw(shadow, new Vector2((int)posX - ((sprites.humanSprites.mNoColor.Width / 8) / 3), (int)posY - ((sprites.humanSprites.mNoColor.Width / 8) / 4)), Color.White * 0.5f);
            if (!female)
            {
                if (!blinking)
                {
                    spriteBatch.Draw(sprites.humanSprites.mEyes, new Rectangle((int)posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2), ((int)posY - (int) posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), eyes, 0, Vector2.Zero, SpriteEffects.None, posY / height);
                    spriteBatch.Draw(sprites.humanSprites.mPants, new Rectangle((int)posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), pants, 0, Vector2.Zero, SpriteEffects.None, posY / height);
                    spriteBatch.Draw(sprites.humanSprites.mShirt, new Rectangle((int)posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), shirt, 0, Vector2.Zero, SpriteEffects.None, posY / height);
                    spriteBatch.Draw(sprites.humanSprites.mShoes, new Rectangle((int)posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), shoes, 0, Vector2.Zero, SpriteEffects.None, posY / height);
                    spriteBatch.Draw(sprites.humanSprites.mSkin, new Rectangle((int)posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), skin, 0, Vector2.Zero, SpriteEffects.None, posY / height);
                    spriteBatch.Draw(sprites.humanSprites.mNoColor, new Rectangle((int)posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), Color.White, 0, Vector2.Zero, SpriteEffects.None, posY / height);

                    if (hairStyle != 0)
                    {
                        spriteBatch.Draw(sprites.humanSprites.GetHair(hairStyle), new Rectangle((int)posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), hair, 0, Vector2.Zero, SpriteEffects.None, posY / height);
                        spriteBatch.Draw(sprites.humanSprites.GetHairNoColor(hairStyle), new Rectangle((int)posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), Color.White, 0, Vector2.Zero, SpriteEffects.None, posY / height);
                    }
                }
                else
                {
                    spriteBatch.Draw(sprites.humanSprites.mEyes, new Rectangle((int)posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, sprites.humanSprites.mNoColor.Height / 2, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), eyes, 0, Vector2.Zero, SpriteEffects.None, posY / height);
                    spriteBatch.Draw(sprites.humanSprites.mPants, new Rectangle((int)posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, sprites.humanSprites.mNoColor.Height / 2, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), pants, 0, Vector2.Zero, SpriteEffects.None, posY / height);
                    spriteBatch.Draw(sprites.humanSprites.mShirt, new Rectangle((int)posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, sprites.humanSprites.mNoColor.Height / 2, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), shirt, 0, Vector2.Zero, SpriteEffects.None, posY / height);
                    spriteBatch.Draw(sprites.humanSprites.mShoes, new Rectangle((int)posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, sprites.humanSprites.mNoColor.Height / 2, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), shoes, 0, Vector2.Zero, SpriteEffects.None, posY / height);
                    spriteBatch.Draw(sprites.humanSprites.mSkin, new Rectangle((int)posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, sprites.humanSprites.mNoColor.Height / 2, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), skin, 0, Vector2.Zero, SpriteEffects.None, posY / height);
                    spriteBatch.Draw(sprites.humanSprites.mNoColor, new Rectangle((int)posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, sprites.humanSprites.mNoColor.Height / 2, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), Color.White, 0, Vector2.Zero, SpriteEffects.None, posY / height);

                    if (hairStyle != 0)
                    {
                        spriteBatch.Draw(sprites.humanSprites.GetHair(hairStyle), new Rectangle((int)posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, sprites.humanSprites.mNoColor.Height / 2, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), hair, 0, Vector2.Zero, SpriteEffects.None, posY / height);
                        spriteBatch.Draw(sprites.humanSprites.GetHairNoColor(hairStyle), new Rectangle((int)posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, sprites.humanSprites.mNoColor.Height / 2, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), Color.White, 0, Vector2.Zero, SpriteEffects.None, posY / height);
                    }
                }
            }
            else
            {
                if (!blinking)
                {
                    spriteBatch.Draw(sprites.humanSprites.fEyes, new Rectangle((int)posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), eyes, 0, Vector2.Zero, SpriteEffects.None, posY / height);
                    spriteBatch.Draw(sprites.humanSprites.fPants, new Rectangle((int)posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), pants, 0, Vector2.Zero, SpriteEffects.None, posY / height);
                    spriteBatch.Draw(sprites.humanSprites.fShirt, new Rectangle((int)posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), shirt, 0, Vector2.Zero, SpriteEffects.None, posY / height);
                    spriteBatch.Draw(sprites.humanSprites.fShoes, new Rectangle((int)posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), shoes, 0, Vector2.Zero, SpriteEffects.None, posY / height);
                    spriteBatch.Draw(sprites.humanSprites.fSkin, new Rectangle((int)posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), skin, 0, Vector2.Zero, SpriteEffects.None, posY / height);
                    spriteBatch.Draw(sprites.humanSprites.fNoColor, new Rectangle((int)posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), Color.White, 0, Vector2.Zero, SpriteEffects.None, posY / height);

                    if (hairStyle != 0)
                    {
                        spriteBatch.Draw(sprites.humanSprites.GetHair(hairStyle), new Rectangle((int)posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), hair, 0, Vector2.Zero, SpriteEffects.None, posY / height);
                        spriteBatch.Draw(sprites.humanSprites.GetHairNoColor(hairStyle), new Rectangle((int)posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), Color.White, 0, Vector2.Zero, SpriteEffects.None, posY / height);
                    }
                }
                else
                {
                    spriteBatch.Draw(sprites.humanSprites.fEyes, new Rectangle((int)posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, sprites.humanSprites.fNoColor.Height / 2, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), eyes, 0, Vector2.Zero, SpriteEffects.None, posY / height);
                    spriteBatch.Draw(sprites.humanSprites.fPants, new Rectangle((int)posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, sprites.humanSprites.fNoColor.Height / 2, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), pants, 0, Vector2.Zero, SpriteEffects.None, posY / height);
                    spriteBatch.Draw(sprites.humanSprites.fShirt, new Rectangle((int)posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, sprites.humanSprites.fNoColor.Height / 2, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), shirt, 0, Vector2.Zero, SpriteEffects.None, posY / height);
                    spriteBatch.Draw(sprites.humanSprites.fShoes, new Rectangle((int)posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, sprites.humanSprites.fNoColor.Height / 2, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), shoes, 0, Vector2.Zero, SpriteEffects.None, posY / height);
                    spriteBatch.Draw(sprites.humanSprites.fSkin, new Rectangle((int)posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, sprites.humanSprites.fNoColor.Height / 2, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), skin, 0, Vector2.Zero, SpriteEffects.None, posY / height);
                    spriteBatch.Draw(sprites.humanSprites.fNoColor, new Rectangle((int)posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, sprites.humanSprites.fNoColor.Height / 2, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), Color.White, 0, Vector2.Zero, SpriteEffects.None, posY / height);

                    if (hairStyle != 0)
                    {
                        spriteBatch.Draw(sprites.humanSprites.GetHair(hairStyle), new Rectangle((int)posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, sprites.humanSprites.fNoColor.Height / 2, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), hair, 0, Vector2.Zero, SpriteEffects.None, posY / height);
                        spriteBatch.Draw(sprites.humanSprites.GetHairNoColor(hairStyle), new Rectangle((int)posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2), ((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, sprites.humanSprites.fNoColor.Height / 2, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), Color.White, 0, Vector2.Zero, SpriteEffects.None, posY / height);
                    }
                }
            }
            if (activity != null)
            {
                activity.Draw(gameTime, spriteBatch, sprites);
            }
        }
    }
}
