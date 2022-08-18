//RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR
//  RConApps		2022-08-18 1:51:44 AM
//	RConUtility
//  CountdownTicker
//  A ticker that counts down for a specified amount of time
//  Usage:
//      Create a new ticker with new
//      Call Start() to activate
//      Call either Tick() or TickAndCheckBuzz in an update loop
//      IsBuzzing or TickAndCheckBuzz will return true 
//          if the timer has passed its threshold
//      If the EndAction is Freeze, calling Reset will run it again
//RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR

using System;
using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;

namespace RConUtility
{
    [Serializable]
	public class CountdownTicker : ITicker
    {
        [ShowInInspector][HideLabel] private const string typeLabel = "Count Down";
        #region Public Vars and Properties
        public bool IsActive { get; set; }
        public bool IsBuzzing => State == ITicker.Action.Buzzing;
        public bool TickAndCheckBuzz => Tick() == ITicker.Action.Buzzing;
        public float Percent => 1f - (_currentTime / _counterTime);
        public ITicker.Action State { get; set; }
        public ITicker.EndOfLife EndAction { get { return _endAction; } set { _endAction = value; } }
        public float RandomMin { get { return _randomMin; } set { _randomMin = value; } }
        public float RandomMax { get { return _randomMax; } set { _randomMax = value; } }
        #endregion

        #region Private Vars and Properties
        [SerializeField][HorizontalGroup("G1")] private float _counterTime;
        [SerializeField][HorizontalGroup("G1")] private float _currentTime;
        [SerializeField] private ITicker.EndOfLife _endAction;
        [ShowIf("_endAction", ITicker.EndOfLife.RepeatRandom)][HorizontalGroup("G2")][SerializeField] private float _randomMin;
        [ShowIf("_endAction", ITicker.EndOfLife.RepeatRandom)][HorizontalGroup("G2")][SerializeField] private float _randomMax;
        #endregion

        #region Initialization
        public CountdownTicker()
        {
            _counterTime = 1f;
            _currentTime = _counterTime;
            IsActive = false;
            State = ITicker.Action.None;
        }

        public CountdownTicker(float maxTime)
        {
            _counterTime = maxTime;
            _currentTime = _counterTime;
            IsActive = false;
            State = ITicker.Action.None;
        }

        public void Start()
        {
            IsActive = true;
            State = ITicker.Action.Set;
        }
        #endregion

        #region Updates
        public ITicker.Action Tick()
        {
            if(IsActive)
            {
                State = ITicker.Action.Counting;

                float delta = Time.deltaTime;

                _currentTime -= delta;

                if(_currentTime <= 0f)
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
                    IsActive = false;
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
            _currentTime = _counterTime;
        }
        #endregion
    }
}