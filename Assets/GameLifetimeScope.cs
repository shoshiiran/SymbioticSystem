using VContainer;
using VContainer.Unity;
using Symbiotic.Core;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        // builder.Register<BehaviorService>(Lifetime.Singleton);

        // builder.RegisterEntryPoint<BehaviorService>();
        builder.Register<LogService>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();


        builder.UseEntryPoints(Lifetime.Singleton, EntryPoints =>
        {
            EntryPoints.Add<EcsService>();
            // EntryPoints.Add<BehaviorService>();
            EntryPoints.Add<TimerService>();

        });

        builder.RegisterEntryPoint<BehaviorService>().AsSelf();
        // builder.RegisterEntryPoint<BehaviorService>();



        builder.RegisterEntryPoint<Launcher>();
    }
}
