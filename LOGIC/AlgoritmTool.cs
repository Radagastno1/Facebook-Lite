namespace LOGIC;
public class AlgoritmTool
{
    public int BinarySearch(int[]data, int search)
    {
        int lower = 1;
        int upper = data.Length;
        while(lower <= upper)
        {
            int mid = (lower + upper)/2;
            if(data[mid] > search)
            {
                upper = lower - 1;
            }
            else if(data[mid] < search)
            {
                lower = mid + 1;
            }
            else
            {
                return mid;
            }
        }
        return -1;
    }
}