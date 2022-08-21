//RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR
//  RConApps		2018-04-14
//	RConUtility
//  Ticker
//  An advanced timer
//RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR

using System;
using UnityEngine;

namespace RConUtility
{
    [Serializable]
    public class OmniTicker : ITicker
    {
        public enum CountType { CountUp, CountDown, PingPong }
        public CountType direction;
        CountType pingPongDirection;
        bool CountingDown { get { return direction == CountType.CountDown || (direction == CountType.PingPong && pingPongDirection == CountType.CountDown); } }

        public ITicker.EndOfLife EndAction { get { return repeat; } set { repeat = value; } }
        public ITicker.EndOfLife repeat;

        public ITicker.Action State { get { return action; } set { action = value; } }
        ITicker.Action action;

        [SerializeField] private float time;
        [SerializeField] private float currentTime;
        public float RandomMin { get { return randomMin; } set { randomMin = value; } }
        public float RandomMax { get { return randomMax; } set { randomMax = value; } }
        public float randomMin, randomMax;

        public float Percent { get { return currentTime / time; } }

        public bool IsActive { get { return isActive; } set { isActive = value; } }
        bool isActive;

        public bool IsBuzzing { get { return State == ITicker.Action.Buzzing; } }
        public bool TickAndCheckBuzz { get { return Tick() == ITicker.Action.Buzzing; } }

        public OmniTicker()
        {
            time = 1f;
            direction = CountType.CountDown;
            Reset();
        }

        public OmniTicker(float maxTime, CountType countDirection)
        {
            time = maxTime;
            direction = countDirection;

            if(direction == CountType.PingPong)
            {
                pingPongDirection = CountType.CountUp;
            }

            isActive = false;
            Reset();
        }

        public void Start()
        {
            isActive = true;
            action = ITicker.Action.Counting;
        }

        public void Pause()
        {
            isActive = false;
            action = ITicker.Action.Paused;
        }

        public void Reset()
        {
            if(CountingDown)
            {
                currentTime = time;
            }
            else
            {
                currentTime = 0f;
            }
            State = ITicker.Action.Set;
        }

        public ITicker.Action Tick()
        {
            if(isActive)
            {
                action = ITicker.Action.Counting;

                float delta = Time.deltaTime;
                if(CountingDown)
                {
                    delta = -delta;
                }

                currentTime += delta;

                if(CountingDown)
                {
                    if(currentTime <= 0f)
                    {
                        CountdownReached();
                    }
                }
                else
                {
                    if(currentTime >= time)
                    {
                        CountdownReached();
                    }
                }
            }

            return action;
        }

        public void CountdownReached()
        {
            if(direction != CountType.PingPong)
            {
                switch(repeat)
                {
                    case ITicker.EndOfLife.Continue:

                        break;
                    case ITicker.EndOfLife.Freeze:
                        isActive = false;
                        break;
                    case ITicker.EndOfLife.Repeat:
                        Reset();
                        break;
                    case ITicker.EndOfLife.RepeatRandom:
                        time = UnityEngine.Random.Range(randomMin, randomMax);
                        Reset();
                        break;
                }
            }
            else
            {
                if(pingPongDirection == CountType.CountUp)
                {
                    pingPongDirection = CountType.CountDown;
                    currentTime = time;
                }
                else
                {
                    pingPongDirection = CountType.CountUp;
                    currentTime = 0f;
                }
            }
            action = ITicker.Action.Buzzing;
        }

        public void Finish()
        {
            if(CountingDown)
            {
                currentTime = 0f;
            }
            else
            {
                currentTime = time;
            }

            isActive = false;
            action = ITicker.Action.Finished;
        }

        public void Restart()
        {
            Reset();
            Start();
        }
    }
}