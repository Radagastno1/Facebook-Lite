using CORE;
namespace LOGIC;
public class AlgoritmTool
{
    public int BinarySearch(int[] data, int search)
    {
        int lower = 1;
        int upper = data.Length;
        while (lower <= upper)
        {
            int mid = (lower + upper) / 2;
            if (data[mid] > search)
            {
                upper = lower - 1;
            }
            else if (data[mid] < search)
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
    public static List<Conversation> BubblesortConversations(List<Conversation> objects)
    {
        //tagit från w3 schools
        bool needsSorting = true;
        //Gör en loop för varje tal som skall sorteras, avbryt om talen är sorterade
        for (int i = 0; i < objects.Count() - 1 && needsSorting; i++)
        {
            //Signalera att vi börjar om en ny vända med sortering
            needsSorting = false;
            //Gå igenom alla tal fram till och med de tal som ev. 
            //redan är sorterade (variabeln i)
            for (int j = 0; j < objects.Count() - 1 - i; j++)
            {
                //Flytta större tal fram i vektorn
                if (objects[j].ID > objects[j + 1].ID)
                {
                    //Signalera att vi kommer att behöva fortsätta sortera
                    needsSorting = true;
                    //Byt plats på tal
                    int holder = objects[j + 1].ID;
                    objects[j + 1].ID = objects[j].ID;
                    objects[j].ID = holder;
                }
            }
            //Har vi nu inte behövt sortera några tal så är 
            //needsSorting == false och loop'en kommer att avbrytas
        }
        return objects;
    }

    public static List<Conversation> ConversationsPerID(List<Conversation> conversations)
    {
         List<Conversation>conversationsPerId = new();
        List<Conversation>allConversations = conversations;
        foreach(Conversation item in allConversations)
        {
            int thisId = item.ID;
            foreach(Conversation conversation in allConversations)
            {
                if(conversation.ID == thisId)
                {
                    conversationsPerId.Add(conversation);
                }
                else
                {
                    break;
                }
            }
        }
        return conversationsPerId;
    }
}