using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sokoban.API.Models
{
    public class LevelstoreDatabaseSettings : ILevelstoreDatabaseSettings
    {
        public string LevelsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ILevelstoreDatabaseSettings
    {
        string LevelsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
