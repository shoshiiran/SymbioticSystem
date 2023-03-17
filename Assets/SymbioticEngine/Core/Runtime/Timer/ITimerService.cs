using System;
public interface ITimerService
{
    void RegisterUpdate();
    void RegisterLaterUpdate();
    void RegisterFixUpdate();
    void RegisterTimer(float time, EventHandler eventHandler);
}