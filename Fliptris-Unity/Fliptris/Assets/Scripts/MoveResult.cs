using System;
using System.Collections.Generic;
using System.Linq;

namespace fliptris.core
{
    public class MoveResult
    {
        public bool IsGameOver { get; set; }
        public IEnumerable<Position> RemovedParts { get; set; }
    }
}
