﻿// Copyright (C) 2012-2013 Weekend Game Studio
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.

#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Input;
using WaveEngine.Common.Math;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Physics3D;
using WaveEngine.Framework.Services; 
#endregion

namespace StockRoomProject.Behaviors
{
    public class LaunchBehavior : Behavior
    {
        [RequiredComponent]
        public Camera Camera;

        private const int BALL_MASS = 3;
        private Vector3 ballSize = new Vector3(10);
        private TouchPanelState touchPanelState;
        private bool pressed;
        private Entity ball;

        public LaunchBehavior()
            : base("LaunchBehavior")
        {
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void Update(TimeSpan gameTime)
        {
             touchPanelState = WaveServices.Input.TouchPanelState;

             if (touchPanelState.IsConnected && touchPanelState.Count > 0)
             {
                 if (!pressed)
                 {
                     pressed = true;

                     if (ball == null)
                     {
                         ball = Helpers.CreateSphere("ball", Vector3.Zero, ballSize, BALL_MASS, Color.Gray);
                         EntityManager.Add(ball);
                     }

                     RigidBody3D rigidBody = ball.FindComponent<RigidBody3D>();
                     rigidBody.ResetPosition(Camera.Position);
                     var direction = Camera.LookAt - Camera.Position;
                     rigidBody.ApplyLinearImpulse(3000 * direction);                     
                 }                 
             }
             else
             {
                 pressed = false;
             }
        }               

        public void Reset()
        {
            if (ball != null)
            {
                EntityManager.Remove(ball);
            }
        }
    }
}