using System;

namespace Meep.Tech.XBam.Tests.Timers {
  public class TimedTask {
    readonly Action _task;

    public DateTime? Start { get; private set; }
    public DateTime? End { get; private set; }
    public TimeSpan? RunTime
      => Start.HasValue && End.HasValue 
        ? End - Start
        : null;

    public TimedTask(Action task) {
      _task = task;
    }

    public TimedTask Run(double numberOfTimes = 1) {
      Start = DateTime.Now;
      while (numberOfTimes-- > 0) {
        _task();
      }
      End = DateTime.Now;

      return this;
    }
  }
}
