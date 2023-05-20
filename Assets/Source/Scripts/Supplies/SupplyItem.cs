public class SupplyItem
{
    private ForPipeData _element;
    private int _count;

    public ForPipeData Element => _element;
    public int Count => _count;
    public SupplyItem(ForPipeData forPipeData, int count)
    {
        _element = forPipeData;
        _count = count;
    }
}
