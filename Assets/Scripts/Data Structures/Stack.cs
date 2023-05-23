using System.Collections.Generic;
using System.Linq;

namespace JengaTask
{
    public class Stack
    {
        public List<Block> Blocks { get; private set; }

        public void AddBlock(Block block)
        {
            if (Blocks == null)
            {
                Blocks = new List<Block>();
            }
            Blocks.Add(block);
        }

        public void RearrangeBlocks()
        {
            Blocks = Blocks.OrderBy(block => block.Domain).ThenBy(block => block.Cluster).ThenBy(block => block.StandardId).ToList();
        }
    }
}