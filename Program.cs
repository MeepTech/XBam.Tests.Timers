using Meep.Tech.XBam.Configuration;
using Meep.Tech.XBam.Examples.StructOnlyModels;
using System;
using Meep.Tech.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Meep.Tech.XBam.Tests.Timers {
  internal class Program {
    static void Main(string[] args) {
      Loader loader;
      var xbamInitTask 
        = new TimedTask(() => loader = _initLoader())
          .Run();

      Console.WriteLine(
        $"Xbam Initialization for {Archetypes.All.Count()} Archetypes, {Models.DefaultUniverse.Models.Count} Models, {Archetypes.DefaultUniverse.Enumerations.ByType.Sum(t => t.Value.Count())} Enumerations, and {XBam.Components.DefaultUniverse.Components.Count} Components:\n"
          + xbamInitTask.RunTime.ToString()
      );


      Console.WriteLine(
        $"Time to make a struct based model:\n"
        + new TimedTask(() => {
          Tile.Types.Get<Grass>().Make();
        }).Run().RunTime
      );

      int x = 1000;
      List<Tile> tiles = new();
      Console.WriteLine(
        $"Time to make {x} struct based models:\n"
        + new TimedTask(() => {
          tiles.Add(Tile.Types.Get<Grass>().Make());
        }).Run(x).RunTime
      );
         
      x = 1000000;
      tiles.Clear();
      Console.WriteLine(
        $"Time to make {x} struct based models:\n"
        + new TimedTask(() => {
          tiles.Add(Tile.Types.Get<Grass>().Make());
        }).Run(x).RunTime
      );

      x = 100000000;
      tiles.Clear();
      Console.WriteLine(
        $"Time to make {x} struct based models:\n"
        + new TimedTask(() => {
          tiles.Add(Tile.Types.Get<Grass>().Make());
        }).Run(x).RunTime
      );
    }

    static Loader _initLoader() {
      Loader loader = new(new() {
        PreLoadAssemblies = new List<Assembly> {
            typeof(Grass).Assembly
          }
      });
      loader.Initialize();

      return loader;
    }

    static Loader _initLoaderWithLogger() {
      Loader loader = new(new() {
        PreLoadAssemblies = new List<Assembly> {
            typeof(Grass).Assembly
          }
      });

      Universe universe = new(loader);
      universe.SetExtraContext(new ConsoleProgressLogger(true, true));
      loader.Initialize();

      return loader;
    }
  }
}
