using System.Collections.Generic;

namespace JengaTask
{
    public static class StacksBuilder
    {
        private static Dictionary<string, Stack> labeledStacks;

        public static Dictionary<string, Stack> CreateLabeledStacks(List<Block> blocks)
        {
            labeledStacks = new Dictionary<string, Stack>();
            AddBlocksToTheirCorrespondingStacks(blocks);
            RearrangeTheOrderOfTheBlocksInEachStack();
            return labeledStacks;
        }

        private static void AddBlocksToTheirCorrespondingStacks(List<Block> blocks)
        {
            foreach (var block in blocks)
            {
                if (!labeledStacks.ContainsKey(block.Grade))
                {
                    labeledStacks[block.Grade] = new Stack();
                }
                labeledStacks[block.Grade].AddBlock(block);
            }
        }

        private static void RearrangeTheOrderOfTheBlocksInEachStack()
        {
            foreach (var stack in labeledStacks.Values)
            {
                stack.RearrangeBlocks();
            }
        }
    }
}