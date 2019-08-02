using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace WifiMeasurement.Console.Dataflow.Models
{
    public class DataflowResult<T>
    {
        public ITargetBlock<T> Start { get; set; }

        public IEnumerable<IDataflowBlock> Leaves { get; set; }

        public IEnumerable<Task> Completions
        {
            get
            {
                return Leaves
                    .Select(block => { block.Complete(); return block.Completion; })
                    .ToArray();
            }
        }
    }
}
