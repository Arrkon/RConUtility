//RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR
//  RConApps		2022-08-18 1:51:44 AM
//	RConUtility
//  ITicker
//  Interface for a timer class
//RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR

namespace RConUtility
{
    public interface ITicker
    {
        public enum Action { None, Set, Counting, Paused, Buzzing, Finished }
        public Action State { get; set; }
        public enum EndOfLife { Continue, Freeze, Repeat, RepeatRandom }
        public EndOfLife EndAction { get; set; }
        public bool IsActive { get; set; }
        public bool IsBuzzing { get; }
        public bool TickAndCheckBuzz { get; }
        public float Percent { get; }
        public float RandomMin { get; set; }
        public float RandomMax { get; set; }

        public void CountdownReached();
        public void Finish();
        public void Pause();
        public void Reset();
        public void Start();
        public void Restart();
        public Action Tick();
    }
}