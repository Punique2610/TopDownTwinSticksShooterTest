namespace Ilumisoft.TwinStickShooterKit.Game
{
    /// <summary>
    /// Default implementation of the mission component.
    /// It does not perform any logic on it's own ( aside from handling register/unregister and invoking events) and just exposes the interface of the mission component, 
    /// so you can define any logic in other behaviours (or visual scripts) and call CompleteMission or FailMission on this object.
    /// </summary>
    public class Mission : MissionComponent { }
}