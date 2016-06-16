public class Structure
{
    public Chunk chunk;

    public void Generate(Chunk chunk, int x, int z, int initialY)
    {
        this.chunk = chunk;

        GenerateStructure(x, z, initialY);
    }

    public virtual void GenerateStructure(int x, int z, int initialY) { }
}