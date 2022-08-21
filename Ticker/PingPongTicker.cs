//RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR
//  RConApps		2022-08-18 1:51:44 AM
//	RConUtility
//  PingPongTicker
//  A ticker that ping pongs between 0 and a specified amount of time
//  Usage:
//      Create a new ticker with new
//      Call Start() to activate
//      Call either Tick() or TickAndCheckBuzz in an update loop
//      IsBuzzing or TickAndCheckBuzz will return true 
//          if the timer has passed its threshold
//RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR

using System;
using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;

namespace RConUtility
{
    [Serializable]
    public class PingPongTicker : ITicker
    {
        [ShowInInspector][HideLabel] private const string typeLabel = "Ping Pong";
        #region Public Vars and Properties
        public enum CountType { CountUp, CountDown }
        public bool IsActive { get; set; }
        public bool IsBuzzing => State == ITicker.Action.Buzzing;
        public bool TickAndCheckBuzz => Tick() == ITicker.Action.Buzzing;
        public float Percent => 1f - (_currentTime / _counterTime);
        public ITicker.Action State { get; set; }
        public ITicker.EndOfLife EndAction { 
            get { return _endAction; } 
            set { 
                _endAction = value;
                if(_endAction != ITicker.EndOfLife.Repeat && _endAction != ITicker.EndOfLife.RepeatRandom)
                    _endAction = ITicker.EndOfLife.Repeat;
            }
        }
        public float RandomMin { get { return _randomMin; } set { _randomMin = value; } }
        public float RandomMax { get { return _randomMax; } set { _randomMax = value; } }
        #endregion

        #region Private Vars and Properties
        [SerializeField][HorizontalGroup("G1")] private float _counterTime;
        [SerializeField][HorizontalGroup("G1")] private float _currentTime;
        [ValueDropdown("ValidEOL")][SerializeField] private ITicker.EndOfLife _endAction = ITicker.EndOfLife.Repeat;
        private ITicker.EndOfLife[] ValidEOL = { ITicker.EndOfLife.Repeat, ITicker.EndOfLife.RepeatRandom };
        [ShowIf("_endAction", ITicker.EndOfLife.RepeatRandom)][HorizontalGroup("G2")][SerializeField] private float _randomMin;
        [ShowIf("_endAction", ITicker.EndOfLife.RepeatRandom)][HorizontalGroup("G2")][SerializeField] private float _randomMax;
        private CountType _direction;
        #endregion

        #region Initialization
        public PingPongTicker()
        {
            _counterTime = 1f;
            _currentTime = 0f;
            IsActive = false;
            _direction = CountType.CountUp;
            State = ITicker.Action.None;
        }

        public PingPongTicker(float maxTime)
        {
            _counterTime = maxTime;
            _currentTime = 0f;
            IsActive = false;
            _direction = CountType.CountUp;
            State = ITicker.Action.None;
        }

        public void Start()
        {
            IsActive = true;
            State = ITicker.Action.Counting;
        }
        #endregion

        #region Updates
        public ITicker.Action Tick()
        {
            if(IsActive)
            {
                State = ITicker.Action.Counting;

                float delta = Time.deltaTime;
                if(_direction == CountType.CountDown)
                {
                    delta = -delta;
                }

                _currentTime += delta;

                if(_direction == CountType.CountUp && _currentTime >= _counterTime)
                {
                    CountdownReached();
                }
                else if(_direction == CountType.CountDown && _currentTime <= 0)
                {
                    CountdownReached();
                }                
            }
            return State;
        }
        #endregion

        #region Functions
        public void CountdownReached()
        {
            switch(EndAction)
            {
                case ITicker.EndOfLife.Continue:
                    break;
                case ITicker.EndOfLife.Freeze:
                    break;
                case ITicker.EndOfLife.Repeat:
                    Reset();
                    break;
                case ITicker.EndOfLife.RepeatRandom:
                    _counterTime = UnityEngine.Random.Range(RandomMin, RandomMax);
                    Reset();
                    break;
            }
            State = ITicker.Action.Buzzing;
        }

        public void Finish()
        {
            _currentTime = 0f;
            IsActive = false;
            State = ITicker.Action.Finished;
        }

        public void Pause()
        {
            IsActive = false;
            State = ITicker.Action.Paused;
        }

        public void Reset()
        {
            if(_direction == CountType.CountUp)
            {
                _currentTime = 0f;
            }
            else
            {
                _currentTime = _counterTime;
            }
            State = ITicker.Action.Set;
        }

        public void Restart()
        {
            Reset();
            Start();
        }
        #endregion
    }
}