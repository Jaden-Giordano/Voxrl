public class Tree : Structure
{
    //Branches
    bool applyWeightOnBranch = true;
    float branchingPercent = 0.3f;
    float branchNarrowing = 0.5f;
    float branchSizeDecrease = 0.75f;

    //Leaves
    int leafLife = 5;
    float leavesPercent = 0.2f;
    int numberOfLeavesSpawned = 2;

    //Trunk
    int MaxLife = 64;
    bool stimulateBranchingAtEnd = true;
    float thickHeightRatio = 0.05f;
    int trunkSplitOriginalLifeDecrease = 1;
    float trunkSplitPercentage = 0.01f;
    float trunkSplitSpawnLifeDecrease = 0.5f;
    float zigzagTrunkPercent = 0.1f;
    int zigzagTrunkStrength = 18;



    public override void GenerateStructure(int x, int z, int initialY)
    {

    }
}

/* http://dwight.skyon.be#c=Articles&p=2DProceduralTreeAlgorithm */